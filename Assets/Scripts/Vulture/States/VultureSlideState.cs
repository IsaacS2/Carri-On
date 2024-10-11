using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureSlideState : VultureStateClass
{
    [SerializeField] private float slideDuration;

    private float slideTime;
    private bool glidePrepped, duckPrepped;

    private void OnEnable()
    {
        RestartSlideState();
    }

    private void OnDisable()
    {
        if (_rb != null) { _rb.useGravity = true; }
    }

    protected override void Start()
    {
        base.Start();

        RestartSlideState();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        slideTime += Time.deltaTime;
    }

    protected override void FixedUpdate()
    {
        RaycastHit hit;

        if (_rb != null)
        {
            isGrounded = Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f) && vultObj.PlatformContact();
        }

        _rb.velocity = new Vector3(_rb.transform.forward.x * speed * Time.fixedDeltaTime, 
            0, _rb.transform.forward.z * speed * Time.fixedDeltaTime);

        if (slideTime > slideDuration)
        {
            if (!duckPrepped && _movementDirection != Vector2.zero
                && _movementDirection != new Vector2(_rb.transform.forward.x, _rb.transform.forward.z) * -1)  // switching straight into a glide
            {
                ChildSwitchState((int)AnimalStates.Gliding);
            }
            else
            {
                if (isGrounded)
                {
                    if (duckPrepped)
                    {
                        ChildSwitchState((int)AnimalStates.Ducking);
                    }
                    else {
                        ChildSwitchState((int)AnimalStates.Grounded);
                    }
                }
                else
                {
                    ChildSwitchState((int)AnimalStates.Airborne);
                }
            }
        }
    }

    public override void Jumping()
    {
        ChildSwitchState((int)AnimalStates.Soaring);
    }

    public override void DisableDucking()
    {
        duckPrepped = false;
        glidePrepped = false;
    }

    private void RestartSlideState()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
        }

        glidePrepped = duckPrepped = true;
        slideTime = 0;
    }
}
