using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureDuckingState : VultureStateClass
{
    private Vector3 initialScale;
    private GameObject vultureBase;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (vultureBase != null)
        {
            vultureBase.transform.localScale = new Vector3(initialScale.x, initialScale.y * .5f, initialScale.z);
        }

        isGrounded = true;
        isJumping = false;
        stepsSinceLastGrounded = 0;

        if (vultAnim)
        {
            vultAnim.ResetTrig("Jump");
            vultAnim.ResetTrig("Idle");
            vultAnim.ResetTrig("Downed");
            vultAnim.SetBoolean("Airborne", false);
        }
    }

    private void OnDisable()
    {
        if (vultureBase != null)
        {
            vultureBase.transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
    }

    protected override void Start()
    {
        base.Start();

        vultureBase = Vulture != null && Vulture.transform.parent ? Vulture.transform.parent.gameObject : Vulture;

        if (vultureBase != null)
        {
            vultureBase.transform.localScale = new Vector3(initialScale.x, initialScale.y * .5f, initialScale.z);
        }

        if (vultAnim)
        {
            vultAnim.ResetTrig("Jump");
            vultAnim.ResetTrig("Idle");
            vultAnim.ResetTrig("Downed");
            vultAnim.SetBoolean("Airborne", false);
        }
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
        if (isGrounded && _rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(Vector3.up * baseJumpPower, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true;
            if (vultAnim)
            {
                vultAnim.SetTrig("Jump");
            }
        }
    }
    
    public override void DisableDucking()  // stop ducking
    {
        if (vultureBase != null)
        {
            ChildSwitchState((int)AnimalStates.Grounded);
        }
    }
}