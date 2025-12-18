using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using DG.Tweening;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPoint;
    private Vector3 spawnOffset; //spawnpoint 보다 뒤에서 enemy를 생성하게 하기 위함

    public float countdown = 5f; //최초의 카운트다운, 이후 언제 next wave를 내보낼 지

    public static int waveIndex = 0;
    
    public Text waveCountdownText;    
    public Text waveText;
    public Text enemyNameText;
//    public Text stageText;
    private float roundtime = 65f;
    public GameObject gameOverUI;       
    public GameObject mainCamera; 
    public Image redPanel;
    private int bossUp = 0;
    private GameObject BossWarningFrame;
    private GameObject WavePanels;
    private GameObject NodeUI;
    private GameObject WarningUI;

    void Awake()
    {
        NodeUI = Instantiate(Resources.Load("NodeUI", typeof(GameObject))) as GameObject;        //NodeUI를 불러온다.
        WarningUI = Instantiate(Resources.Load("WarningUI", typeof(GameObject))) as GameObject;        //WarningUI를 불러온다.
        WavePanels = Instantiate(Resources.Load("WavePanels", typeof(GameObject))) as GameObject;
        WavePanels.transform.SetParent(GameObject.Find("PopUpCanvas").transform);//WavePanels가 PopUpCanvas의 Child로 들어간다
        WavePanels.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        WavePanels.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        
        if(WavePanels)
        {
            Debug.Log("WavePanels is Ready");
        }

    }

    void Start()
    {
        waveIndex = -1;   
        bossUp = 0;
        spawnOffset = new Vector3(0,0,0.01f);        
        BossWarningFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("BossWarningFrame").gameObject;
        
       
        /*if(FindObjectOfType<WavePanelDummy>() != null)
        {
            //WavePanels = GameObject.Find("PopUpCanvas").transform.Find("WavePanels").gameObject;
            WavePanels = (GameObject)Resources.Load("WavePanels");
            Debug.Log("WavePanels Ready");
        }*/
        
    }

    void Update()
    {        
        if(PlayerPrefs.GetInt("skill03")==1)
        {
            return;
        }
        if(GameManager.GameIsOver) 
        {
            Time.timeScale = 1f;
            Debug.Log("Game is Over, no Spawn Update");
            return;
        }

        waveText.text = "Wave " + (waveIndex+1).ToString();
        if(roundtime <= 0f)
        {
            Debug.Log("wave.length : " + waves.Length);
            if(waveIndex+1 == waves.Length)
            {
                EndGame();
                Debug.Log("Game is Over");                
                return;
            }            
            if(WavePanels && waveIndex == (waves.Length-3))  //wavePanel이 있고, wave4에서 보너스 판넬 발동
            {
                Debug.Log("Bonus Panel Up");
                bonusPanelUp();            
            }
            if(bossUp == 0 && waveIndex == (waves.Length-2))  //boss alarm 작동
            {
                bossAlarm();
                bossUp = 1;                
            }
            roundtime = 65f;
            countdown = 5f;            
        }
        if(countdown <= 0f)
        {               
            waveIndex++;            
            StartCoroutine(SpawnWave()); //IEnumerator니까 StartCoroutine 으로 실행
            countdown = roundtime;                      
        }

        countdown -= Time.deltaTime;
        roundtime -= Time.deltaTime;

        waveCountdownText.text = string.Format("{0:00}",countdown);               
    }

    IEnumerator SpawnWave() 
    {
        
        Wave wave = waves[waveIndex];
        Debug.Log("SpawnWave, waveIndex : "+waveIndex);
        enemyNameText.text = waves[waveIndex].enemyname;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1/wave.rate);
        }
        //waveIndex++;//매 번 enemy 숫자를 하나씩 늘린다

        if (waveIndex == waves.Length)
        {
            yield return new WaitUntil(() => PlayerStats.enemycount == 0);
            SpawnBoss();
            this.enabled = false; //Boss를 생성하고 SpawnWave 종료
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position+spawnOffset,spawnPoint.rotation);
    }

    void SpawnBoss()
    {
        Debug.Log("Level Won!");              
    }

    public void EndGame()
    {
        GameManager.GameIsOver = true;        
        gameOverUI.SetActive(true);
    }

    void bossAlarm()
    {
        BossWarningFrame.SetActive(true);
        if(FindObjectOfType<AudioManager>())
        {
            FindObjectOfType<AudioManager>().Play("bossup");
        }        
        playShake();
        panelRed();
        Invoke("panelWhite",0.75f);
        Invoke("panelRed",1.6f);
        Invoke("panelWhite",2.35f);
        Invoke("playShake",1.6f);       
    }

    void playShake()
    {
        mainCamera.GetComponent<MMCameraOrthographicSizeShaker>().Play();
    }

    void panelRed()
    {        
        redPanel.DOFade(0.4f,1f);      
    }
    void panelWhite()
    {
        redPanel.DOFade(0f,1f);
    }

    void bonusPanelUp()
    {
        int dice = Random.Range(0,6);
        {
            if(dice <= 1)
            {
                WavePanels.transform.Find("WavePanel01").gameObject.SetActive(true);
            }
            else if(dice > 1 && dice <= 2)
            {
                WavePanels.transform.Find("WavePanel02").gameObject.SetActive(true);
            }
            else if(dice > 2 && dice <= 3)
            {
                WavePanels.transform.Find("WavePanel03").gameObject.SetActive(true);
            }
            else if(dice > 3 && dice <= 4)
            {
                WavePanels.transform.Find("WavePanel04").gameObject.SetActive(true);
            }
            else if(dice > 4 && dice <= 5)
            {
                WavePanels.transform.Find("WavePanel05").gameObject.SetActive(true);
            }
            else if(dice > 5 && dice <= 6)
            {
                WavePanels.transform.Find("WavePanel06").gameObject.SetActive(true);
            }
        }
    }
    
}
