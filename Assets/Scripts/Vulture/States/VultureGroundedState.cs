using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureGroundedState : VultureStateClass
{
    private GameObject vultureBase;
    private float initialSpeed;
    private bool jumping;

    private void OnEnable()
    {
        isJumping = false;
    }

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
        if (_rb) {
            _rb.velocity = _velocity = Vector3.zero;
            _rb.AddForce(Vector3.up * baseJumpPower, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true;
        }
    }

    public override void Ducking()  // begin ducking
    {
        if (vultureBase != null && isGrounded)
        {
            if (_movementDirection != Vector2.zero)
            {
                ChildSwitchState((int)AnimalStates.Sliding);

            }
            else
            {
                ChildSwitchState((int)AnimalStates.Ducking);
            }
        }
    }
}
