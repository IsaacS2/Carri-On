using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureSoarState : VultureStateClass
{
    //
    // Thanks to Jasper Flick for his tutorial for Unity movement,
    // found in Catlike Coding: https://catlikecoding.com/unity/tutorials/movement/sliding-a-sphere/
    // 
    [SerializeField, Range(0f, 90f)] private float soarTurnSpeed, maxRotation = 45, rotationSpeedExp, expCoefficient, maxSoarTime;

    private Vector2 lastMovementDirection, currentYDirection, previousForwardDirection;
    private float turnPercent, currentUpwardsRotation, soarTime, originalMaxRotation;

    private void OnEnable()
    {
        originalMaxRotation = maxRotation / 90f;  // get normalized value
        RestartSoarState();
    }

    private void OnDisable()
    {
        if (_rb != null)
        {
            _rb.useGravity = false;
            _rb.transform.forward = new Vector3(_rb.transform.forward.x, 0, _rb.transform.forward.z);
        }
    }

    protected override void Start()
    {
        base.Start();

        RestartSoarState();
    }

    protected override void Update()
    {
        base.Update();

        soarTime += Time.deltaTime;
        currentUpwardsRotation = Mathf.Min(Mathf.Pow(soarTime, rotationSpeedExp) * expCoefficient, maxRotation);

        if (soarTime >= maxSoarTime)
        {
            ChildSwitchState((int)AnimalStates.Gliding);
        }
    }

    protected override void FixedUpdate()
    {
        RaycastHit hit;

        if (_rb != null)
        {
            isGrounded = Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f) && vultObj.PlatformContact();
            Debug.DrawRay(_rb.position, Vector3.down * hit.distance, Color.green, 1f);

            // getting direction in relation to x and z only for input comparison
            Vector2 XZForward = new Vector2(_rb.transform.forward.x, _rb.transform.forward.z).normalized;

            if (_movementDirection != Vector2.zero && _movementDirection != XZForward * -1)
            {
                if (_movementDirection != lastMovementDirection)
                {
                    previousForwardDirection = XZForward;
                    turnPercent = 0;
                }
                lastMovementDirection = new Vector2(_movementDirection.x, _movementDirection.y);
            }

            Vector2 newDirection = Vector2.Lerp(new Vector2(previousForwardDirection.x, previousForwardDirection.y),
                lastMovementDirection, turnPercent);
            //Debug.Log(newDirection);
            _rb.transform.forward = new Vector3(newDirection.x, currentUpwardsRotation, newDirection.y).normalized;

            _rb.velocity =_rb.transform.forward * speed * Time.fixedDeltaTime;

            turnPercent += Time.deltaTime * soarTurnSpeed;
        }
    }

    public override void Ducking()
    {
        ChildSwitchState((int)AnimalStates.BeakSlam);
    }

    private void RestartSoarState()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
            _rb.transform.forward = new Vector3(_rb.transform.forward.x, 0, _rb.transform.forward.z);
            lastMovementDirection = previousForwardDirection = new Vector2(_rb.transform.forward.x, _rb.transform.forward.z);
        }

        turnPercent = soarTime = 0;
    }
}
