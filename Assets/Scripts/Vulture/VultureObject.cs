using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureObject : MonoBehaviour
{
    private bool hasStateMachine;
    
    public bool GetStateMachine()
    {
        return hasStateMachine;
    }

    public void SetStateMachine()
    {
        hasStateMachine = true;
    }
}
