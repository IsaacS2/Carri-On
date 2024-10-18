using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureGlidingState : VultureStateClass
{
    //
    // Thanks to Jasper Flick for his tutorial for Unity movement,
    // found in Catlike Coding: https://catlikecoding.com/unity/tutorials/movement/sliding-a-sphere/
    // 
    [SerializeField, Range(0f, 100f)] private float glideDownwardForce, glideTurnSpeed, newGlideDownwardMultiplier;
    [SerializeField] private bool newGlide;

    private Vector3 previousForwardDirection;
    private Vector2 lastMovementDirection;
    private float turnPercent;

    private void OnEnable()
    {
        RestartGlideState();
    }

    private void OnDisable()
    {
        if (vultAnim)
        {
            vultAnim.SetBoolean("Glide", false);
        }
        if (_rb != null) { _rb.useGravity = true; }
    }

    protected override void Start()
    {
        base.Start();

        RestartGlideState();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (_rb != null)
        {
            isGrounded = vultObj.PlatformContact();

            if (newGlide)
            {
                if (_movementDirection != Vector2.zero && new Vector3(_movementDirection.x, 0, _movementDirection.y) != _rb.transform.forward * -1)
                {
                    if (_movementDirection != lastMovementDirection)
                    {
                        previousForwardDirection = _rb.transform.forward;
                        turnPercent = 0;
                    }
                    lastMovementDirection = new Vector2(_movementDirection.x, _movementDirection.y);
                }

                Vector2 newDirection = Vector2.Lerp(new Vector2(previousForwardDirection.x, previousForwardDirection.z),
                    lastMovementDirection, turnPercent);
                _rb.transform.forward = new Vector3(newDirection.x, 0, newDirection.y);

                _rb.velocity = new Vector3(_rb.transform.forward.x * speed * Time.fixedDeltaTime,
                    0, _rb.transform.forward.z * speed * Time.fixedDeltaTime);
                _rb.AddForce(Vector3.down * glideDownwardForce * Time.fixedDeltaTime * newGlideDownwardMultiplier, 
                    ForceMode.Impulse);

                turnPercent += Time.deltaTime * glideTurnSpeed;
            }
            else
            {
                if (_movementDirection != Vector2.zero)
                {
                    _rb.transform.forward = new Vector3(_movementDirection.x, 0, _movementDirection.y).normalized;
                }

                _rb.velocity = new Vector3(_rb.transform.forward.x * speed * Time.fixedDeltaTime,
                    _rb.velocity.y, _rb.transform.forward.z * speed * Time.fixedDeltaTime);
                _rb.AddForce(Vector3.down * glideDownwardForce * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }
    }

    public override void Jumping()
    {
        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Soaring);
        }
        else
        {
            ChildSwitchState((int)AnimalStates.Airborne);
        }
    }

    public override void Ducking()
    {
        if (vultAnim)
        {
            vultAnim.SetTrig("Downed");
        }

        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Ducking);
        }
        else
        {
            ChildSwitchState((int)AnimalStates.BeakSlam);
        }
    }

    private void RestartGlideState()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
            previousForwardDirection = _rb.transform.forward;
            lastMovementDirection = new Vector2(_rb.transform.forward.x, _rb.transform.forward.z);
            turnPercent = 0;
        }

        if (vultAnim)
        {
            Debug.Log("glide?");
            vultAnim.SetBoolean("Glide", true);
        }
    }
}
