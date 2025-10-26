using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundManager 
{
    void PlaySound2D(AudioEventSO eventToPlay);
    void PlaySound3D(AudioEventSO eventToPlay, Vector3 position);

}
