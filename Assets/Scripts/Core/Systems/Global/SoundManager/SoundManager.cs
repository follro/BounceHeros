using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour, ISoundManager
{
    public enum AudioType
    {
        Master,
        SFX,
        BGM,

        End
    }
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("AudioMixerGroup")]
    [SerializeField] private AudioMixerGroup masterMixerGroup;
    [SerializeField] private AudioMixerGroup bgmMixerGroup; 
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    [Header("오디오 소스 프리팹")]
    [SerializeField]
    private PooledAudioSource audioSourcePrefab; 

    [Header("풀 설정")]
    [SerializeField]
    private int initialPoolSize = 16;

    private BounceHeros.ObjectPool<PooledAudioSource> pool;

    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        GameObject poolParent = new GameObject("AudioSourcePool");
        poolParent.transform.SetParent(this.transform);
        if (audioSourcePrefab == null)
        {
            Debug.LogError("audioSourcePrefab이 null입니다!");
            return;
        }
        pool = new BounceHeros.ObjectPool<PooledAudioSource>(audioSourcePrefab, initialPoolSize, poolParent.transform, 16);
    }

    private AudioMixerGroup GetMixerGroup(AudioType type)
    {
        return type switch
        {
            AudioType.Master => masterMixerGroup,
            AudioType.BGM => bgmMixerGroup,
            AudioType.SFX => sfxMixerGroup,
            _ => bgmMixerGroup
        };
    }

    public void SetVolume(AudioType type , float volume)
    {
        audioMixer.SetFloat(type.ToString(), LinearToDecibel(volume));
    }
    private float LinearToDecibel(float linear)
    {
        return linear > 0 ? Mathf.Log10(linear) * 20 : -80f;
    }

    public void PlaySound2D(AudioEventSO audioEvent)
    {
        PooledAudioSource source = pool.Spawn(transform.position, Quaternion.identity);
        if (source != null)
        {
            AudioMixerGroup mixerGroup = GetMixerGroup(audioEvent.audioType);
            source.Play(audioEvent, mixerGroup);
        }
    }

    public void PlaySound3D(AudioEventSO audioEvent, Vector3 position)
    {
        PooledAudioSource source = pool.Spawn(position, Quaternion.identity);
        if (source != null)
        {
            AudioMixerGroup mixerGroup = GetMixerGroup(audioEvent.audioType);
            source.Play(audioEvent, mixerGroup);
        }
    }

    public PooledAudioSource PlayLooping(AudioEventSO audioEvent)
    {
        PooledAudioSource source = pool.Spawn(transform.position, Quaternion.identity);
        if (source != null)
        {
            AudioMixerGroup mixerGroup = GetMixerGroup(audioEvent.audioType);
            source.Play(audioEvent, mixerGroup);
            return source; // 나중에 StopAndReturn()을 호출하기 위해 반환
        }
        return null;
    }
}

