using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureGlidingState : VultureStateClass
{
    //
    // Thanks to Jasper Flick for his tutorial for Unity movement,
    // found in Catlike Coding: https://catlikecoding.com/unity/tutorials/movement/sliding-a-sphere/
    // 
    [SerializeField, Range(0f, 100f)] float maxAcceleration = 4f, glideDownwardForce;

    private void OnEnable()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
        }
    }

    private void OnDisable()
    {
        if (_rb != null) { _rb.useGravity = true; }
    }

    protected override void Start()
    {
        base.Start();

        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        RaycastHit hit;

        if (_rb != null)
        {
            isGrounded = Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f) && vultObj.PlatformContact();
            Debug.DrawRay(_rb.position, Vector3.down * hit.distance, Color.green, 1f);

            if (_movementDirection != Vector2.zero)
            {
                _rb.transform.forward = new Vector3(_movementDirection.x, 0, _movementDirection.y).normalized;
            }

            _rb.velocity = new Vector3(_rb.transform.forward.x * speed * Time.fixedDeltaTime, 
                _rb.velocity.y, _rb.transform.forward.z * speed * Time.fixedDeltaTime);
            _rb.AddForce(Vector3.down * glideDownwardForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    public override void DisableJumping()
    {
        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Grounded);
        }
        else
        {
            ChildSwitchState((int)AnimalStates.Airborne);
        }
    }

    public override void Ducking()
    {
        ChildSwitchState((int)AnimalStates.BeakSlam);
    }
}
