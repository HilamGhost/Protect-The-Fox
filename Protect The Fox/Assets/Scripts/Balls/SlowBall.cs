using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBall : Ball
{
    private PlayerController player;
    public override void ApplyBallEffect()
    {
        player.ApplyEffect(player.Slow());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            this.player = player;
            ApplyBallEffect();
            player.SetPlayerAnimatorTrigger("Attack");
            collision.transform.GetComponent<PlayerVisual>().SlowVFX();
        }

        DestroyBall();
    }
}
