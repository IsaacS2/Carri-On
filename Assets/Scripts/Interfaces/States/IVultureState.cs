using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVultureState : IState
{
    void Ducking();
    void DisableDucking();
    void Jumping();
    void DisableJumping();
    void Landing();
}
