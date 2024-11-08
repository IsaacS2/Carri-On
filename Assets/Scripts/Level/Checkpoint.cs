using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private int checkpointNum;

    [SerializeField] private Transform newTransform;
    [SerializeField] private bool ending;

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
            LevelManager.Instance.NewCheckpointHit(checkpointNum, newTransform ? newTransform.position : Vector3.zero);
            
            TempLevelStorage collectedItems = other.gameObject.GetComponent<TempLevelStorage>();

            if (collectedItems)
            {
                //Debug.Log("Collected carrions :" + collectedItems.GetCarrionObtained().Count);
                LevelManager.Instance.AddCarrions(collectedItems.GetCarrionObtained());
                collectedItems.ClearCarrions();
            }

            if (ending)
            {
                LevelManager.Instance.LevelComplete();
            }
        }
    }
}
