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
        isGrounded = true;
        isJumping = false;
        stepsSinceLastGrounded = 0;
        if (vultAnim)
        {
            vultAnim.SetBoolean("Airborne", false);
        }
    }

    protected override void Start()
    {
        base.Start();

        initialSpeed = speed;

        if (vultAnim)
        {
            vultAnim.SetBoolean("Airborne", false);
        }

        // vulture's base location
        vultureBase = Vulture != null && Vulture.transform.parent ? Vulture.transform.parent.gameObject : Vulture;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isGrounded && stepsSinceLastGrounded > 1)
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
            if (vultAnim)
            {
                vultAnim.SetTrig("Jump");
            }
        }
    }

    public override void Ducking()  // begin ducking
    {
        if (vultureBase != null && isGrounded)
        {
            vultAnim.SetBoolean("Airborne", false);

            if (_movementDirection != Vector2.zero)
            {
                if (vultAnim)
                {
                    vultAnim.SetBoolean("Glide", true);
                    vultAnim.SetTrig("Downed");
                }
                ChildSwitchState((int)AnimalStates.Sliding);
            }
            else
            {
                vultAnim.SetTrig("Downed");
                ChildSwitchState((int)AnimalStates.Ducking);
            }
        }
    }
}
