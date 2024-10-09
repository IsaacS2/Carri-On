using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureAirborneState : VultureStateClass
{
    [SerializeField, Range(0f, 100f)] float maxAcceleration = 4f;
    Vector2 _velocity;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        RaycastHit hit;

        if (_rb != null)
        {
            Vector2 desiredVelocity = new Vector3(_movementDirection.x, _movementDirection.y) * speed;
            _velocity = new Vector2(_rb.velocity.x, _rb.velocity.z);
            float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;

            isGrounded = Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f) && vultObj.PlatformContact();
            Debug.DrawRay(_rb.position, Vector3.down * hit.distance, Color.green, 1f);

            _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, maxSpeedChange);
            _velocity.y = Mathf.MoveTowards(_velocity.y, desiredVelocity.y, maxSpeedChange);
            _rb.velocity = new Vector3(_velocity.x, _rb.velocity.y, _velocity.y);
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
