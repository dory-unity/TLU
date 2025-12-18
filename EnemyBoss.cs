using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private float bosshealth;
    private Transform ui;
    private GameObject victoryUI;    
    void Start()
    {        
        bosshealth = gameObject.GetComponent<Enemy>().health;
        ui = GameObject.Find("Canvas").transform.Find("VictoryUI");
        victoryUI = ui.gameObject;
    }

    
    void Update()
    {        
        if (bosshealth > 0)
        {
            Debug.Log(bosshealth);
        }
        if (bosshealth <= 0)
        {
            Debug.Log("Boss Death");
            victoryUI.SetActive(true);
            Time.timeScale = 0;
        }
        if(gameObject == null)
        {
            Debug.Log("GO null");
            victoryUI.SetActive(true);
            Time.timeScale = 0;            
        }
    }
}
