using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureObject : MonoBehaviour
{
    private bool hasStateMachine;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetStateMachine()
    {
        return hasStateMachine;
    }
    public void SetStateMachine()
    {
        hasStateMachine = true;
    }
}
