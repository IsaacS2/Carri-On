using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureGroundedState : VultureStateClass
{
    private float initialSpeed;
    private GameObject vultureBase;

    private void Start()
    {
        Debug.Log("grounded");
        initialSpeed = speed;
        vultureBase = _vulture.transform.parent.gameObject;  // foot location of the vulture
    }

    private void Update()
    {
        
    }

    public new void Ducking()  // begin ducking
    {
        if (vultureBase != null)
        {
            vultureBase.transform.localScale *= (1/2);
            speed *= (1 / 4);
        }
    }

    public new void DisableDucking()  // stop ducking
    {
        if (vultureBase != null && initialSpeed != speed)
        {
            vultureBase.transform.localScale *= 2;
            speed = initialSpeed;
        }
    }
}
