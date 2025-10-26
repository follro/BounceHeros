using BounceHeros;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : PoolableObject
{
    private AudioSource _audioSource;
    private Coroutine _autoReturnCoroutine; 

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false; 
    }

    public void Play(AudioEventSO audioEvent)
    {
        if (_autoReturnCoroutine != null)
        {
            StopCoroutine(_autoReturnCoroutine);
            _autoReturnCoroutine = null;
        }

        _audioSource.clip = audioEvent.clip;
        _audioSource.volume = audioEvent.volume;
        _audioSource.pitch = audioEvent.pitch;
        _audioSource.outputAudioMixerGroup = audioEvent.mixerGroup;
        _audioSource.loop = audioEvent.isLooping;

        _audioSource.Play();

        if (!_audioSource.loop)
        {
            float duration = audioEvent.clip.length / _audioSource.pitch;
            _autoReturnCoroutine = StartCoroutine(AutoReturn(duration));
        }
    }

    private IEnumerator AutoReturn(float delay)
    {
        yield return new WaitForSeconds(delay);

        _autoReturnCoroutine = null;
        OnReturnToPool();
    }

    public void StopAndReturn()
    {
        _audioSource.Stop();

        if (_autoReturnCoroutine != null)
        {
            StopCoroutine(_autoReturnCoroutine);
            _autoReturnCoroutine = null;
        }

        OnReturnToPool();
    }

    public override void OnReturnToPool()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
        _audioSource.loop = false;
        base.OnReturnToPool();
    }
}
