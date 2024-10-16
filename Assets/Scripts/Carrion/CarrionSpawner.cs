using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class CarrionSpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UITimer;
    [SerializeField] private GameObject carrionPrefab;
    [SerializeField] private Transform carrionTransform;
    [SerializeField] private float carrionTimeLimit;

    private GameObject newCarrion;
    private string originalText;
    private float carrionTimer;
    private int carrionNum;

    private void Update()
    {
        if (carrionTimer > 0)
        {
            carrionTimer -= Time.deltaTime;

            if (carrionTimer <= 0 && newCarrion)  // didn't grab carrion in time :(
            {
                carrionTimer = 0;
                Destroy(newCarrion);
            }
            UITimer.text = originalText + carrionTimer.ToString("#.00");  // display time left
        }

        // disable timer when carrion is collected or destroyed
        if (newCarrion == null && UITimer.enabled)
        {
            UITimer.enabled = false;
        }
    }

    private void OnEnable()
    {
        carrionTimer = 0;
        originalText = UITimer.text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && carrionPrefab)
        {
            newCarrion = Instantiate(carrionPrefab, carrionTransform.position + carrionPrefab.transform.position, Quaternion.identity);
            Carrion newCarrionScript = newCarrion.GetComponent<Carrion>();

            if (newCarrionScript)
            {
                newCarrionScript.SetCarrionNum(carrionNum);
                //Debug.Log("Script Collected from carrion number " + newCarrionScript.GetCarrionNum());
            }

            // Disable spawner collider since the carrion should only be spawned once
            GetComponent<Collider>().enabled = false;
            carrionTimer = carrionTimeLimit;
            UITimer.enabled = true;
        }
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
