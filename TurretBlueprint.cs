using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBlueprint //monobehavior를 없애야 여기저기 적용 가능
{
    //여기서 Dicing 랜덤뽑기 기능을 추가해야 함
    public GameObject prefab;
    public GameObject dummy; //turret prefab 마다 넣어줘야 함
    public Sprite prefabimage;
    
    //public int cost = 100;    

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
