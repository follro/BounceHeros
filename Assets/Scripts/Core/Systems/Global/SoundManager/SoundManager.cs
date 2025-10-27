using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour, ISoundManager
{

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

    public void PlaySound2D(AudioEventSO audioEvent)
    {
        PooledAudioSource source = pool.Spawn(transform.position, Quaternion.identity);
        if (source != null)
        {
            source.Play(audioEvent);
        }
    }

    public void PlaySound3D(AudioEventSO audioEvent, Vector3 position)
    {
        PooledAudioSource source = pool.Spawn(position, Quaternion.identity);
        if (source != null)
        {
            source.Play(audioEvent);
        }
    }

    public PooledAudioSource PlayLooping(AudioEventSO audioEvent)
    {
        PooledAudioSource source = pool.Spawn(transform.position, Quaternion.identity);
        if (source != null)
        {
            source.Play(audioEvent);
            return source; // 나중에 StopAndReturn()을 호출하기 위해 반환
        }
        return null;
    }
}

