using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureAirborneState : VultureStateClass
{
    [SerializeField, Range(0f, 100f)] float maxAcceleration = 4f, maxDownwardVelocity = 100f;  // limit acceleration and speed that player falls
    Vector2 _XZvelocity;

    private void OnEnable()
    {
        if (vultAnim) {
            Debug.Log("air?");

            vultAnim.SetBoolean("Airborne", true);
            vultAnim.ResetTrig("Jump");
        }
    }

    protected override void Start()
    {
        base.Start();

        if (vultAnim)
        {
            Debug.Log("air?");

            vultAnim.SetBoolean("Airborne", true);
            vultAnim.ResetTrig("Jump");
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (_rb != null)
        {
            Vector2 desiredVelocity = new Vector3(_movementDirection.x, _movementDirection.y) * speed;
            _XZvelocity = new Vector2(_rb.velocity.x, _rb.velocity.z);
            float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;

            isGrounded = vultObj.PlatformContact();

            if (_movementDirection != Vector2.zero)
            {
                _rb.transform.forward = (new Vector3(_movementDirection.x, 0, _movementDirection.y)).normalized;
            }

            _XZvelocity.x = Mathf.MoveTowards(_XZvelocity.x, desiredVelocity.x, maxSpeedChange);
            _XZvelocity.y = Mathf.MoveTowards(_XZvelocity.y, desiredVelocity.y, maxSpeedChange);
            _rb.velocity = new Vector3(_XZvelocity.x, Mathf.Max(_rb.velocity.y, -maxDownwardVelocity), _XZvelocity.y);
        }

        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Grounded);
        }
    }

    public override void Jumping()
    {
        ChildSwitchState((int)AnimalStates.Gliding);
    }

    public override void Ducking()
    {
        ChildSwitchState((int)AnimalStates.BeakSlam);
    }
}
