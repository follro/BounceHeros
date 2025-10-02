using BounceHeros;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    void Subscribe(ILevelObserver observer);
    void Unsubscribe(ILevelObserver observer);
    void OnLevelChange(int level);


}
