using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterBall : Ball
{
    private PlayerController player;
    public override void ApplyBallEffect()
    {
        player.ApplyEffect(player.Invert());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            this.player = player;
            ApplyBallEffect();
            player.SetPlayerAnimatorTrigger("Attack");
            collision.transform.GetComponent<PlayerVisual>().InvertVFX();
        }

        DestroyBall();
    }
}
