using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference pause;

    private PlayerControls _input;

    private void Awake()
    {
        _input = new PlayerControls();  // contains all player input maps

        // player is not controllable at the start of the game
        _input.Vulture.Enable();
        _input.Universal.Disable();
        _input.Menu.Enable();
    }

    private void OnEnable()
    {
        pause.action.started += Pause;
    }

    private void OnDisable()
    {
        pause.action.started -= Pause;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        Debug.Log("Pause");
        // Switch input map control between vulture and menu for pausing
        if (_input.Vulture.enabled)
        {
            _input.Vulture.Disable();
            _input.Menu.Enable();
        }
        else
        {
            _input.Vulture.Enable();
            _input.Menu.Disable();
        }
    }
}
