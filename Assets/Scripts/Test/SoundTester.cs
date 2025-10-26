using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    ISoundManager soundManager;
    public AudioEventSO testAudioEvent;
    private void Awake()
    {
        if (GlobalServiceLocator.TryGet<ISoundManager>(out soundManager))
            Debug.Log("음악 매니저 로드 성공");
    }

    private void Update()
    {
        var key = KeyCode.Space;

        if(Input.GetKeyDown(key))
            soundManager.PlaySound2D(testAudioEvent);
    }
}
