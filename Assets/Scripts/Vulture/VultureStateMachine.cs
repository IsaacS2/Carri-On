using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class VultureStateMachine : MonoBehaviour
{
    [SerializeField] private VultureStateClass[] vultureStates;
    [SerializeField] private float maxJumpTime, minAttackBuffer;
    [SerializeField] private int maxJumps;

    private GameObject vulture;
    private AnimalStates state;
    private Vector2 movementDirection;
    private float jumpTimer, postAttackTimer;
    private int jumpsLeft;
    private bool dieContact;

    private void OnEnable()
    {
        GameObject[] vultures = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < vultures.Length; i++)
        {
            // Find players without state machine assigned
            if (vultures[i].GetComponent<VultureObject>() && !vultures[i].GetComponent<VultureObject>().GetStateMachine()) {
                vulture = vultures[i]; 
            }
        }

        if (vulture == null) { Destroy(this); }  // no point in this state machine

        for (int i = 0; i < vultureStates.Length; i++)
        {
            vultureStates[i].Vulture = this.vulture;
        }
    }

    void Start()
    {
        state = AnimalStates.Grounded;  // vulture starts on ground
        postAttackTimer = minAttackBuffer;
        jumpTimer = maxJumpTime;
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        if (jumpTimer >= maxJumpTime) {  }
    }

    public void SetMovementDirection(Vector2 _movementDirection)
    {
        if (state != AnimalStates.Dying) { movementDirection = _movementDirection; }
    }

    public void StartJump()
    {
        if (state != AnimalStates.Dying && jumpsLeft > 0)
        {
            jumpsLeft--;
            vultureStates[Mathf.Min((int)state, vultureStates.Length)].Jumping();

            if (jumpsLeft > 0) { 
                jumpTimer = 0;
            }
        }
    }

    public void HaltJump()
    {
        if (state != AnimalStates.Dying && state != AnimalStates.Airborne && jumpTimer < maxJumpTime) 
        { 
            vultureStates[Mathf.Min((int)state, vultureStates.Length)].DisableJumping(); 
        }
    }

    public void StartDuck()
    {
        if (state != AnimalStates.Dying) {
            vultureStates[Mathf.Min((int)state, vultureStates.Length)].Ducking(); }
    }

    public void HaltDuck()
    {
        if (state != AnimalStates.Dying && state != AnimalStates.Airborne) { vultureStates[Mathf.Min((int)state, vultureStates.Length)].DisableDucking(); }
    }

    public void Attack()
    {
        if (state != AnimalStates.Dying && postAttackTimer >= minAttackBuffer) // buffer to prevent attack spamming
        { 
            postAttackTimer = 0; 
        }
    }
}
