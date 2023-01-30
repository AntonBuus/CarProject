using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sounds 
{

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public string name;


    [HideInInspector]
    public AudioSource source;


    



}
