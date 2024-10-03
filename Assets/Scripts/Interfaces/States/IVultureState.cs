using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVultureState : IState
{
    bool jump { get; }
    bool duck { get; }
    bool attack { get; }

    void Ducking();
    void DisableDucking();
    void Jumping();
    void DisableJumping();
    void Landing();
}
