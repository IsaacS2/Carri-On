using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureSlideState : VultureStateClass
{
    [SerializeField] private float slideDuration;

    private float slideTime;
    private bool glidePrepped;

    private void OnEnable()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
        }

        glidePrepped = true;
        slideTime = 0;
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

    // Update is called once per frame
    protected override void Update()
    {
        slideTime += Time.deltaTime;
    }

    protected override void FixedUpdate()
    {
        _rb.velocity = new Vector3(_rb.transform.forward.x * speed * Time.fixedDeltaTime, 
            0, _rb.transform.forward.z * speed * Time.fixedDeltaTime);

        if (slideTime > slideDuration)
        {
            if (glidePrepped)  // switching straight into a glide
            {
                ChildSwitchState((int)AnimalStates.Gliding);
            }
            else
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
        }
    }

    public override void DisableDucking()
    {
        glidePrepped = false;
    }
}
