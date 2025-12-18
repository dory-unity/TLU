using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class Shop : MonoBehaviour
{    
    private Transform turret;
    [Header ("UI Component")]
    public Image turretImage;
    public Text damageText;
    public Text rangeText;
    public Text speedText;
    public Text legendText;
    private float damage;
    private float range;
    private int rangefixed;
    private float speed;
    private string legend;
    private Text moneyText;
    private Text diaText;

    
    [Header ("Archer")]
    public TurretBlueprint normalArcher;
    public TurretBlueprint rareArcher;
    public TurretBlueprint uniqueArcher;
    public TurretBlueprint hiddenArcher;
    public TurretBlueprint legendArcher;
    public TurretBlueprint godArcher;
    
    [Header ("Warrior")]
    public TurretBlueprint normalWarrior;
    public TurretBlueprint rareWarrior;
    public TurretBlueprint uniqueWarrior;
    public TurretBlueprint hiddenWarrior;
    public TurretBlueprint legendWarrior;
    public TurretBlueprint godWarrior;
    [Header ("Wizard")]
    public TurretBlueprint normalWizard;
    public TurretBlueprint rareWizard;
    public TurretBlueprint uniqueWizard;
    public TurretBlueprint hiddenWizard;
    public TurretBlueprint legendWizard;
    public TurretBlueprint godWizard;
    private float damageUp;
    private int diamond;
    private int gamedia; //슈퍼다이스에 필요한 dia 수
    public Text gamediaText;
    private GameObject GoldWarningFrame;
    private GameObject DiaWarningFrame;
    private GameObject SelectedHeroWarningFrame;
    private GameObject DiaGoldWarningFrame;
    private GameObject HiddenSelectFrame;
    private GameObject LegendSelectFrame;
    private GameObject GodSelectFrame;
    private GameObject BonusGoldFrame;
    private GameObject WarningUI;
    private Text BonusGoldText;
    public static Shop instance; //instance는 Shop inside the Shop;


    BuildManager buildManager;
    void Awake() 
    {
        if (instance != null) //혹시라도 Shop가 두개 있으면 작동하지 않도록
        {
            Debug.LogError("More than one Shop in scene!");
            return;
        }
        instance = this; //Shop가 scene 내에서 단 하나만 있게 설정하기 위함        
    }

    void Start()
    {                        
        buildManager = BuildManager.instance;
        damageUp = ES3.Load<float>("damage",1); //default 값을 1로 넣어줘야 함     
        WarningUI = GameObject.FindWithTag("WarningUI");
        GoldWarningFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("GoldWarningFrame").gameObject;
        DiaWarningFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("DiaWarningFrame").gameObject;
        SelectedHeroWarningFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("SelectedHeroWarningFrame").gameObject;
        DiaGoldWarningFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("DiaGoldWarningFrame").gameObject;
        HiddenSelectFrame = WarningUI.transform.Find("SelectCanvas").transform.Find("HiddenSelectFrame").gameObject;        
        LegendSelectFrame = WarningUI.transform.Find("SelectCanvas").transform.Find("LegendSelectFrame").gameObject;        
        GodSelectFrame = WarningUI.transform.Find("SelectCanvas").transform.Find("GodSelectFrame").gameObject;      
        /*GoldWarningFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("GoldWarningFrame").gameObject;
        DiaWarningFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("DiaWarningFrame").gameObject;
        SelectedHeroWarningFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("SelectedHeroWarningFrame").gameObject;
        DiaGoldWarningFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("DiaGoldWarningFrame").gameObject;
        HiddenSelectFrame = GameObject.Find("WarningUI").transform.Find("SelectCanvas").transform.Find("HiddenSelectFrame").gameObject;        
        LegendSelectFrame = GameObject.Find("WarningUI").transform.Find("SelectCanvas").transform.Find("LegendSelectFrame").gameObject;        
        GodSelectFrame = GameObject.Find("WarningUI").transform.Find("SelectCanvas").transform.Find("GodSelectFrame").gameObject;      
        */  
        moneyText = GameObject.Find("Canvas").transform.Find("TopPanel").transform.Find("MoneyImage").transform.Find("MoneyText").gameObject.GetComponent<Text>();
        diaText = GameObject.Find("Canvas").transform.Find("TopPanel").transform.Find("DiamondImage").transform.Find("DiamondText").gameObject.GetComponent<Text>();
        
        /*if(FindObjectOfType<WavePanelDummy>() != null)
        {
            BonusGoldFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("BonusGoldFrame").gameObject;
            BonusGoldText = WarningUI.transform.Find("WarningCanvas").transform.Find("BonusGoldFrame").transform.Find("BonusGoldText").gameObject.GetComponent<Text>();
        }
        */ //새버전 오류로 인한 제외
    }
    
    public void DiceStage00() //tutorial stage dice
    {        
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (PlayerStats.Money < 100)
        {
            Debug.Log ("Not enough money to build that!");
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }     
        
        else
        {
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
                        
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 40f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 40f && dice1 < 70f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }else if (dice1 >= 70f && dice1 < 80f) //hidden
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }            
            }
            else  //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }            
        }
        Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
        DiceUpdate();  
    }


    public void DiceStage01() //stage01 dice
    {        
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (PlayerStats.Money < 100)
        {
            Debug.Log ("Not enough money to build that!");
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }     
        
        else
        {
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
                        
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 55f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 55f && dice1 < 85f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }            
        }
        Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
        DiceUpdate();  
    }

    public void DiceStage02()
    {        
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (PlayerStats.Money < 100)
        {
            Debug.Log ("Not enough money to build that!");
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }     
        
        else
        {
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
                        
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 55f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 55f && dice1 < 85f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 85f && dice1 < 95f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else //hidden;
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }            
        }
        Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
        DiceUpdate();  
    }
    public void DiceStage03()
    {        
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (PlayerStats.Money < 100)
        {
            Debug.Log ("Not enough money to build that!");
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }    
        
        else
        {
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
                        
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 55f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 55f && dice1 < 85f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 85f && dice1 < 95f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else if (dice1 >= 95f && dice1 < 99.5f) //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
            else //legend
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                LegendSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(legendArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(legendWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(legendWizard);
                }
            }            
        }
        Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
        DiceUpdate();  
    }
    public void DiceStage04()
    {        
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (PlayerStats.Money < 100)
        {
            Debug.Log ("Not enough money to build that!");
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }    
        
        else
        {
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
                        
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 40f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 40f && dice1 < 70f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 70f && dice1 < 90f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else if (dice1 >= 90f && dice1 < 99f) //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
            else //legend
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                LegendSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(legendArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(legendWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(legendWizard);
                }
            }            
        }
        Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
        DiceUpdate();  
    }
    public void DiceStage05()
    {        
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (PlayerStats.Money < 100)
        {
            Debug.Log ("Not enough money to build that!");
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }  
        
        else
        {
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
                        
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 38f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 38f && dice1 < 68f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 68f && dice1 < 88f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else if (dice1 >= 88f && dice1 < 98f) //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                HiddenSelectFrame.SetActive(true);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
            else if (dice1 >= 98f && dice1 < 99.5f) //legend
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                LegendSelectFrame.SetActive(true);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(legendArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(legendWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(legendWizard);
                }
            }
            else // god
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("godselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                GodSelectFrame.SetActive(true); //Hero Select 알림
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(godArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(godWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(godWizard);
                }
            }
        }
        Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
        DiceUpdate();  
    }
    public void DiceStage06()
        {        
            if (buildManager.turretToBuild != null)
            {
                Debug.Log ("Place Current Turret");            
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
                SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
                return;
            }

            if (PlayerStats.Money < 100)
            {
                Debug.Log ("Not enough money to build that!");
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
                GoldWarningFrame.SetActive(true); //Gold 부족 경고
                return;
            }  
            
            else
            {
                PlayerStats.Money -= 100;
                moneyText.text = PlayerStats.Money.ToString();
                            
                
                float dice1 = Random.Range(0,100);
                float dice2 = Random.Range(0,3);
                            
                //Debug.Log(dice1);
                //Debug.Log(dice2);
            
                if (dice1 >= 0f && dice1 < 33f) //normal
                {
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(normalArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(normalWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(normalWizard);
                    }            
                }
                else if (dice1 >= 33f && dice1 < 63f) //rare
                {
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(rareArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(rareWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(rareWizard);
                    }            
                }
                else if (dice1 >= 63f && dice1 < 86f) //unique
                {
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(uniqueArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(uniqueWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(uniqueWizard);
                    }
                }
                else if (dice1 >= 86f && dice1 < 96f) //hidden
                {
                    if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                    HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                    HiddenSelectFrame.SetActive(true);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(hiddenArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(hiddenWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(hiddenWizard);
                    }
                }
                else if (dice1 >= 96f && dice1 < 99f) //legend
                {
                    if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                    HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                    LegendSelectFrame.SetActive(true);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(legendArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(legendWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(legendWizard);
                    }
                }
                else // god
                {
                    if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("godselect");
                }
                    HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                    GodSelectFrame.SetActive(true); //Hero Select 알림
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(godArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(godWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(godWizard);
                    }
                }
            }
            Debug.Log("Turret Bought! Money Left : " + PlayerStats.Money);              
            DiceUpdate();  
        }

    public void PowerDiceStage00() //tutorial stage powerdice
    {        
        diamond = ES3.Load<int>("dia");
        gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
                
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (diamond < gamedia || PlayerStats.Money < 100)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            Debug.Log ("Not enough money to build that!");
            DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }     
        
        else
        {
            ES3.Save<int>("dia",diamond-gamedia);            
            diaText.text = (diamond-gamedia).ToString();
            gamedia += 2;
            PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
            gamediaText.text = gamedia.ToString();    
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
            
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 10f) //normal 10%
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher); 
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 10f && dice1 < 30f) //rare 20%
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }else if (dice1 >= 30f && dice1 < 70f) //hidden 40%
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }            
            }
            else // unique 30%
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);                
                
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
        }
        Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
        DiceUpdate();
    }

    public void PowerDiceStage01() //stage01 powerdice
    {        
        diamond = ES3.Load<int>("dia");
        gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
                
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (diamond < gamedia || PlayerStats.Money < 100)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            Debug.Log ("Not enough money to build that!");
            DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }     
        
        else
        {
            ES3.Save<int>("dia",diamond-gamedia);            
            diaText.text = (diamond-gamedia).ToString();
            gamedia += 2;
            PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
            gamediaText.text = gamedia.ToString();    
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
            
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 10f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 10f && dice1 < 60f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
        }
        Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
        DiceUpdate();
    }
    public void PowerDiceStage02()
    {        
        diamond = ES3.Load<int>("dia");
        gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (diamond < gamedia || PlayerStats.Money < 100)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            Debug.Log ("Not enough money to build that!");
            DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }      
        
        else
        {
            ES3.Save<int>("dia",diamond-gamedia);            
            diaText.text = (diamond-gamedia).ToString();
            gamedia += 2;
            PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
            gamediaText.text = gamedia.ToString();    
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
            
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 40f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 40f && dice1 < 70f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 70f && dice1 < 90f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
        }
        Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
        DiceUpdate();
    }
    public void PowerDiceStage03()
    {    
        diamond = ES3.Load<int>("dia");
        gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (diamond < gamedia || PlayerStats.Money < 100)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            Debug.Log ("Not enough money to build that!");
            DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }      
        
        else
        {
            ES3.Save<int>("dia",diamond-gamedia);            
            diaText.text = (diamond-gamedia).ToString();
            gamedia += 2;
            PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
            gamediaText.text = gamedia.ToString();    
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 40f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 40f && dice1 < 70f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 70f && dice1 < 90f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else if (dice1 >= 90f && dice1 < 99f) //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
            else //legend
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                LegendSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(legendArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(legendWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(legendWizard);
                }
            }
        }
        Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
        DiceUpdate();
    }
    public void PowerDiceStage04()
    {    
        diamond = ES3.Load<int>("dia");
        gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (diamond < gamedia || PlayerStats.Money < 100)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            Debug.Log ("Not enough money to build that!");
            DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }      
        
        else
        {
            ES3.Save<int>("dia",diamond-gamedia);            
            diaText.text = (diamond-gamedia).ToString();
            gamedia += 2;
            PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
            gamediaText.text = gamedia.ToString();    
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 35f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 35f && dice1 < 65f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 65f && dice1 < 87f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else if (dice1 >= 87f && dice1 < 98f) //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HiddenSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
            else //legend
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                LegendSelectFrame.SetActive(true); //Hero Select 알림
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(legendArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(legendWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(legendWizard);
                }
            }
        }
        Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
        DiceUpdate();
    }
    public void PowerDiceStage05()
    {        
        diamond = ES3.Load<int>("dia");
        gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
        if (buildManager.turretToBuild != null)
        {
            Debug.Log ("Place Current Turret");            
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
            return;
        }

        if (diamond < gamedia || PlayerStats.Money < 100)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
            Debug.Log ("Not enough money to build that!");
            DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
            return;
        }      
        
        else
        {
            ES3.Save<int>("dia",diamond-gamedia);            
            diaText.text = (diamond-gamedia).ToString();
            gamedia += 2;
            PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
            gamediaText.text = gamedia.ToString();    
            PlayerStats.Money -= 100;
            moneyText.text = PlayerStats.Money.ToString();
            
            float dice1 = Random.Range(0,100);
            float dice2 = Random.Range(0,3);
                        
            //Debug.Log(dice1);
            //Debug.Log(dice2);
        
            if (dice1 >= 0f && dice1 < 24f) //normal
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(normalArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(normalWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(normalWizard);
                }            
            }
            else if (dice1 >= 24f && dice1 < 54f) //rare
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(rareArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(rareWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(rareWizard);
                }            
            }
            else if (dice1 >= 54f && dice1 < 76f) //unique
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(uniqueArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(uniqueWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(uniqueWizard);
                }
            }
            else if (dice1 >= 76f && dice1 < 96f) //hidden
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                HiddenSelectFrame.SetActive(true);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(hiddenArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(hiddenWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(hiddenWizard);
                }
            }
            else if (dice1 >= 96f && dice1 < 99f) //legend
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                LegendSelectFrame.SetActive(true);
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(legendArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(legendWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(legendWizard);
                }
            }
            else // god
            {
                if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("godselect");
                }
                HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                GodSelectFrame.SetActive(true); //Hero Select 알림
                if (dice2 >= 0f && dice2 < 1f)
                {
                    buildManager.SelectTurretToBuild(godArcher);
                }
                else if (dice2 >= 1f && dice2 < 2f)
                {
                    buildManager.SelectTurretToBuild(godWarrior);
                }
                else
                {
                    buildManager.SelectTurretToBuild(godWizard);
                }
            }
        }
        Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
        DiceUpdate();
    }
    public void PowerDiceStage06()
        {        
            diamond = ES3.Load<int>("dia");
            gamedia = PlayerPrefs.GetInt("gamedia",1); //슈퍼다이스에 필요한 dia 수
            if (buildManager.turretToBuild != null)
            {
                Debug.Log ("Place Current Turret");            
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
                SelectedHeroWarningFrame.SetActive(true); //Hero Select 경고
                return;
            }

            if (diamond < gamedia || PlayerStats.Money < 100)
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);            
                Debug.Log ("Not enough money to build that!");
                DiaGoldWarningFrame.SetActive(true); //Gold 부족 경고
                return;
            }      
            
            else
            {
                ES3.Save<int>("dia",diamond-gamedia);            
                diaText.text = (diamond-gamedia).ToString();
                gamedia += 2;
                PlayerPrefs.SetInt("gamedia",gamedia); //필요 다이아 2개 추가
                gamediaText.text = gamedia.ToString();    
                PlayerStats.Money -= 100;
                moneyText.text = PlayerStats.Money.ToString();
                
                float dice1 = Random.Range(0,100);
                float dice2 = Random.Range(0,3);
                            
                //Debug.Log(dice1);
                //Debug.Log(dice2);
            
                if (dice1 >= 0f && dice1 < 20f) //normal
                {
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(normalArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(normalWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(normalWizard);
                    }            
                }
                else if (dice1 >= 20f && dice1 < 47f) //rare
                {
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(rareArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(rareWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(rareWizard);
                    }            
                }
                else if (dice1 >= 47f && dice1 < 72f) //unique
                {
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(uniqueArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(uniqueWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(uniqueWizard);
                    }
                }
                else if (dice1 >= 72f && dice1 < 92f) //hidden
                {
                    if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("hiddenselect");
                }
                    HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                    HiddenSelectFrame.SetActive(true);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(hiddenArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(hiddenWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(hiddenWizard);
                    }
                }
                else if (dice1 >= 92f && dice1 < 98f) //legend
                {
                    if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("legendselect");
                }
                    HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                    LegendSelectFrame.SetActive(true);
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(legendArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(legendWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(legendWizard);
                    }
                }
                else // god
                {
                    if(FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("godselect");
                }
                    HapticPatterns.PlayConstant(0.5f, 0.5f, 0.5f);
                    GodSelectFrame.SetActive(true); //Hero Select 알림
                    if (dice2 >= 0f && dice2 < 1f)
                    {
                        buildManager.SelectTurretToBuild(godArcher);
                    }
                    else if (dice2 >= 1f && dice2 < 2f)
                    {
                        buildManager.SelectTurretToBuild(godWarrior);
                    }
                    else
                    {
                        buildManager.SelectTurretToBuild(godWizard);
                    }
                }
            }
            Debug.Log("Power Turret Bought! Money Left : " + PlayerStats.Money);
            DiceUpdate();
        }

    public void DiceUpdate() //이미지 없애는건 Node.cs에 있음
    {   
        float weaponDamage = 1f;
        if(buildManager.turretToBuild.prefab.CompareTag("Archer")) //무기 데미지를 더해주는 부분
        {
            weaponDamage = Upgrade.damageArrow * Upgrade.archerDamage * Upgrade.archerUp;
        }
        else if(buildManager.turretToBuild.prefab.CompareTag("Warrior"))
        {
            weaponDamage = Upgrade.damageSlash * Upgrade.warriorDamage * Upgrade.warriorUp;;
        }
        else if(buildManager.turretToBuild.prefab.CompareTag("Wizard"))
        {
            weaponDamage = Upgrade.damageSpell * Upgrade.wizardDamage * Upgrade.wizardUp;;
        }


        turretImage.sprite = buildManager.turretToBuild.prefabimage;        
        Debug.Log("Damage Up : " + damageUp);
        Debug.Log("Weapon Damage : " + weaponDamage);
        
        damage = buildManager.turretToBuild.prefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().damage * weaponDamage;
        range = buildManager.turretToBuild.prefab.GetComponent<Turret>().range;
        RangeChange();
        speed = buildManager.turretToBuild.prefab.GetComponent<Turret>().fireRate;
        legend = buildManager.turretToBuild.prefab.GetComponent<Turret>().legend;
        damageText.text = "DMG " + damage.ToString("F0");
        rangeText.text = "RNG " + rangefixed.ToString();
        speedText.text = "SPD " + speed.ToString("F1");
        legendText.text = legend;
    }   
    
    private void RangeChange()
    {
        range = buildManager.turretToBuild.prefab.GetComponent<Turret>().range;
        if (range < 1)
        {
            rangefixed = 1;
        }
        else if (range >= 1 && range < 1.5)
        {
            rangefixed = 2;
        }
        else if (range >= 1.5 && range < 2)
        {
            rangefixed = 3;
        }
        else if (range >= 2 && range < 2.5)
        {
            rangefixed = 4;
        }
        
        else if (range >= 5)
        {
            rangefixed = 5;
        }
    }

    public void archerLegendBonus()
    {
        if (buildManager.turretToBuild != null)
        {            
            PlayerStats.Money += 400;
            moneyText.text = PlayerStats.Money.ToString();
            BonusGoldFrame.SetActive(true);
            BonusGoldText.text = "400 Gold!";
            return;
        }
        else
        {
            buildManager.SelectTurretToBuild(legendArcher);
            DiceUpdate();
        }
    }
    public void warriorLegendBonus()
    {
        if (buildManager.turretToBuild != null)
        {            
            PlayerStats.Money += 400;
            moneyText.text = PlayerStats.Money.ToString();
            BonusGoldFrame.SetActive(true);
            BonusGoldText.text = "400 Gold!";
            return;
        }
        else
        {
            buildManager.SelectTurretToBuild(legendWarrior);            
            DiceUpdate();
        }
    }
    public void wizardLegendBonus()
    {
        if (buildManager.turretToBuild != null)
        {            
            PlayerStats.Money += 400;
            moneyText.text = PlayerStats.Money.ToString();
            BonusGoldFrame.SetActive(true);
            BonusGoldText.text = "400 Gold!";
            return;
        }
        else
        {
            buildManager.SelectTurretToBuild(legendWizard);            
            DiceUpdate();
        }
    }

    public void archerHiddenBonus()
    {
        if (buildManager.turretToBuild != null)
        {            
            PlayerStats.Money += 200;
            moneyText.text = PlayerStats.Money.ToString();
            BonusGoldFrame.SetActive(true);
            BonusGoldText.text = "200 Gold!";
            return;
        }
        else
        {
            buildManager.SelectTurretToBuild(hiddenArcher);
            DiceUpdate();
        }
    }
    public void warriorHiddenBonus()
    {
        if (buildManager.turretToBuild != null)
        {            
            PlayerStats.Money += 200;
            moneyText.text = PlayerStats.Money.ToString();
            BonusGoldFrame.SetActive(true);
            BonusGoldText.text = "200 Gold!";
            return;
        }
        else
        {
            buildManager.SelectTurretToBuild(hiddenWarrior);            
            DiceUpdate();
        }
    }
    public void wizardHiddenBonus()
    {
        if (buildManager.turretToBuild != null)
        {            
            PlayerStats.Money += 200;
            moneyText.text = PlayerStats.Money.ToString();
            BonusGoldFrame.SetActive(true);
            BonusGoldText.text = "200 Gold!";
            return;
        }
        else
        {
            buildManager.SelectTurretToBuild(hiddenWizard);            
            DiceUpdate();
        }
    }
}
