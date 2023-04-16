using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public ParticleSystem slowVFX;
    public ParticleSystem shieldVFX;
    public ParticleSystem stunVFX;
    public ParticleSystem moneyVFX;
    public ParticleSystem fastVFX;
    public ParticleSystem invertVFX;

    [Space(4)] 
    [Header("Audios")] 
    public AudioClip slowSFX;
    public AudioClip shieldSFX;
    public AudioClip stunSFX;
    public AudioClip moneySFX;
    public AudioClip fastSFX;
    public AudioClip invertSFX;

    private AudioSource AudioSource;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void SlowVFX()
    {
        slowVFX.Play();
        AudioSource.PlayOneShot(slowSFX);
    }
    public void ShieldVFX() 
    {
        shieldVFX.Play();
        AudioSource.PlayOneShot(shieldSFX);
    }
    public void StunVFX() 
    {
        stunVFX.Play();
        AudioSource.PlayOneShot(stunSFX);
    }
    public void MoneyVFX()
    {
        moneyVFX.Play();
        AudioSource.PlayOneShot(moneySFX);
    }
    public void FastVFX() 
    {
        fastVFX.Play();
        AudioSource.PlayOneShot(fastSFX);
    }
    public void InvertVFX()
    {
        invertVFX.Play();
        AudioSource.PlayOneShot(invertSFX);
    }
}
