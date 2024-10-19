using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalLevels;

    private int[] collectedCarrions;
    private int[] carrionTotals;
    private int lastLevel;
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

            if (totalLevels > 0)
            {
                collectedCarrions = new int[totalLevels];
                carrionTotals = new int[totalLevels];
            }
            else
            {
                collectedCarrions = new int[1];
                carrionTotals = new int[1];
            }
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += Instance.OnSceneLoaded;
    }

    void OnDisable()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= Instance.OnSceneLoaded;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject carrionText = GameObject.FindGameObjectWithTag("CarrionCollected");

        if (carrionText && carrionText.GetComponent<TextMeshProUGUI>())
        {
            TextMeshProUGUI carrionTextMesh = carrionText.GetComponent<TextMeshProUGUI>();
            string originalText = carrionTextMesh.text;
            carrionTextMesh.text = originalText + " " + collectedCarrions[lastLevel] + "/" + carrionTotals[lastLevel];
        }
    }

    public void OnLevelComplete(int _levelIndex, int _carrionCollected, int _carrionTotal)
    {
        lastLevel = _levelIndex;
        collectedCarrions[lastLevel] = _carrionCollected;
        carrionTotals[lastLevel] = _carrionTotal;
        LevelManager.Instance.EndLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}