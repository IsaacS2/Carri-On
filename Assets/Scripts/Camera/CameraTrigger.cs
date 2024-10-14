using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 newPosDiff, newDirections, newRotation;

    private CameraMovement camMovement;
    private Vector3 newTargetPos;

    private void OnEnable()
    {
        Camera mainCam = Camera.main;

        if (mainCam && mainCam.gameObject.GetComponent<CameraMovement>())
        {
            camMovement = mainCam.gameObject.GetComponent<CameraMovement>();
        }

        // If position fields are zero, grab actual target position and obtain actual difference between target and camera
        newTargetPos = target ? target.position : (GameObject.FindWithTag("Player") ? GameObject.FindWithTag("Player").transform.position : Vector3.zero);
        newPosDiff = newPosDiff != Vector3.zero ? newPosDiff : newTargetPos - Camera.main.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<CameraTarget>()) 
        {
            // if non-player target position is not set, the player's current position will be set for the new target position
            camMovement.SetNewMovement(newTargetPos, newPosDiff, newDirections, newRotation);
        }
    }
}
