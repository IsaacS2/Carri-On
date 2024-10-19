using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] checkpoints, carrionSpawners;
    [SerializeField] private int currentLevelIndex;

    public static LevelManager Instance;

    private bool[] checkpointsDisabled, carrionSpawnersDisabled;  // keep track of enabled/disabled checkpoints
    private GameObject player;
    private Vector3 currentCheckpointPosition;
    private int carrionCollected, carrionTotal;
    
    private void Awake()
    {
        CreateNewLevelInstance();
    }

    private void OnEnable()
    {
        Instance?.StartLevel();

        //Instance.ResetCheckpoints();
        //Instance.ResetCarrionSpawners();
        //Debug.Log("Level Manager Enabled Called");

        SceneManager.sceneLoaded += Instance.OnSceneLoaded;
    }

    void OnDisable()
    {
        //Debug.Log("OnDisable");
        if (Instance == this) {
            //Debug.Log("OnDisable2");
            SceneManager.sceneLoaded -= Instance.OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Instance.RepositionPlayer();
        Instance.ResetCheckpoints();
        Instance.ResetCarrionSpawners();
    }

    private void CreateNewLevelInstance(int _levelIndex = -1)
    {
        
        if (Instance != null && Instance != this)
        {
            //Debug.Log("currentCheckpointPosition before: " + Instance.currentCheckpointPosition);
            //Debug.Log("Intruder level manager object");
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            //Debug.Log("currentCheckpointPosition before: " + Instance.currentCheckpointPosition);
            DontDestroyOnLoad(Instance.gameObject);

            currentLevelIndex = _levelIndex <= -1 ? currentLevelIndex : _levelIndex;

            // set checkpoint index order in-level based on checkpoints array ordering
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].GetComponent<Checkpoint>())
                {
                    checkpoints[i].GetComponent<Checkpoint>().SetCheckpointNum(i);
                }
            }

            checkpointsDisabled = new bool[checkpoints.Length];

            // find all carrion spawners in the level and set index number for them
            for (int i = 0; i < carrionSpawners.Length; i++)
            {
                if (carrionSpawners[i].GetComponent<CarrionSpawner>())
                {
                    carrionSpawners[i].GetComponent<CarrionSpawner>().SetCarrionNum(i);
                }
            }

            carrionSpawnersDisabled = new bool[carrionSpawners.Length];

            player = GameObject.FindWithTag("Player");
            if (player && player.GetComponent<Rigidbody>()) {
                //Debug.Log("currentCheckpoint shouldn't be messed with here!");
                currentCheckpointPosition = player.GetComponent<Rigidbody>().position; 
            }

            //Debug.Log("Level Manager Awake Called");
        }

        //Debug.Log("currentCheckpointPosition after: " + Instance.currentCheckpointPosition);
    }

    public void LevelComplete()
    {
        GameManager.Instance.OnLevelComplete(currentLevelIndex, carrionCollected, carrionTotal);
    }

    public void StartLevel()
    {
        carrionCollected = 0;

        carrionTotal = carrionSpawners.Length;
    }

    public void EndLevel()
    {
        if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    public void NewCheckpointHit(int _checkpointNum, Vector3 _newPosition)
    {
        // Disable checkpoints and keep them disabled if scene is reloaded
        Instance.checkpoints[_checkpointNum].SetActive(false);
        Instance.checkpointsDisabled[_checkpointNum] = true;
        Instance.currentCheckpointPosition = _newPosition;
        //Debug.Log("New checkpoint activated: " + Instance.currentCheckpointPosition);
    }

    public void AddCarrions(List<int> _carrionNums)
    {
        // save each carrion collected and store it in level data
        foreach (int num in _carrionNums)
        {
            Debug.Log("Adding carrrion from spawner number: " + num);
            Instance.carrionSpawners[num].SetActive(false);
            Instance.carrionSpawnersDisabled[num] = true;
            Instance.carrionCollected += 1;
        }
    }

    public void AssignCarrion(int _carrionNum, GameObject _carrion)
    {
        Instance.carrionSpawners[_carrionNum].GetComponent<CarrionSpawner>().newCarrion = _carrion;
    }

    private void RepositionPlayer()
    {
        player = GameObject.FindWithTag("Player");

        if (player && player.GetComponent<Rigidbody>()) {
            //Debug.Log("Player found! Repositioning to..." + Instance.currentCheckpointPosition);
            player.GetComponent<Rigidbody>().position = Instance.currentCheckpointPosition;
        }
    }

    private void ResetCheckpoints()
    {
        // deactivate any checkpoints that were alrady activated beforehand
        for (int i = 0; i < Instance.checkpoints.Length; i++)
        {
            Instance.checkpoints[i].SetActive(!Instance.checkpointsDisabled[i]);
        }
    }

    private void ResetCarrionSpawners()
    {
        // deactivate any checkpoints that were alrady activated beforehand
        for (int i = 0; i < Instance.carrionSpawners.Length; i++)
        {
            Instance.carrionSpawners[i].GetComponent<Collider>().enabled = true;
            Debug.Log("Resetting carrrion spawner number: " + i);
            Instance.carrionSpawners[i].SetActive(!Instance.carrionSpawnersDisabled[i]);
            if (Instance.carrionSpawners[i].activeSelf)
            {
                CarrionSpawner script = Instance.carrionSpawners[i].GetComponent<CarrionSpawner>();
                script.cutsceneBlocker.SetActive(true);

                GameObject prey = Instantiate(script.livePrefab, script.carrionTransform.position + script.livePrefab.transform.position, script.livePrefab.transform.rotation);

                PreCarrion newPreyScript = prey.GetComponent<PreCarrion>();

                if (newPreyScript)
                {
                    newPreyScript.SetCarrionNum(i);
                }
            }
        }
    }
}
