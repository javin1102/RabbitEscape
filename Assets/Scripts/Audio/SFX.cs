using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SFX
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public AudioSource source;
}