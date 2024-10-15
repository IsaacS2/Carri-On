using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VultureObject : MonoBehaviour
{
    [SerializeField, Range(0f, 90f)] float maxGroundAngle = 25f;
    [SerializeField, Min(0f)] private float probeDistance = 1.5f;
    [SerializeField] LayerMask probeMask = -1;
    [SerializeField] private Rigidbody _rb;
    
    private Vector3 contactNormal;
    private float minGroundDotProduct;

    private bool hasStateMachine, platformContact, death;

    void Awake()
    {
        OnValidate();
    }

    private void FixedUpdate()
    {
        Debug.Log("On platform: " + platformContact);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            EvaluateCollision(collision);
        }

        if (collision.gameObject.GetComponent<Murderer>())
        {
            death = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Murderer>())
        {
            death = true;
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
        platformContact = false;
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

    public float OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        return minGroundDotProduct;
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

    public bool SnapToGround(ref Vector3 velocity, int _stepsSinceLastGrounded)
    {
        if (_stepsSinceLastGrounded > 1)
        {
            return false;
        }
        if (!Physics.Raycast(_rb.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask))
        {
            return false;
        }
        if (hit.normal.y < minGroundDotProduct)
        {
            return false;
        }

        contactNormal = hit.normal;
        float speed = velocity.magnitude;
        float dot = Vector3.Dot(velocity, hit.normal);

        if (dot > 0f)
        {
            velocity = (velocity - hit.normal * dot).normalized * speed;
        }

        return true;
    }

    public bool GetDeathStatus()
    {
        return death;
    }
}
