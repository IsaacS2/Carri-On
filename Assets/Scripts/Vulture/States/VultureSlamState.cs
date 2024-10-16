using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureSlamState : VultureStateClass
{
    [SerializeField] private float beakSlamForce;

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
        return;
    }

    protected override void FixedUpdate()
    {
        isGrounded = vultObj.PlatformContact();

        // Beak Slam force applied
        if (_rb != null) {
            _rb.AddForce(Vector3.down * beakSlamForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Grounded);
        }
    }
}
