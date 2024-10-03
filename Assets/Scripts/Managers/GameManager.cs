using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numLevelSegments;

    public static GameManager Instance;

    private GameObject player;
    private Vector3 currentCheckpointPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;

            DontDestroyOnLoad(Instance.gameObject);
        }
    }
}