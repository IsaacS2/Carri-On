using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class VultureInputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference movement, jump, duck, attack;
    [SerializeField] private VultureStateMachine vultureStateMach;

    //public event Action<Vector2> OnMovementChange = (_movementDirection) => { };
    private Vector2 movementDirection;

    private void OnEnable()
    {
        jump.action.started += Jump;
        duck.action.started += Duck;
        attack.action.started += Attack;
        jump.action.canceled += StopJump;
        duck.action.canceled += StopDuck;
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
        duck.action.started -= Duck;
        attack.action.started -= Attack;
        jump.action.canceled -= StopJump;
        duck.action.canceled -= StopDuck;
    }

    private void Start()
    {
        // no point in this script without a vulture state machine
        if (vultureStateMach == null)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        movementDirection = movement.action.ReadValue<Vector2>();

        if (movementDirection != Vector2.zero)
        {
            vultureStateMach.SetMovementDirection(movementDirection);
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("jump!");
        vultureStateMach.StartJump();
    }

    private void Duck(InputAction.CallbackContext obj)
    {
        Debug.Log("duck!");
        vultureStateMach.StartDuck();
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        Debug.Log("attack!");
        vultureStateMach.Attack();
    }

    private void StopJump(InputAction.CallbackContext obj)
    {
        vultureStateMach.HaltJump();
    }

    private void StopDuck(InputAction.CallbackContext obj)
    {
        vultureStateMach.HaltDuck();
    }
}
