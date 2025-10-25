using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour 
{
    //private IResourceManager resourceManager;

    [Header("BGM")]  
    public AudioClip bgmClips;
    public float bgmVolume;
    private List<AudioSource> bgmSource;

    [Header("SFX")]
    public AudioClip sfxClips;

    public void Initialize(AssetLoadManager resourceManager)
    {

    }
}
