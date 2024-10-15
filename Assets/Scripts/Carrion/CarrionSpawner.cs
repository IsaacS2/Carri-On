using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carrionPrefab;
    [SerializeField] private Transform carrionTransform;

    private int carrionNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && carrionPrefab)
        {
            GameObject newCarrion = Instantiate(carrionPrefab, carrionTransform.position + carrionPrefab.transform.position, Quaternion.identity);
            Carrion newCarrionScript = newCarrion.GetComponent<Carrion>();

            if (newCarrionScript)
            {
                newCarrionScript.SetCarrionNum(carrionNum);
                //Debug.Log("Script Collected from carrion number " + newCarrionScript.GetCarrionNum());
            }
        }

        // Disable spawner since the carrion should only be spawned once
        gameObject.SetActive(false);
    }

    public void SetCarrionNum(int _carrionNum)
    {
        carrionNum = _carrionNum;
    }

    public int GetCarrionNum()
    {
        return carrionNum;
    }
}
