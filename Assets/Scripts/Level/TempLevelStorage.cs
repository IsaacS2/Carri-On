using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLevelStorage : MonoBehaviour
{
    private List<int> carrionObtained;

    private void Awake()
    {
        carrionObtained = new List<int>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Carrion deadAnimal = collision.gameObject.GetComponent<Carrion>();

        if (deadAnimal)
        {
            carrionObtained.Add(deadAnimal.GetCarrionNum());

            // destroy collected carrion
            Destroy(collision.gameObject);
        }
    }

    public List<int> GetCarrionObtained()
    {
        return carrionObtained;
    }

    public void ClearCarrions()
    {
        carrionObtained.Clear();
    }
}
