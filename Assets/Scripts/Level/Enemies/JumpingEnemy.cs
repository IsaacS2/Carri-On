using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpingEnemy : Enemy
{
    [SerializeField] protected GameObject platform;
    [SerializeField] protected Vector3 jumpDirection = Vector3.up;
    [SerializeField] protected float jumpForce, groundedTimeLimit;

    protected Rigidbody _rb;
    protected float groundedTimer;

    protected void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        currentState = AnimalStates.Grounded;

        if (platform)  // we have a platform meant specifically for the enemy to jump on
        {
            platform.transform.parent = null;  // make sure the platform is not the child of this object so it remains still
        }

        groundedTimer = 0;
    }

    protected void Update()
    {
        if (currentState == AnimalStates.Grounded)
        {
            groundedTimer += Time.deltaTime;

            if (groundedTimer >= groundedTimeLimit)
            {
                _rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
                currentState = AnimalStates.Airborne;
                groundedTimer = 0;
            }
        }
        else if (currentState == AnimalStates.Attacking)
        {
            attackingTimer += Time.deltaTime;

            if (attackingTimer >= attackingTimeLimit)
            {
                currentState = AnimalStates.Airborne;
                attackingTimer = 0;
                _rb.useGravity = true;
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (currentState == AnimalStates.Attacking)  // enemy just hit a player
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
        }
        else  // enemy hit a non-player (we'll assume a platform in this case)
        {
            currentState = AnimalStates.Grounded;
        }
    }
}
