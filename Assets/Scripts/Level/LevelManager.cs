using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevelIndex;

    public static LevelManager Instance;

    private GameObject[] checkpoints;
    private GameObject player;
    private Vector3 currentCheckpointPosition;
    private int carrionCollected, carrionTotal;
    
    private void Awake()
    {
        CreateNewLevelInstance();
    }

    private void CreateNewLevelInstance(int _levelIndex = -1)
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

        currentLevelIndex = _levelIndex <= -1 ? currentLevelIndex : _levelIndex;
    }

    private void OnEnable()
    {
        Instance?.StartLevel();

        // set checkpoint index order in-level based on checkpoints array ordering
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].GetComponent<Checkpoint>())
            {
                checkpoints[i].GetComponent<Checkpoint>().SetCheckpointNum(i);
            }
        }
    }

    private void LevelComplete()
    {
        GameManager.Instance.OnLevelComplete(currentLevelIndex, carrionCollected);
    }

    public void StartLevel()
    {
        carrionCollected = 0;

        carrionTotal = GameObject.FindGameObjectsWithTag("Carrion").Length;
    }

    public void EndLevel()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
