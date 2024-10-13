using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Vector3 ChangeCameraDirection(Vector3 newDirection)
    {
        return newDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Accepted");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Accepted");
    }
}
