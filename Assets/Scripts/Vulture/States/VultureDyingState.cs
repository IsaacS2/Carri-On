using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VultureDyingState : VultureStateClass
{
    [SerializeField] private float deathTimer = 1;

    private float currentDeathTime;

    private void OnEnable()
    {
        currentDeathTime = 0;
    }

    private void OnDisable()
    {
        currentDeathTime = deathTimer;
    }

    protected override void Update()
    {
        if (currentDeathTime < deathTimer)
        {
            currentDeathTime += Time.deltaTime;

            if (currentDeathTime >= deathTimer)
            {
                // Restart Level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


}
