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
        if (vultAnim)
        {
            vultAnim.SetTrig("Dead");
        }
    }

    private void OnDisable()
    {
        currentDeathTime = deathTimer;
    }

    protected override void Start()
    {
        base.Start();

        if (vultAnim)
        {
            vultAnim.SetTrig("Dead");
        }
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
