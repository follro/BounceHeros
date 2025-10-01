using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BounceHeros
{
    public class LevelManager : IInitializable
    {
        private readonly List<ILevelObserver> observers = new();
        public int CurrentLevel { get; private set; }
        public int MaxLevel { get; private set; }

        //public event Action<int> OnLevelChanged;
        public void Initialize()
        {
            CurrentLevel = 0;
        }

        public void OnLevelChange()
        {
            if (CurrentLevel < MaxLevel)
                CurrentLevel++;

            foreach (var observer in observers)
                observer.OnLevelChanged(CurrentLevel);
        }

        public void Subscribe(ILevelObserver observer) => observers.Add(observer);
        public void Unsubscribe(ILevelObserver observer) => observers.Remove(observer);

    }
}
