using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreCarrion : MonoBehaviour
{
    [SerializeField] private GameObject deadAnimal;
    private int carrionNum;

    public void SetCarrionNum(int _num)
    {
        carrionNum = _num;
    }

    public int GetCarrionNum()
    {
        return carrionNum;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.GetComponent<Predator>())  // create dead animal
        {
            GameObject animal = Instantiate(deadAnimal, transform.position, deadAnimal.transform.rotation);

            Carrion carrion = animal.GetComponent<Carrion>();
            if (carrion)
            {
                carrion.SetCarrionNum(carrionNum);
            }

            Destroy(gameObject);
        }
    }
}
