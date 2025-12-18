using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public string bgm;
    public GameObject gameOverUI;
    
    void Start()
    {
        GameIsOver = false; //시작할때는 false
        if(FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().PlayBGM(bgm);
        }
    }

    
    void Update()
    {       
        if(GameIsOver) 
        {            
            Time.timeScale = 1f;
            return; //game 끝나면 종료
        }        

        if(Input.GetKeyDown("e"))
        {
            EndGame();
        }


        if(PlayerStats.enemycount >= 50)
        {
            EndGame();
        }        
    }

    public void EndGame()
    {
        GameIsOver = true;        
        gameOverUI.SetActive(true);
    }
}
