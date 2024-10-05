using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VultureObject : MonoBehaviour
{
    private bool hasStateMachine, platformContact;
    private Collider col;

    private void OnEnable()
    {
        col = GetComponent<Collider>();
    }

    public bool GetStateMachine()
    {
        return hasStateMachine;
    }

    public void SetStateMachine()
    {
        hasStateMachine = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformContact = true;
            Debug.Log(platformContact);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformContact = false;
            Debug.Log(platformContact);
        }
    }
}
