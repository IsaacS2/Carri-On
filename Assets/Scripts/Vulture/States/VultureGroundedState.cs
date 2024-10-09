using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureGroundedState : VultureStateClass
{
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

        if (!isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Airborne);
        }
    }

    public override void Jumping()
    {
        if (isGrounded && _rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(Vector3.up * baseJumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public override void Ducking()  // begin ducking
    {
        if (vultureBase != null)
        {
            ChildSwitchState((int)AnimalStates.Ducking);
        }
    }
}
