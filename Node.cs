using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour //node 위에 turret을 짓게 해주는 스크립트
{
    private Vector3 positionOffset;
    private Vector3 effectOffset;
    BuildManager buildManager;
    
    private Image turretImage;
    private Text damageText; //Shop의 text를 null 하기 위함
    private Text rangeText; //Shop의 text를 null 하기 위함
    private Text speedText; //Shop의 text를 null 하기 위함
    private Text legendText; //Shop의 text를 null 하기 위함

    private Transform canvas;
    private Transform shop;

    [Header ("Optional")]
    public GameObject turret;

    [HideInInspector]
	public TurretBlueprint turretBlueprint;

    
    void Start()
    {        
        buildManager = BuildManager.instance;        
        shop = GameObject.Find("Canvas").transform.Find("Shop");
        canvas = GameObject.Find("Canvas").transform;
        positionOffset = new Vector3(0,0,-1.01f);
        effectOffset = new Vector3(0,0.3f,-2.02f); //등장 효과 
    }

    
    void Update()
    {
        
    }


    void OnMouseDown() 
    {
        
        if(turret != null) //이미 터렛이 있으면 지을 수 없음
        {
            Debug.Log("Can't build there - TODO : Display on screen.");
            return;            
        }

        if (!buildManager.CanBuild) //CanBuild가 아닐 경우 return
        return;

        BuildTurret(buildManager.GetTurretToBuild()); // turretToBuild를 가져와서 생성
        buildManager.ResetTurret(); // turretToBuild = null로
        turretImageNull();
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position + positionOffset, Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position + effectOffset, Quaternion.identity); //새로 만들때 이펙트
        Destroy(effect, 3f); //3초 후에 이펙스 삭제
        
    }

    void turretImageNull() //이미지 만드는건 Shop.cs에 있음
    {
        turretImage = shop.Find("TurretImage").GetComponent<Image>();
        turretImage.sprite = canvas.Find("TurretImage2").GetComponent<Image>().sprite; //TurretImage2의 이미지를 가져다 쓰기
        damageText = shop.Find("TurretText").Find("DamageText").GetComponent<Text>();
        rangeText = shop.Find("TurretText").Find("RangeText").GetComponent<Text>();
        speedText = shop.Find("TurretText").Find("SpeedText").GetComponent<Text>();
        legendText = shop.Find("TurretText").Find("LegendText").GetComponent<Text>();
        damageText.text = null;
        rangeText.text = null;
        speedText.text = null;
        legendText.text =null;
    }

    public void UpdateTurret(GameObject _turret)
    {
        turret = _turret;
    }
}
