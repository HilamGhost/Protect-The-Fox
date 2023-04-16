using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : Ball
{

    public override void ApplyBallEffect()
    {
        GameManager.Instance.GiveScore();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            ApplyBallEffect();
            player.SetPlayerAnimatorTrigger("Defend");
            collision.transform.GetComponent<PlayerVisual>().ShieldVFX();
        }
        else if (collision.transform.TryGetComponent(out Fox _fox))
        {
            GameManager.Instance.HitFox();
            _fox.HurtFox();
        }

        DestroyBall();
    }
}
