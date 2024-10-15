using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

//
// Thanks to Board To Bits for the improved jumping code:
// https://www.youtube.com/watch?v=7KiK0Aqtmzc&t=435s
//
public class VultureStateMachine : MonoBehaviour
{
    [SerializeField] private VultureStateClass[] vultureStates;
    [SerializeField] private float maxJumpTime, minAttackBuffer, fallMultiplier, lowJumpMultiplier;
    [SerializeField] private int maxJumps;

    private GameObject vulture;
    private Rigidbody _rb;
    private AnimalStates state;
    private Vector2 movementDirection;
    private float jumpTimer, postAttackTimer;
    private int jumpsLeft;
    private bool dieContact, ducking;

    private void OnEnable()
    {
        GameObject[] vultures = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < vultures.Length; i++)
        {
            // Find players without state machine assigned
            if (vultures[i].GetComponent<VultureObject>() && !vultures[i].GetComponent<VultureObject>().GetStateMachine()) {
                vulture = vultures[i];
                vulture.GetComponent<VultureObject>().SetStateMachine();
            }
        }

        if (vulture == null) { Destroy(this); }  // no point in this state machine without a vulture object
        else if (vulture.transform.parent && vulture.transform.parent.GetComponent<Rigidbody>()) { _rb = vulture.transform.parent.GetComponent<Rigidbody>(); }
        else if (vulture.GetComponent<Rigidbody>()) { _rb = vulture.GetComponent<Rigidbody>(); }

        state = AnimalStates.Grounded;  // vulture starts on ground

        for (int i = 0; i < vultureStates.Length; i++)
        {
            vultureStates[i].Vulture = this.vulture;
            vultureStates[i].OnStateSwitch += SwitchState;
            vultureStates[i].enabled = i == (int)state ? true : false;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < vultureStates.Length; i++)
        {
            if (vultureStates[i])
            {
                vultureStates[i].OnStateSwitch -= SwitchState;
            }
        }
    }

    void Start()
    {
        postAttackTimer = minAttackBuffer;
        jumpTimer = maxJumpTime;
    }

    void Update()
    {
        if (jumpTimer < maxJumpTime) { 
            jumpTimer += Time.deltaTime;
        }
        if (jumpsLeft <= 0)
        {
            //Debug.Log("No jumps left!");
        }
    }

    private void FixedUpdate()
    {
        if (vulture.GetComponent<VultureObject>().GetDeathStatus() && !dieContact) { Death(); }  // player's rigidbody has hit a killing object

        if (_rb && _rb.useGravity)
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * (fallMultiplier - 1);
            }
            else if (_rb.velocity.y > 0 && jumpTimer >= maxJumpTime) 
            { 
                _rb.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * (lowJumpMultiplier - 1); 
            }
        }
    }

    public void StartJump()
    {
        if (state != AnimalStates.Dying)
        {
            //jumpsLeft--;
            ducking = false;

            if (state == AnimalStates.Grounded) { jumpTimer = 0; }
            else { jumpTimer = maxJumpTime; }
            vultureStates[Mathf.Min((int)state, vultureStates.Length - 1)].Jumping();
        }
    }

    public void HaltJump()
    {
        if (state != AnimalStates.Dying)
        {
            jumpTimer = maxJumpTime;

            vultureStates[Mathf.Min((int)state, vultureStates.Length - 1)].DisableJumping();
        }
    }

    public void StartDuck()
    {
        if (state != AnimalStates.Dying) {
            ducking = true;
            vultureStates[Mathf.Min((int)state, vultureStates.Length - 1)].Ducking(); 
        }
    }

    public void HaltDuck()
    {
        if (state != AnimalStates.Dying && state != AnimalStates.Airborne) { 
            vultureStates[Mathf.Min((int)state, vultureStates.Length - 1)].DisableDucking(); 

            if (ducking)
            {
                jumpTimer = maxJumpTime;
            }
        }
    }

    public void Attack()
    {
        if (state != AnimalStates.Dying && postAttackTimer >= minAttackBuffer) // buffer to prevent attack spamming
        {
            postAttackTimer = 0;
        }
    }

    public void Death()
    {
        //Debug.Log("Death cometh");
        dieContact = true;
        SwitchState((int)AnimalStates.Dying);
    }

    public void SwitchState(int _newState) {
        vultureStates[Mathf.Min((int)state, vultureStates.Length - 1)].enabled = false;
        vultureStates[Mathf.Min(_newState, vultureStates.Length - 1)].enabled = true;
        state = (AnimalStates)_newState;
        Debug.Log("new state: " + state);
    }
}
