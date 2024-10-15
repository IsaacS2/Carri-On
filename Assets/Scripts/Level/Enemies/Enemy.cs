using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float attackingTimeLimit;

    protected AnimalStates currentState;
    protected float attackingTimer;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = AnimalStates.Attacking;
        }
    }
}
