using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;
public class Vibration : MonoBehaviour
{   
    public GameObject OnButton;
    public GameObject OffButton;
    bool vibration;
    
    void Start() 
    {        
        vibration = PlayerPrefs.GetInt("vib",1)==1? true:false;
        Debug.Log("vibration : "+vibration);
        if(vibration == true)
        {
            HapticController.hapticsEnabled = true;
        }
        else if(vibration == false)
        {
            HapticController.hapticsEnabled = false;
        }
        
        OnButton.SetActive(vibration);
        OffButton.SetActive(!vibration);
    }

    void Update()
    {        
        //vibration = PlayerPrefs.GetInt("vib",1)==1? true:false;
    }
    public void VibeOn()
    {
        Debug.Log("Vibration On");
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
        HapticController.hapticsEnabled = true;
        OnButton.SetActive(true);
        OffButton.SetActive(false);
        vibration = true;
        PlayerPrefs.SetInt("vib", vibration? 1:0);        
    }

    public void VibeOff()
    {
        Debug.Log("Vibration Off");
        HapticController.hapticsEnabled = false;
        OnButton.SetActive(false);
        OffButton.SetActive(true);    
        vibration = false;
        PlayerPrefs.SetInt("vib", vibration? 1:0);            
    }
}
