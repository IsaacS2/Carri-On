using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class CarrionSpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UITimer;
    [SerializeField] public GameObject livePrefab, cutsceneBlocker, predatorPrefab;
    [SerializeField] public Transform carrionTransform, predatorTransform;
    [SerializeField] private float carrionTimeLimit, cutsceneTime = 0;

    public GameObject newCarrion;
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

            if (cutsceneBlocker.activeSelf && carrionTimer <= carrionTimeLimit)  // cutscene done
            {
                cutsceneBlocker.SetActive(false);
                UITimer.enabled = true;
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
        if (other.gameObject.CompareTag("Player") && livePrefab)
        {
            // create predator
            Instantiate(predatorPrefab, predatorTransform.position + carrionTransform.position, predatorPrefab.transform.rotation);

            // Disable spawner collider since the carrion should only be spawned once
            GetComponent<Collider>().enabled = false;
            carrionTimer = carrionTimeLimit + cutsceneTime;
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
