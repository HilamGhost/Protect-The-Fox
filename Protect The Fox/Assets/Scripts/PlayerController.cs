using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    [Space(2)] 
    [SerializeField] private GameObject playerModel;

    [Space(2)] 
    [SerializeField] private bool isStunned;
    [SerializeField] private bool isGainedSpeed;
    [SerializeField] private bool isInverted;
    [SerializeField] private bool isSlowed;
    
    
    private Rigidbody playerRB;
    private Animator playerAnimator;
    
    
    private Vector3 moveDirection;
    float _yRot = -90;
    
    #region Properties

    public bool IsMoving => !Mathf.Approximately(moveDirection.x, 0) || !Mathf.Approximately(moveDirection.z, 0);

    public float PlayerSpeed
    {
        get
        {
            var speed = moveSpeed;
            if (isSlowed) speed /= 2;
            if (isGainedSpeed) speed *= 2;
            return speed;
        }
    }

    #endregion
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = playerModel.GetComponent<Animator>();
    }

  
    
    
    void Update()
    {
        SetMoveDirection();
        if(!isStunned) ChangeModelsRotation();
        PlayAnimations();
    }

    private void FixedUpdate()
    {
        if(!isStunned) MovePlayer();
    }

    void SetMoveDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
       
        if (isInverted)
        {
            moveDirection = new Vector3(-x, 0, -z);
            return;
        }
        moveDirection = new Vector3(x, 0, z);
    }

    void MovePlayer()
    {
        Vector3 _moveDirectionRaw = -Vector3.right * moveDirection.z + Vector3.forward * moveDirection.x;
        Vector3 _moveDirection = _moveDirectionRaw * PlayerSpeed;

        playerRB.velocity = new Vector3(_moveDirection.x,playerRB.velocity.y,_moveDirection.z);
    }

    #region Visuals

    void ChangeModelsRotation()
    {
        
             if (moveDirection.z ==  1 && moveDirection.x ==  0) _yRot = -90;
        else if (moveDirection.z == -1 && moveDirection.x ==  0) _yRot = 90;
        else if (moveDirection.z ==  0 && moveDirection.x ==  1) _yRot = 0;
        else if (moveDirection.z ==  0 && moveDirection.x == -1) _yRot = 180;
        
        else if (moveDirection.z ==  1 && moveDirection.x ==  1) _yRot = -45;
        else if (moveDirection.z ==  1 && moveDirection.x == -1) _yRot = -135;
        
        else if (moveDirection.z == -1 && moveDirection.x == -1) _yRot = 135;
        else if (moveDirection.z == -1 && moveDirection.x ==  1) _yRot = 45;
        
        playerModel.transform.localRotation = Quaternion.Euler(0,_yRot,0);

    }

    void PlayAnimations()
    {
        playerAnimator.SetBool("isMoving",IsMoving);
        playerAnimator.SetBool("isStunned",isStunned);
    }

    public void SetPlayerAnimatorTrigger(string _animator) => playerAnimator.SetTrigger(_animator);

    #region Properties

    public void ApplyEffect(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    public IEnumerator Stun()
    {
        isStunned = true;
        playerRB.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        isStunned = false;
    }
    public  IEnumerator Slow()
    {
        isSlowed = true;
        yield return new WaitForSeconds(3);
        isSlowed = false;
    }
    public IEnumerator Invert()
    {
        isInverted = true;
        yield return new WaitForSeconds(3);
        isInverted = false;
    }
    public IEnumerator Speed()
    {
        isGainedSpeed = true;
        yield return new WaitForSeconds(3);
        isGainedSpeed = false;
    }
    #endregion

    #endregion

}
