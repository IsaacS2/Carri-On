using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class MenuInputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference move, select;

    private void OnEnable()
    {
        select.action.started += Select;
    }

    private void OnDisable()
    {
        select.action.started -= Select;
    }

    private void Select(InputAction.CallbackContext obj)
    {
        Debug.Log("select!");
    }
}
