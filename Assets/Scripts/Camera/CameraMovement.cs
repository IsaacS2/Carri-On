using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 directions;
    [SerializeField] private float moveRate;

    private Vector3 positionDifference, alteredStartingPosition, unalteredStartingPos, eulerRotation;
    private bool resetCamera;

    private void Start()
    {
        eulerRotation = transform.localEulerAngles;

        if (target)
        {
            alteredStartingPosition = transform.position;

            // negating any directional values that being tracked with the target (to keep the camera at the same position relative to the target)
            alteredStartingPosition.x *= (1 - directions.x);
            alteredStartingPosition.y *= (1 - directions.y);
            alteredStartingPosition.z *= (1 - directions.z);

            positionDifference = target.position - transform.position;
        }
    }

    void Update()
    {
        MoveCamera(Time.deltaTime * moveRate);
    }

    public void SetNewMovement(Vector3 _targetPos, Vector3 _posDiff, Vector3 _direction, Vector3 _rotation)
    {
        // set new target position, position difference between target and camera,
        // camera following direction, and camera rotation
        directions = _direction;
        positionDifference = _posDiff != Vector3.zero ? _posDiff : positionDifference;
        unalteredStartingPos = _targetPos - positionDifference;
        eulerRotation = _rotation;

        alteredStartingPosition = unalteredStartingPos;
        alteredStartingPosition.x *= (1 - directions.x);
        alteredStartingPosition.y *= (1 - directions.y);
        alteredStartingPosition.z *= (1 - directions.z);

        Vector3 newPos = target.position - positionDifference;

        // negating any directional values that aren't being tracked (to avoid unneeded target position values)
        newPos.x *= directions.x;
        newPos.y *= directions.y;
        newPos.z *= directions.z;

        if (!resetCamera)
        {
            // move and rotate camera
            transform.position = Vector3.Lerp(transform.position, newPos + alteredStartingPosition, 1f);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, eulerRotation, 1f);
            resetCamera = true;
        }
    }

    public Vector3 GetStartPos()
    {
        return unalteredStartingPos;
    }

    public Vector3 GetDirections()
    {
        return directions;
    }

    public Vector3 GetRotation()
    {
        return eulerRotation;
    }

    private void MoveCamera(float _moveRate)
    {
        Vector3 newPos = target.position - positionDifference;

        // negating any directional values that aren't being tracked (to avoid unneeded target position values)
        newPos.x *= directions.x;
        newPos.y *= directions.y;
        newPos.z *= directions.z;

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, newPos + alteredStartingPosition, _moveRate);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, eulerRotation, _moveRate);
        }
    }
}
