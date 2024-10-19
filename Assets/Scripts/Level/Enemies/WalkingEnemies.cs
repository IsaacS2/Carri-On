using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemies : Enemy
{
    [SerializeField] private Transform[] walkingPositions;
    [SerializeField] private float walkingSpeed, targetDiff;  // distance the enemy must be to switch to new target

    private Vector3 currentTargetPos;
    private int currentTargetIndex;

    private void OnEnable()
    {
        currentState = AnimalStates.Grounded;
        currentTargetPos = walkingPositions.Length > 0 ? walkingPositions[0].position : Vector3.zero;
        currentTargetIndex = 0;
        foreach (Transform Pos in walkingPositions)  // break apart transforms so they don't move with the parent
        {
            Pos.parent = null;
        }
    }

    private void Update()
    {
        if (currentState == AnimalStates.Grounded) {
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, walkingSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentTargetPos) < targetDiff)
            {
                if (currentTargetIndex + 1 >= walkingPositions.Length)  // go to first target position and repeat
                {
                    currentTargetIndex = 0;
                }
                else
                {
                    currentTargetIndex++;
                }

                currentTargetPos = walkingPositions[currentTargetIndex].position;

                if (model)
                {
                    model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x,
                        model.transform.localEulerAngles.y + 180, model.transform.localEulerAngles.z);
                }
            }
        }
        else if (currentState == AnimalStates.Attacking)
        {
            attackingTimer += Time.deltaTime;

            if (attackingTimer >= attackingTimeLimit)
            {
                currentState = AnimalStates.Grounded;
                attackingTimer = 0;
            }
        }
    }
}
