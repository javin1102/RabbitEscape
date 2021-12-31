using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Music[] music;
    public SFX[] sfx;
    public SFX2[] ring;
    public AudioSource mSource, sSource, rSource;

    public static AudioManager instance;

    private void Awake()
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

        foreach (Music m in music)
        {
            m.source = mSource;
        }

        foreach (SFX s in sfx)
        {
            s.source = sSource;
        }

        foreach (SFX2 r in ring)
        {
            r.source = rSource;
        }


    }

    public void SetMasterVolume()
    {
        mSource.volume = currentBaseVolM * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("soundVolume", 1f);
        sSource.volume = currentBaseVolS * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
        rSource.volume = currentBaseVolS * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
    }
    float currentBaseVolM, currentBaseVolS, currentBaseVolR;
    public void PlayM(string name)
    {
        Music m = Array.Find(music, music => music.name == name);
        currentBaseVolM = m.volume;
        m.source.volume = currentBaseVolM * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("soundVolume", 1f);
        m.source.clip = m.clip;
        m.source.Play();
        
    }

    public void PlayS(string name)
    {
        SFX s = Array.Find(sfx, sfx => sfx.name == name);
        currentBaseVolS = s.volume;
        s.source.volume = currentBaseVolS * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
        s.source.clip = s.clip;
        s.source.PlayOneShot(s.source.clip, s.source.volume);
    }

    public void PlayR(string name)
    {
        SFX2 r = Array.Find(ring, ring => ring.name == name);
        currentBaseVolR = r.volume;
        r.source.volume = currentBaseVolR * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
        r.source.clip = r.clip;
        r.source.PlayOneShot(r.source.clip, r.source.volume);
    }
}
