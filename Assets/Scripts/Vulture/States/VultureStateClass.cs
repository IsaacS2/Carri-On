using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureStateClass : MonoBehaviour, IVultureState
{
    protected GameObject _vulture;
    private bool _jump, _duck, _attack;

    public GameObject Vulture
    {
        get
        {
            return _vulture;
        }

        set
        {
            _vulture = value;
        }
    }

    public bool jump
    {
        get
        {
            return _jump;
        }

        set
        {
            _jump = value;
        }
    }

    public bool duck
    {
        get
        {
            return _duck;
        }

        set
        {
            _duck = value;
        }
    }

    public bool attack
    {
        get
        {
            return _attack;
        }

        set
        {
            _attack = value;
        }
    }

    public void Ducking()
    {
        return;
    }

    public void DisableDucking()
    {
        return;
    }

    public void Jumping()
    {
        return;
    }

    public void DisableJumping()
    {
        return;
    }

    public void Landing()
    {
        return;
    }
}
