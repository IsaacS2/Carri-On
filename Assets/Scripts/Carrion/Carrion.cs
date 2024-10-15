using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrion : MonoBehaviour
{
    private int carrionNum;

    public void SetCarrionNum(int _num)
    {
        carrionNum = _num;
    }

    public int GetCarrionNum()
    {
        return carrionNum;
    }
}
