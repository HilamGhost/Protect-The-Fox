using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBall : Ball
{
    public override void ApplyBallEffect()
    {
        GameManager.Instance.GiveScore();
        GameManager.Instance.GiveScore();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            ApplyBallEffect();
            player.SetPlayerAnimatorTrigger("Attack");
            collision.transform.GetComponent<PlayerVisual>().MoneyVFX();
        }
        else if (collision.transform.TryGetComponent(out Fox _fox))
        {
            GameManager.Instance.HitFox();
            _fox.HurtFox();
        }

        DestroyBall();
    }
}
