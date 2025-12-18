using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{
    public GameObject OnButton;
    public GameObject OffButton;

    bool sound;
    void Start()
    {                
        sound = PlayerPrefs.GetInt("sound",1)==1? true:false;
        Debug.Log("sound : "+sound);
        if(sound)
        {
            OnButton.SetActive(sound);
            OffButton.SetActive(!sound);
            if(FindObjectOfType<AudioManager>())
            FindObjectOfType<AudioManager>().VolumeOn();
        }
        else
        {
            OnButton.SetActive(sound);
            OffButton.SetActive(!sound);
            if(FindObjectOfType<AudioManager>())
            FindObjectOfType<AudioManager>().VolumeOff();
        }
    }

    
    void Update()
    {
        //sound = PlayerPrefs.GetInt("sound",1)==1? true:false;
    }

    public void SoundOn()
    {
        Debug.Log("Sound On");        
        OnButton.SetActive(true);
        OffButton.SetActive(false);
        sound = true;
        PlayerPrefs.SetInt("sound", sound? 1:0);
        FindObjectOfType<AudioManager>().VolumeOn();
    }

    public void SoundOff()
    {
        Debug.Log("Sound Off");        
        OnButton.SetActive(false);
        OffButton.SetActive(true);    
        sound = false;
        PlayerPrefs.SetInt("sound", sound? 1:0);
        FindObjectOfType<AudioManager>().VolumeOff();
    }
}
