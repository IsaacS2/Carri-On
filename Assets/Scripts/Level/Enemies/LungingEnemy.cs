using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungingEnemy : JumpingEnemy
{
    [SerializeField] private float startingZRotation;
    private bool turnEnemy;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
            transform.localEulerAngles.y, transform.localEulerAngles.z + startingZRotation);
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.y < 0 && !turnEnemy && currentState == AnimalStates.Airborne)
        {
            turnEnemy = true;

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y, startingZRotation - 90);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        _rb.velocity = Vector3.zero;

        if (currentState == AnimalStates.Grounded && turnEnemy)
        {
            jumpDirection.x *= -1;  // switch jumping direction
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y + 180, startingZRotation);
            turnEnemy = false;
        }
    }
}
