using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    private Rigidbody cannonRb;
    

    public virtual void ApplyForce(Vector3 _direction,float projectileSpeed)
    {
        cannonRb = GetComponent<Rigidbody>();
        cannonRb.AddForce(_direction*projectileSpeed,ForceMode.Impulse);
    }
    public abstract void ApplyBallEffect();

    protected void DestroyBall()
    {
        GetComponentInChildren<ParticleSystem>().Play();;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        
        Destroy(gameObject,1);

    }
}
