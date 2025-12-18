using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] bgms;
    

    public static AudioManager instance;
    
    

    void Awake()
    {   
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);        


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;                  
        }

        foreach (Sound s in bgms)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;                  
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgms, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("BGM: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    
    public void StopBGM(string name)
    {
        Sound s = Array.Find(bgms, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void VolumeOn()
    {        
        Debug.Log("sound length : " + sounds.Length);
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = sounds[i].volume;
        }
    }
    public void VolumeOff()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = 0;
        }
    }
    
    public void BGMOn()
    {
        Debug.Log("BGM length : " + bgms.Length);
        for (int i = 0; i < bgms.Length; i++)
        {
            bgms[i].source.volume = bgms[i].volume;
        }
    }   

    public void BGMOff()
    {
        for (int i = 0; i < bgms.Length; i++)
        {
            bgms[i].source.volume = 0;
        }
    }

    public void audioReset()
    {
        AudioSettings.Reset(AudioSettings.GetConfiguration());        
    }
}
