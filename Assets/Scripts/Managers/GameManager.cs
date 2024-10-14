using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalLevels;

    private int[] collectedCarrions;
    public static GameManager Instance;

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

        if (totalLevels > 0)
        {
            collectedCarrions = new int[totalLevels];
        }
        else
        {
            collectedCarrions = new int[1];
        }
    }

    public void OnLevelComplete(int _levelIndex, int _carrionCollected)
    {

    }
}