using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public interface ILevelObserver
    {
        UniTask OnLevelChanged(int newLevelNumber);
    }
}
