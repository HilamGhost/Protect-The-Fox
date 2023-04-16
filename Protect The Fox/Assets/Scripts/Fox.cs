using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fox : MonoBehaviour
{
    [Header("Borders")] 
    [SerializeField] private Transform maxBorder;
    [SerializeField] private Transform minBorder;
    
    [Header("Properties")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float sitTime = 10;

    [SerializeField] private bool isActive;
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private bool isArrived;
    [Space(2)] 
    [SerializeField] private ParticleSystem hurtEffect;
    [SerializeField] private ParticleSystem brokenHearthEffect;

    private Animator foxAnimator;
    private AudioSource audioSource;
    private void Start()
    {
        foxAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SetFoxPosition());
    }

    private void Update()
    {
        SetFoxAnimation();
        
        if (isActive)
        {
            CheckIsFoxArrived();
            MoveFox();
            ChangeFoxLookRotation();
        }
        
    }

    IEnumerator SetFoxPosition()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            targetPoint = RandomFoxLocation();
            yield return new WaitForSeconds(sitTime);
        }
        
    }
    private Vector3 RandomFoxLocation()
    {
        float randomX = Random.Range(minBorder.position.x,maxBorder.position.x);
        float randomZ = Random.Range(minBorder.position.z,maxBorder.position.z);

        isActive = true;
        return new Vector3(randomX, transform.position.y, randomZ);
    }
    
    void ChangeFoxLookRotation()
    {
        transform.LookAt(targetPoint);
    }
    void CheckIsFoxArrived()
    {
        if ((Mathf.Approximately(transform.position.x, targetPoint.x) &&
             Mathf.Approximately(transform.position.y, targetPoint.y)))
        {
            isArrived = true;
            isActive = false;
        }
        else isArrived = false;

    }
 
    void MoveFox()
    {
        if (!isArrived)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPoint,moveSpeed*Time.deltaTime);
        }
    }

    void SetFoxAnimation()
    {
        foxAnimator.SetBool("isMoving",isActive);
    }

    public void HurtFox()
    {
        audioSource.Play();
        foxAnimator.SetTrigger("Hurt");
        brokenHearthEffect.Play();
        hurtEffect.Play();
    }
    
}
