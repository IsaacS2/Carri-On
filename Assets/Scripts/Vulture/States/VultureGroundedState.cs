using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureGroundedState : VultureStateClass
{
    private float initialSpeed;
    private GameObject vultureBase;

    protected override void Start()
    {
        base.Start();

        initialSpeed = speed;
        vultureBase = Vulture != null ? Vulture.transform.parent.gameObject : null;  // foot location of the vulture
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Jumping()
    {
        if (jump && _rb != null)
        {
            Debug.Log("Ok");
            _rb.velocity = Vector3.zero;
            _rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    public override void Ducking()  // begin ducking
    {
        if (vultureBase != null)
        {
            vultureBase.transform.localScale = new Vector3(vultureBase.transform.localScale.x, 
                vultureBase.transform.localScale.y * .5f, vultureBase.transform.localScale.z);
            speed *= (.25f);
        }
    }

    public override void DisableDucking()  // stop ducking
    {
        if (vultureBase != null && initialSpeed != speed)
        {
            vultureBase.transform.localScale = new Vector3(vultureBase.transform.localScale.x,
                vultureBase.transform.localScale.y * 2, vultureBase.transform.localScale.z);
            speed = initialSpeed;
        }
    }
}
