using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VultureObject : MonoBehaviour
{
    [SerializeField, Range(0f, 90f)] float maxGroundAngle = 25f;

    Vector3 contactNormal;
    float minGroundDotProduct;

    private bool hasStateMachine, platformContact;

    void Awake()
    {
        OnValidate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            EvaluateCollision(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            EvaluateCollision(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformContact = false;
        }
    }

    public bool PlatformContact()
    {
        return platformContact;
    }

    void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            platformContact |= normal.y >= minGroundDotProduct;

            if (normal.y >= minGroundDotProduct)
            {
                contactNormal = normal;
            }
        }
    }

    void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }

    public bool GetStateMachine()
    {
        return hasStateMachine;
    }

    public void SetStateMachine()
    {
        hasStateMachine = true;
    }

    public Vector3 GetContactNormal()
    {
        return contactNormal;
    }
}
