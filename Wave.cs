using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave //여기저기 가져다 쓰기, TurretBluePrint와 같다. monobehavior가 아님
{
    
    public GameObject enemy;
    public string enemyname;
    public int count;
    public float rate;    
}
