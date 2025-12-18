using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountUI : MonoBehaviour
{
    public Text enemycountText;    

    void Start()
    {
             
    }

    
    void Update()
    {
        enemycountText.text = PlayerStats.enemycount.ToString();
        EnemyCountColor();       
        

    }

    void EnemyCountColor()
    {
        if(PlayerStats.enemycount >= 25 && PlayerStats.enemycount < 40)
        {
            enemycountText.color = Color.yellow;
        }        
        else if(PlayerStats.enemycount >= 40)
        {
            enemycountText.color = Color.red;
        }    
        else
        {
            enemycountText.color = Color.white;
        }    
    }
}
