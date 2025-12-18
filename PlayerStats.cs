using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{    
    public static int Money; //Money가 계속 유지되도록 함    
    public int startMoney = 350;    
    public static int enemycount;
    public static int warriorcount;
    public static int wizardcount;
    public static int archercount;    
    public int Rounds;
    private Text moneyText;
    GameObject[] enemies;
    GameObject[] warriors;
    GameObject[] wizards;
    GameObject[] archers;
    
    public int startint = 0;
    public Text gameDiaText;
    void Start()
    {   
        PlayerPrefs.SetInt("gamedia",1);
        gameDiaText.text = PlayerPrefs.GetInt("gamedia").ToString();
        moneyText = GameObject.Find("Canvas").transform.Find("TopPanel").transform.Find("MoneyImage").transform.Find("MoneyText").gameObject.GetComponent<Text>();
        Money = startMoney;
        moneyText.text = PlayerStats.Money.ToString();
        Rounds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        warriors = GameObject.FindGameObjectsWithTag("Warrior");
        wizards = GameObject.FindGameObjectsWithTag("Wizard");
        archers = GameObject.FindGameObjectsWithTag("Archer");
        enemycount = enemies.Length;
        warriorcount = warriors.Length;
        wizardcount = wizards.Length;
        archercount = archers.Length;
        Rounds = WaveSpawner.waveIndex;
    }
}
