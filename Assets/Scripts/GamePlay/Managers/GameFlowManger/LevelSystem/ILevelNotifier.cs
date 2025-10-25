using BounceHeros;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelNotifier
{
    void Subscribe(ILevelObserver observer);
    void Unsubscribe(ILevelObserver observer);
    void NotifyLevelChanged(int level);


}
