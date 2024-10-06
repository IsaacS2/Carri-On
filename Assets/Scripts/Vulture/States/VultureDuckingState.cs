using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureDuckingState : VultureStateClass
{
    [SerializeField] private float highJumpPower;

    private float initialSpeed;
    private GameObject vultureBase;

    protected override void Start()
    {
        base.Start();

        initialSpeed = speed;

        // vulture's base location
        vultureBase = Vulture != null && Vulture.transform.parent ? Vulture.transform.parent.gameObject : Vulture;
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
            if (duck)
            {
                DisableDucking();
                _rb.AddForce(Vector3.up * highJumpPower, ForceMode.Impulse);
            }
            else
            {
                _rb.AddForce(Vector3.up * baseJumpPower, ForceMode.Impulse);
            }
            ChildSwitchState((int)AnimalStates.Airborne);
        }
    }

    public override void Ducking()  // begin ducking
    {
        if (vultureBase != null)
        {
            duck = true;
            vultureBase.transform.localScale = new Vector3(vultureBase.transform.localScale.x,
                vultureBase.transform.localScale.y * .5f, vultureBase.transform.localScale.z);
            speed *= (.25f);
        }
    }

    public override void DisableDucking()  // stop ducking
    {
        if (vultureBase != null && initialSpeed != speed)
        {
            duck = false;
            vultureBase.transform.localScale = new Vector3(vultureBase.transform.localScale.x,
                vultureBase.transform.localScale.y * 2, vultureBase.transform.localScale.z);
            speed = initialSpeed;
        }
    }
}