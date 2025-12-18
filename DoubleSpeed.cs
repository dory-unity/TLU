using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class DoubleSpeed : MonoBehaviour
{       
    public Text speedText;

    public void Double()
    {        
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
        if (Time.timeScale == 1.0f)
        {            
            Time.timeScale = 2.0f;
            speedText.text = "×2";
        }
        else if (Time.timeScale == 2.0f)
        {
            Time.timeScale = 3.0f;
            speedText.text = "×3";
        }
        else if (Time.timeScale == 3.0f)
        {
            Time.timeScale = 1.0f;
            speedText.text = "";
            return;
        }
    }
}
