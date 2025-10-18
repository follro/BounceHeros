using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public interface ILevelObserver : IDisposable
    {
        ILevelNotifier Notifier { get; set; }
        void OnLevelChanged(int newLevel);
    }
}
