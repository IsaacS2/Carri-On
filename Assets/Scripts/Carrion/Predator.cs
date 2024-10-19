using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    [SerializeField] private float TimeLimit, speed;

    private float timer;
    
    void Start()
    {
        timer = TimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
