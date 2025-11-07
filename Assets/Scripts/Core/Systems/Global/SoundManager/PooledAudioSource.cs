using BounceHeros;
using UnityEngine;
using Cysharp.Threading.Tasks; 
using System.Threading;      
using System;
using UnityEngine.Audio;

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

    public void Play(AudioEventSO audioEvent, AudioMixerGroup audioMixerGroup)
    {
        if (audioEvent == null || audioEvent.clip == null)
        {
            Debug.LogWarning("Invalid audio event or clip");
            return;
        }

        CleanupCancellationToken();

        audioSource.clip = audioEvent.clip;
        audioSource.volume = audioEvent.volume;
        audioSource.pitch = audioEvent.pitch;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
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
            if (!token.IsCancellationRequested) 
            {
                OnReturnToPool();
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            Debug.LogError($"AutoReturn failed: {ex}");
        }
    }

    /*public void StopAndReturn()
    {
        CleanupCancellationToken();

        OnReturnToPool();
    }*/

    public override void OnReturnToPool()
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;

        CleanupCancellationToken();

        base.OnReturnToPool();
    }

    private void CleanupCancellationToken()
    {
        if (autoReturnCts != null)
        {
            autoReturnCts.Cancel();
            autoReturnCts.Dispose();
            autoReturnCts = null;
        }
    }
}