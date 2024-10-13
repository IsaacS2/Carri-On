using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 directions;
    [SerializeField] private float moveRate;

    private Vector3 positionDifference, startingPosition;
    private Vector3 eulerRotation;

    private void Start()
    {
        if (target)
        {
            startingPosition = transform.position;
            startingPosition.x *= (1 - directions.x);
            startingPosition.y *= (1 - directions.y);
            startingPosition.z *= (1 - directions.z);

            positionDifference = target.position - transform.position;
        }
    }

    void Update()
    {
        Vector3 newPos = target.position - positionDifference;
        newPos.x *= directions.x;
        newPos.y *= directions.y;
        newPos.z *= directions.z;

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, newPos + startingPosition, Time.deltaTime * moveRate);
        }
    }

    public void SetNewStartingPos(Vector3 _startPos)
    {
        startingPosition = _startPos;
    }
}
