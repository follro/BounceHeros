using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObjects/Managers/AudioEventSO")]
public class AudioEventSO : ScriptableObject
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool isLooping = false;
    public SoundManager.AudioType audioType;
}