using BounceHeros;
using UnityEngine;
using Cysharp.Threading.Tasks; 
using System.Threading;      
using System;                

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : PoolableObject
{
    private AudioSource audioSource;
    private CancellationTokenSource autoReturnCts; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Play(AudioEventSO audioEvent)
    {
        if (autoReturnCts != null)
        {
            autoReturnCts.Cancel();
            autoReturnCts.Dispose();
            autoReturnCts = null;
        }

        audioSource.clip = audioEvent.clip;
        audioSource.volume = audioEvent.volume;
        audioSource.pitch = audioEvent.pitch;
        audioSource.outputAudioMixerGroup = audioEvent.mixerGroup;
        audioSource.loop = audioEvent.isLooping;

        audioSource.Play();

        if (!audioSource.loop)
        {
            float duration = audioEvent.clip.length / audioSource.pitch;

            autoReturnCts = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());

            AutoReturnAsync(duration, autoReturnCts.Token).Forget();
        }
    }

    private async UniTaskVoid AutoReturnAsync(float delay, CancellationToken token)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

            OnReturnToPool();
        }
        catch (OperationCanceledException)
        {
        }
    }

    public void StopAndReturn()
    {
        audioSource.Stop();

        if (autoReturnCts != null)
        {
            autoReturnCts.Cancel();
            autoReturnCts.Dispose();
            autoReturnCts = null;
        }

        OnReturnToPool();
    }

    public override void OnReturnToPool()
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;

        if (autoReturnCts != null)
        {
            autoReturnCts.Cancel();
            autoReturnCts.Dispose();
            autoReturnCts = null;
        }

        base.OnReturnToPool();
    }
}