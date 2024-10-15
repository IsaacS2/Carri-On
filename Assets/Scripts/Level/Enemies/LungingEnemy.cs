using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungingEnemy : JumpingEnemy
{
    [SerializeField] private GameObject model;
    private bool turnEnemy;

    private void FixedUpdate()
    {
        if (_rb.velocity.y < 0 && !turnEnemy && currentState == AnimalStates.Airborne)
        {
            turnEnemy = true;

            if (model)
            {
                model.transform.localEulerAngles =
                    new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, -model.transform.localEulerAngles.z);
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (currentState == AnimalStates.Grounded && turnEnemy)
        {
            jumpDirection.x *= -1;  // switch jumping direction
            if (model) {
                model.transform.localEulerAngles =
                    new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + 180, -model.transform.localEulerAngles.z); 
            }
            turnEnemy = false;
        }
    }
}
