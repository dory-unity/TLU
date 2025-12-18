using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class PlayerSetup : MonoBehaviour
{
    
    private int zip;
    private int diamond;
    private float damage; //% damage 증가율    
    
    public Text zipText;
    public Text diamondText;    
    
    [Header ("Upgrade UI")]
    //public Button upButton01;
    public Button upButton02;
    public Button upButton03;
    public Button upButton04;
    public Button upButton05;
    public Button upButton06;
    public Button upButton07;
    
    //public Text upWarningText01;
    public Text upWarningText02;
    public Text upWarningText03;
    public Text upWarningText04;
    public Text upWarningText05;
    public Text upWarningText06;
    public Text upWarningText07;

    private GameObject ZipWarningFrame; 

    public static PlayerSetup instance;
    
    
    void Awake() 
    {
        if (instance != null) //혹시라도 PlayerSetup이 두개 있으면 작동하지 않도록
        {
            Debug.LogError("More than one PlayerSetup in scene!");
            return;
        }
        instance = this; //PlayerSetup이 scene 내에서 단 하나만 있게 설정하기 위함        
    }

    
    void Start()
    {         
        if(!ES3.KeyExists("dia"))
        {
            ES3.Save<int>("dia",20); //최초 다이아 설정
        }
        if(!ES3.KeyExists("zip"))
        {
            ES3.Save<int>("zip",5000); //최초 zip 설정
        }
        /*if(!ES3.KeyExists("level"))
        {
            ES3.Save<int>("level",1); //최초 level 설정
        }*/
        
        ZipWarningFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("ZipWarningFrame").gameObject;                
        FindObjectOfType<AudioManager>().PlayBGM("mainbgm1"); //debug 용으로 이용해도 좋음        
        //stageInitialize();                
                
        InvokeRepeating("expSave", 0f, 0.5f);    //0.5초에 한번씩 경험치 저장
        zip = ES3.Load<int>("zip",5000);        
        diamond = ES3.Load<int>("dia",20);        
        zipText.text = zip.ToString();
        diamondText.text = diamond.ToString();
    }
    
    void Update()
    {          
        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            BuyExp();
            Debug.Log("Buy Exp");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetUp();
            levelReset();
            Debug.Log("Reset");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            DiaUp();
            Debug.Log("dia up");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            levelReset();
            Debug.Log("Level Reset");
        }              
        */
        
    }    


    public void ResetUp()
    {  
        PlayerPrefs.DeleteKey("damage");
        PlayerPrefs.DeleteKey("slow"); //업그레이드 초기화
        PlayerPrefs.DeleteKey("critical"); //업그레이드 초기화
        PlayerPrefs.DeleteKey("criticprob"); //업그레이드 초기화
        PlayerPrefs.DeleteKey("deleteprob"); //업그레이드 초기화
        PlayerPrefs.DeleteKey("deletedam"); //업그레이드 초기화
        PlayerPrefs.DeleteKey("updia1");
        PlayerPrefs.DeleteKey("updia2");
        PlayerPrefs.DeleteKey("updia3");
        PlayerPrefs.DeleteKey("updia4");
        PlayerPrefs.DeleteKey("updia5");
        PlayerPrefs.DeleteKey("updia6");
        PlayerPrefs.DeleteKey("up01");
        PlayerPrefs.DeleteKey("up02");
        PlayerPrefs.DeleteKey("up03");
        PlayerPrefs.DeleteKey("up04");
        PlayerPrefs.DeleteKey("up05");
        PlayerPrefs.DeleteKey("up06");
        PlayerPrefs.DeleteKey("dia");
        PlayerPrefs.DeleteKey("zip");
        PlayerPrefs.DeleteAll(); 
    }

    public void DiaUp()
    {   
        ES3.Save<int>("dia",300+diamond);
        ES3.Save<int>("zip",500000+zip);              
    }

    public void levelReset()
    {
        ResetUp();
        ES3.DeleteKey("level");
        ES3.DeleteKey("exp");
        ES3.DeleteKey("zip");
        ES3.DeleteKey("dia");
        ES3.DeleteKey("damage");
        //ES3.Save<int>("level",1);
        ES3.Save<float>("exp",0f);
        ES3.Save<int>("zip",5000);
        ES3.Save<int>("dia",20);
        ES3.Save<float>("damage",1f);
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("expCost");         
        //level = ES3.Load<int>("level",1);                
        ES3.DeleteFile("randomdefenseuserinfo.es3");        
        var es3File = new ES3File("randomdefenseuserinfo.es3");
        // Clear all keys from the file.
        es3File.Clear();
        
    }

    public void upButtonReset()
    {
        //upButton01.interactable = true;
        upButton02.interactable = true;
        upButton03.interactable = true;
        upButton04.interactable = true;
        upButton05.interactable = true;
        upButton06.interactable = true;
        upButton07.interactable = true;
        //upWarningText01.text = "";
        upWarningText02.text = "";
        upWarningText03.text = "";
        upWarningText04.text = "";
        upWarningText05.text = "";
        upWarningText06.text = "";
        upWarningText07.text = "";
    }
    /*
    public void stageInitialize()
    {
        level = ES3.Load<int>("level",1);
        if (level>=0)
        {
            stage01button.interactable = true;
        }
        if (level>=4)
        {
            stage02button.interactable = true;
        }
        if (level>=8)
        {
            stage03button.interactable = true;            
        }
        if (level>=12)
        {
            stage04button.interactable = true;
        }
        if (level>=16)
        {
            stage01button.interactable = false;
            stage05button.interactable = true;            
        }
        if (level>=20)
        {
            stage02button.interactable = false;
            stage06button.interactable = true;
        }
        if (level>=24)
        {
            stage03button.interactable = false;
            stage07button.interactable = true;
        }
        if (level>=28)
        {
            stage04button.interactable = false;
            stage08button.interactable = true;
        }
        if (level>=32)
        {
            stage05button.interactable = false;
            stage09button.interactable = true;
        }
        if (level>=36)
        {
            stage06button.interactable = false;
            stage10button.interactable = true;
        }        
    }    
    */    

    

    public void GameReset()
    {        
        levelReset();
        ResetUp();
    }

    private void expSave()
    {        
        zip = ES3.Load<int>("zip",5000);        
        diamond = ES3.Load<int>("dia",20);
        damage = ES3.Load<float>("damage",1);                
        zipText.text = zip.ToString();
        diamondText.text = diamond.ToString();        
    }
}
