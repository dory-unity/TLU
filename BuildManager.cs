using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //instance는 Buildmanager inside the Buildmanager
    
    
    void Awake() 
    {
        if (instance != null) //혹시라도 BuildManager가 두개 있으면 작동하지 않도록
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this; //BuildManager가 scene 내에서 단 하나만 있게 설정하기 위함        
    }

    public GameObject buildEffect;
    
    //public GameObject selectedPrefabImage;
    
    public TurretBlueprint turretToBuild = null;

    public TurretBlueprint turretToMerge = null;
    

    void Start() 
    {
        turretToBuild = null;        
        turretToMerge = null;   
        buildEffect = (GameObject)Resources.Load("buildEffect01");
        //buildEffect = Instantiate(Resources.Load("buildEffect01", typeof(GameObject))) as GameObject;
    }

    public bool CanBuild {get{return turretToBuild != null;}} //turretToBuild가 null이 아니면 Can Build는 true
    public bool HasMoney {get{return PlayerStats.Money >= 100;}}    
    
    
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void SelectTurretToMerge(TurretBlueprint turret)
    {
        turretToMerge = turret;
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public TurretBlueprint GetTurretToMerge()
    {        
        return turretToMerge;
    }

    public void ResetTurret()
    {
        turretToBuild = null;
    }

    public void ResetTurretMerge()
    {
        turretToMerge= null;
    }
}
