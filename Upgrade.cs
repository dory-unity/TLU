using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class Upgrade : MonoBehaviour
{
    
    public static int archerUp = 1;
    public static int warriorUp = 1;
    public static int wizardUp = 1;
    public static float damageArrow = 1;
    public static float damageSlash = 1;
    public static float damageSpell = 1;
    public static float damageUp = 1;    
    
    public static float archerDamage;
    public static float warriorDamage;
    public static float wizardDamage;
    public static float criticprob;
    public static float deleteprob;
    public static float warexprob;
    public static float slowAmount;
    public static float deletedam;
    public static float critical;
    public Text archerUpText;
    public Text warriorUpText;
    public Text wizardUpText;    
    
    public Text archerUpPriceText;
    public Text warriorUpPriceText;
    public Text wizardUpPriceText;    
    private Text moneyText;
    private GameObject GoldWarningFrame;
    private GameObject BonusGoldFrame;
    private Text BonusGoldText;
    private GameObject WarningUI;
    public static Upgrade instance; //instance는 Upgrade inside the Upgrade
    void Awake() 
    {
        if (instance != null) //혹시라도 Upgrade가 두개 있으면 작동하지 않도록
        {
            Debug.LogError("More than one Upgrade in scene!");
            return;
        }
        instance = this; //BuildManager가 scene 내에서 단 하나만 있게 설정하기 위함        
    }

    void Start() 
    {
        archerUp = 1;
        warriorUp = 1;
        wizardUp = 1;
        damageUp = ES3.Load<float>("damage",1);        
        archerDamage = ES3.Load<float>("archerDamage",1);        
        warriorDamage = ES3.Load<float>("warriorDamage",1);
        wizardDamage = ES3.Load<float>("wizardDamage",1);
        damageArrow = ES3.Load<float>("damageArrow",1); //무기
        damageSlash = ES3.Load<float>("damageSlash",1); //무기
        damageSpell = ES3.Load<float>("damageSpell",1); //무기 
        slowAmount = ES3.Load<float>("slow",0.2f);
        criticprob = ES3.Load<float>("criticprob",10f);
        deleteprob = ES3.Load<float>("deleteprob",1.0f);
        warexprob = ES3.Load<float>("warexprob",10f);
        critical = ES3.Load<float>("critical",1.1f); 
        deletedam = ES3.Load<float>("deletedam",2.0f);
        moneyText = GameObject.Find("Canvas").transform.Find("TopPanel").transform.Find("MoneyImage").transform.Find("MoneyText").gameObject.GetComponent<Text>();
        WarningUI = GameObject.FindWithTag("WarningUI");
        GoldWarningFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("GoldWarningFrame").gameObject;
        //GoldWarningFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("GoldWarningFrame").gameObject;
        /*
        if(FindObjectOfType<WavePanelDummy>() != null)
        {
            BonusGoldFrame = WarningUI.transform.Find("WarningCanvas").transform.Find("BonusGoldFrame").gameObject;
            BonusGoldText = WarningUI.transform.Find("WarningCanvas").transform.Find("BonusGoldFrame").transform.Find("BonusGoldText").gameObject.GetComponent<Text>();
            //BonusGoldFrame = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("BonusGoldFrame").gameObject;
            //BonusGoldText = GameObject.Find("WarningUI").transform.Find("WarningCanvas").transform.Find("BonusGoldFrame").transform.Find("BonusGoldText").gameObject.GetComponent<Text>();
        }
        */
    }
    public void archerUpgrade()
    {        
        if (PlayerStats.Money < 100 * archerUp)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            Debug.Log ("Not enough money to Upgrade archer!");
            return;
        }   
        else
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
            PlayerStats.Money -= 100 * archerUp;
            moneyText.text = PlayerStats.Money.ToString();
            archerUp++;
            Debug.Log("Archer Upgrade " + archerUp);        
            archerUpText.text = "×" + archerUp.ToString();
            archerUpPriceText.text = (100 * archerUp).ToString();
        }        
    }

    public void warriorUpgrade()
    {        
        if (PlayerStats.Money < 100 * warriorUp)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            Debug.Log ("Not enough money to Upgrade warrior!");
            return;
        }
        else
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
            PlayerStats.Money -= 100 * warriorUp;
            moneyText.text = PlayerStats.Money.ToString();
            warriorUp++;
            Debug.Log("Warrior Upgrade " + warriorUp);
            warriorUpText.text = "×" + warriorUp.ToString();
            warriorUpPriceText.text = (100 * warriorUp).ToString();
        }        
    }

    public void wizardUpgrade()
    {        
        if (PlayerStats.Money < 100 * wizardUp)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            GoldWarningFrame.SetActive(true); //Gold 부족 경고
            Debug.Log ("Not enough money to Upgrade wizard!");
            return;
        }
        else
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
            PlayerStats.Money -= 100 * wizardUp;
            moneyText.text = PlayerStats.Money.ToString();
            wizardUp++;
            Debug.Log("Wizard Upgrade " + wizardUp);
            wizardUpText.text = "×" + wizardUp.ToString();
            wizardUpPriceText.text = (100 * wizardUp).ToString();
        }        
    }

    public void archerUpgradeBonus()
    {   
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);        
        archerUp++;
        Debug.Log("Archer Upgrade " + archerUp);        
        archerUpText.text = "×" + archerUp.ToString();
        archerUpPriceText.text = (100 * archerUp).ToString();
               
    }

    public void warriorUpgradeBonus()
    {   
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);        
        warriorUp++;
        Debug.Log("Warrior Upgrade " + warriorUp);
        warriorUpText.text = "×" + warriorUp.ToString();
        warriorUpPriceText.text = (100 * warriorUp).ToString();        
    }
    public void wizardUpgradeBonus()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);                
        wizardUp++;
        Debug.Log("Wizard Upgrade " + wizardUp);
        wizardUpText.text = "×" + wizardUp.ToString();
        wizardUpPriceText.text = (100 * wizardUp).ToString();     
    }

    public void upgradeDamageBonus()
    {
        damageUp = damageUp * 1.15f;
        archerDamage = archerDamage * 1.15f;
        warriorDamage = warriorDamage * 1.15f;
        wizardDamage = wizardDamage * 1.15f;
    }

    public void upgradeGoldBonus()
    {        
        PlayerStats.Money += 300;
        moneyText.text = PlayerStats.Money.ToString();
        BonusGoldFrame.SetActive(true);
        BonusGoldText.text = "300 Gold!";
    }
    public void upgradeRandomGoldBonus()
    {        
        int dice = Random.Range(50,800);
        PlayerStats.Money += dice;
        moneyText.text = PlayerStats.Money.ToString();
        BonusGoldFrame.SetActive(true);
        BonusGoldText.text = dice.ToString() + " Gold!";
    }
}
