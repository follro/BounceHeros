using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class SoundManager : MonoBehaviour, ISoundManager
{

    [Header("오디오 소스 프리팹")]
    [SerializeField]
    private PooledAudioSource audioSourcePrefab; 

    [Header("풀 설정")]
    [SerializeField]
    private int initialPoolSize = 16;

    private BounceHeros.ObjectPool<PooledAudioSource> pool;

    void Awake()
    {
        GameObject poolParent = new GameObject("AudioSourcePool");
        poolParent.transform.SetParent(this.transform);
        if(audioSourcePrefab!=null)
            pool = new BounceHeros.ObjectPool<PooledAudioSource>(audioSourcePrefab, initialPoolSize, poolParent.transform, 16);
        else
        {
            Debug.LogError("풀이 널인데?");
        }
    }

    public void PlaySound2D(AudioEventSO audioEvent)
    {
        // 풀에서 스폰 (위치는 매니저 자신의 위치)
        PooledAudioSource source = pool.Spawn(transform.position, Quaternion.identity);
        if (source != null)
        {
            // 스폰된 객체에게 SO를 넘겨주며 재생 명령
            source.Play(audioEvent);
        }
    }

    /// <summary>
    /// 3D 사운드를 재생합니다. (월드 공간)
    /// </summary>
    public void PlaySound3D(AudioEventSO audioEvent, Vector3 position)
    {
        // 풀에서 스폰 (지정된 위치)
        PooledAudioSource source = pool.Spawn(position, Quaternion.identity);
        if (source != null)
        {
            // 3D 사운드를 위해 Spatial Blend 설정 (프리팹에서 미리 해도 됨)
            // source.GetComponent<AudioSource>().spatialBlend = 1.0f;
            source.Play(audioEvent);
        }
    }

    /// <summary>
    /// 루프 사운드(BGM 등)를 재생하고, 제어할 수 있도록 인스턴스를 반환합니다.
    /// </summary>
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

