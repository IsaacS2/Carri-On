using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TiltingPlatform : MonoBehaviour
{
    [SerializeField] private float reorientSpeed = 1;

    private Rigidbody _rb;
    private Quaternion originalRotation;
    private float rotationProgress;
    private bool rotationRestored;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        originalRotation = _rb.rotation;
        rotationRestored = true;
    }

    private void FixedUpdate()
    {
        if (!rotationRestored)
        {
            rotationProgress += Time.fixedDeltaTime * reorientSpeed;

            _rb.MoveRotation(Quaternion.Lerp(_rb.rotation, originalRotation, rotationProgress));

            if (_rb.rotation == originalRotation)
            {
                rotationRestored = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rotationRestored = false;
            rotationProgress = 0;
        }
    }
}
