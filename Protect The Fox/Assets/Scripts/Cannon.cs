using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{
    public Ball loadedBall;
    
    [SerializeField] private Transform shotPoint;
    [SerializeField,Range(2,10)] private int lookOffset;
    [SerializeField] private float randomTime;
    [SerializeField] private ParticleSystem cannonFireVFX;
    [SerializeField] private ParticleSystem cannonSpawnVFX;
    private float needX;
    private float needY;
    private float needVelocity;
    
    private Transform target;
    private Animator animator;
    private AudioSource audioSource;
    private void Start()
    {
        target = FindObjectOfType<Fox>().transform;
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        randomTime = Random.Range(1f, 10f);
        cannonSpawnVFX.Play();
        Invoke("ShootBall",randomTime);
    }

    private void Update()
    {
        LookFox();
        CalculateNeeds();
    }

    void LookFox()
    {
        var _foxPosition = new Vector3(target.transform.position.x,target.transform.position.y+lookOffset , target.transform.position.z);
        transform.LookAt(_foxPosition);
    }

    void CalculateNeeds()
    {
        needX = Vector3.Distance(transform.position, target.transform.position);
        var _shootAngle = Mathf.Atan(lookOffset/needX);
     
        
        needVelocity = needX / Mathf.Cos(_shootAngle);
        needVelocity /= 2.75f;


    }
    void ShootBall()
    {
        var _ballObject =Instantiate(loadedBall.gameObject, shotPoint.transform.position, Quaternion.identity);
        var _ball = _ballObject.GetComponent<Ball>();

        var _direction = shotPoint.forward*needVelocity;
        _ball.ApplyForce(_direction,1);
        
        animator.SetTrigger("Shoot");
        cannonFireVFX.Play();
        audioSource.Play();
        
        GameManager.Instance.DestroyCannon(this);
    }
}
