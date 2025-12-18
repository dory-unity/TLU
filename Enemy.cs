using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings; 

public class Enemy : MonoBehaviour
{
    public float startSpeed = 1f;
    [HideInInspector] //speed는 Inspector에서 안보임
    public float speed;
    public float startHealth = 400f; //health bar를 적용하기 위해서는 float
    public float health;
    public int value = 10;
    public GameObject deathEffect;
    private GameObject bossHBGO; // boss healthbar를 이용하기 위함
    private Text BossHealthText = null; // boss healthbar를 이용하기 위함
    private bool BossUp = false;
    private Transform ui;
    private GameObject victoryUI;  
    private float bossGenTime;
    private float bossKillTime;
    public int bossStage;    
    public GameObject hudDamageText;
    public Transform hudPos;
    private float game01time;
    private float game02time;
    private float game03time;
    private float game04time;
    private float game05time;
    private float game06time;
    private float game07time;
    private float game08time;
    private float game09time;
    private float game10time;    
    private float easygame01time;
    private float easygame02time;
    private float easygame03time;
    private float easygame04time;
    private float easygame05time;
    private float easygame06time;
    private float easygame07time;
    private float easygame08time;
    private float easygame09time;
    private float easygame10time;
    private Text moneyText;
    public Text bossTime;

    
    [Header("Unity Stuff")]
    public Image healthBar; //보스는 스스로의 health bar가 없고, 바깥에서 이용
    
    void Start() 
    {        
        BossHealthControl();
        speed = startSpeed;   
        health = startHealth;     
        ui = GameObject.Find("PopUpCanvas").transform.Find("VictoryUI");
        bossTime = GameObject.Find("PopUpCanvas").transform.Find("VictoryUI").transform.Find("RecordText").GetComponent<Text>();
        moneyText = GameObject.Find("Canvas").transform.Find("TopPanel").transform.Find("MoneyImage").transform.Find("MoneyText").gameObject.GetComponent<Text>();
        victoryUI = ui.gameObject;        
    }


    public void TakeDamage(float amount)
    {   
        if(GameManager.GameIsOver == true)
        {
            return;
        }
        
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        float dice = Random.Range(-0.2f,0.2f); //hudPos 좌우로 랜덤하게 등장
        Vector3 hudPosOffset = new Vector3(dice, 0,0);
        GameObject hudText = Instantiate(hudDamageText); //damage 띄우기
        hudText.transform.position = hudPos.position+hudPosOffset; //damage 띄우기        
        hudText.GetComponent<DamageText>().damage = amount; //damage 띄우기

        if (health <= 0)
        {
            health = 0;
            BossDie();
            Die();
        }

        if (BossHealthText != null)
        {
            BossHealthText.text = health.ToString("F0");
        }
    }

    void Die()
    {        
        PlayerStats.Money += value;
        moneyText.text = PlayerStats.Money.ToString();
        if(deathEffect != null)
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect,1f); 
        }        
        Destroy(gameObject);
    }
    
    
    public void Slow(float pct) //0.3만큼 slow 시키면, speed는 0.7이 됨
    {
        speed = startSpeed * (1f - pct);
        Invoke("SlowEnd", 3f);
    }

    private void SlowEnd()
    {
        speed = startSpeed;
    }

    private void BossHealthControl() //보스 체력은 보스 체력바로 관리
    {
        if (healthBar == null)
        {
            BossUp = true; //보스 등장
            bossGenTime = Time.time;
            bossHBGO = GameObject.Find("Canvas").transform.Find("BossHealth").transform.Find("BossHBGO").gameObject;
            bossHBGO.SetActive(true);
            healthBar = GameObject.Find("Canvas").transform.Find("BossHealth").transform.Find("BossHBGO").transform.Find("BossHealthBar").GetComponent<Image>();
            BossHealthText = GameObject.Find("Canvas").transform.Find("BossHealth").transform.Find("BossHBGO").transform.Find("HealthText").GetComponent<Text>();
            BossHealthText.text = startHealth.ToString();
            Debug.Log("Health Bar Found");            
        }
    }

    void BossDie()
    {
        if(BossUp == true)
        {            
            BestTimeLoad();
            BestTimeCheck();
            Debug.Log("Boss Death");
            victoryUI.SetActive(true);
            bossTime.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringTable01","record") + bossKillTime.ToString("F2");
            BossUp = false;
            Time.timeScale = 1.0f;
            GameManager.GameIsOver = true;
        }   
    }

    void BestTimeCheck()
    {        
        bossKillTime = Time.time - bossGenTime;
        var settings = new ES3Settings(ES3.EncryptionType.AES, "dragonhorse12");
        int easystage = PlayerPrefs.GetInt("easystage",0);
        if(bossStage == 1)
        {
            if (bossKillTime <= game01time)
            {     
                ES3.Save<float>("2302game01time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }                 
        }
        else if(bossStage == 2)
        {
            if (bossKillTime <= game02time)
            {
                ES3.Save<float>("2302game02time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 3)
        {
            if (bossKillTime <= game03time)
            {       
                ES3.Save<float>("2302game03time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 4)
        {
            if (bossKillTime <= game04time)
            {       
                ES3.Save<float>("2302game04time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 5)
        {
            if (bossKillTime <= game05time)
            {       
                ES3.Save<float>("2302game05time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 6)
        {
            if (bossKillTime <= game06time)
            {       
                ES3.Save<float>("2302game06time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 7)
        {
            if (bossKillTime <= game07time)
            {       
                ES3.Save<float>("2302game07time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 8)
        {
            if (bossKillTime <= game08time)
            {       
                ES3.Save<float>("2302game08time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 9)
        {
            if (bossKillTime <= game09time)
            {       
                ES3.Save<float>("2302game09time",bossKillTime,settings);                
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }
        else if(bossStage == 10)
        {
            if (bossKillTime <= game10time)
            {  
                ES3.Save<float>("2302game10time",bossKillTime,settings);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
        }

        /* //Easy Stage 기록 삭제
        else if(bossStage == 11)
        {
            if (bossKillTime <= easygame01time)
            {  
                ES3.Save<float>("easygame01time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }
        }
        else if(bossStage == 12)
        {
            if (bossKillTime <= easygame02time)
            {  
                ES3.Save<float>("easygame02time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }            
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }
        }
        else if(bossStage == 13)
        {
            if (bossKillTime <= easygame03time)
            {  
                ES3.Save<float>("easygame03time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }        
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }    
        }
        else if(bossStage == 14)
        {
            if (bossKillTime <= easygame04time)
            {  
                ES3.Save<float>("easygame04time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }   
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }         
        }
        else if(bossStage == 15)
        {
            if (bossKillTime <= easygame05time)
            {  
                ES3.Save<float>("easygame05time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }      
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }      
        }
        else if(bossStage == 16)
        {
            if (bossKillTime <= easygame06time)
            {  
                ES3.Save<float>("easygame06time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }   
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }         
        }
        else if(bossStage == 17)
        {
            if (bossKillTime <= easygame07time)
            {  
                ES3.Save<float>("easygame07time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }     
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }       
        }
        else if(bossStage == 18)
        {
            if (bossKillTime <= easygame08time)
            {  
                ES3.Save<float>("easygame08time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }   
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }         
        }
        else if(bossStage == 19)
        {
            if (bossKillTime <= easygame09time)
            {  
                ES3.Save<float>("easygame09time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }    
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }        
        }
        else if(bossStage == 20)
        {
            if (bossKillTime <= easygame10time)
            {  
                ES3.Save<float>("easygame10time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }      
            if (easystage < (bossStage - 10))
            {
                PlayerPrefs.SetInt("easystage",(bossStage - 10));
            }      
        }
        else if(bossStage == 21)
        {
            if (bossKillTime <= easygame01time)
            {  
                ES3.Save<float>("easygame01_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }   
        }
        else if(bossStage == 22)
        {
            if (bossKillTime <= easygame02time)
            {  
                ES3.Save<float>("easygame02_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }  
        }
        else if(bossStage == 23)
        {
            if (bossKillTime <= easygame03time)
            {  
                ES3.Save<float>("easygame03_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }  
        }
        else if(bossStage == 24)
        {
            if (bossKillTime <= easygame04time)
            {  
                ES3.Save<float>("easygame04_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }         
        }
        else if(bossStage == 25)
        {
            if (bossKillTime <= easygame05time)
            {  
                ES3.Save<float>("easygame05_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }     
        }
        else if(bossStage == 26)
        {
            if (bossKillTime <= easygame06time)
            {  
                ES3.Save<float>("easygame06_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }        
        }
        else if(bossStage == 27)
        {
            if (bossKillTime <= easygame07time)
            {  
                ES3.Save<float>("easygame07_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }      
        }
        else if(bossStage == 28)
        {
            if (bossKillTime <= easygame08time)
            {  
                ES3.Save<float>("easygame08_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }         
        }
        else if(bossStage == 29)
        {
            if (bossKillTime <= easygame09time)
            {  
                ES3.Save<float>("easygame09_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }       
        }
        else if(bossStage == 30)
        {
            if (bossKillTime <= easygame10time)
            {  
                ES3.Save<float>("easygame10_1time",bossKillTime);                     
                Debug.Log("New Record : "+bossKillTime.ToString("F1"));
            }     
        }
        */
    }

    void BestTimeLoad()
    {
        var settings = new ES3Settings(ES3.EncryptionType.AES, "dragonhorse12");
        game01time = ES3.Load<float>("2302game01time",60,settings);
        game02time = ES3.Load<float>("2302game02time",60,settings);
        game03time = ES3.Load<float>("2302game03time",60,settings);
        game04time = ES3.Load<float>("2302game04time",60,settings);
        game05time = ES3.Load<float>("2302game05time",60,settings);
        game06time = ES3.Load<float>("2302game06time",60,settings);
        game07time = ES3.Load<float>("2302game07time",60,settings);
        game08time = ES3.Load<float>("2302game08time",60,settings);
        game09time = ES3.Load<float>("2302game09time",60,settings);
        game10time = ES3.Load<float>("2302game10time",60,settings);
        /*
        easygame01time = ES3.Load<float>("easygame01time",60);
        easygame02time = ES3.Load<float>("easygame02time",60);
        easygame03time = ES3.Load<float>("easygame03time",60);
        easygame04time = ES3.Load<float>("easygame04time",60);
        easygame05time = ES3.Load<float>("easygame05time",60);
        easygame06time = ES3.Load<float>("easygame06time",60);
        easygame07time = ES3.Load<float>("easygame07time",60);
        easygame08time = ES3.Load<float>("easygame08time",60);
        easygame09time = ES3.Load<float>("easygame09time",60);
        easygame10time = ES3.Load<float>("easygame10time",60);        
        easygame01time = ES3.Load<float>("easygame01_1time",60);
        easygame02time = ES3.Load<float>("easygame02_1time",60);
        easygame03time = ES3.Load<float>("easygame03_1time",60);
        easygame04time = ES3.Load<float>("easygame04_1time",60);
        easygame05time = ES3.Load<float>("easygame05_1time",60);
        easygame06time = ES3.Load<float>("easygame06_1time",60);
        easygame07time = ES3.Load<float>("easygame07_1time",60);
        easygame08time = ES3.Load<float>("easygame08_1time",60);
        easygame09time = ES3.Load<float>("easygame09_1time",60);
        easygame10time = ES3.Load<float>("easygame10_1time",60);
        */
    }

}
