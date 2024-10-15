using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private int checkpointNum;

    [SerializeField] private Transform newTransform;

    public void SetCheckpointNum(int _num)
    {
        checkpointNum = _num;
    }

    public int GetCheckpointNum()
    {
        return checkpointNum;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.NewCheckpointHit(checkpointNum, newTransform.position);
            Debug.Log("checkpoint!");
        }
    }
}
