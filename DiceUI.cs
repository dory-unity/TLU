using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    private Image selectedImage;
    BuildManager buildManager;
    private TurretBlueprint _turret;
    private GameObject turretPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite DiceUpdate()
    {
        _turret = buildManager.turretToBuild;        
        selectedImage.sprite = _turret.prefabimage;
        return selectedImage.sprite;
    }

    
}
