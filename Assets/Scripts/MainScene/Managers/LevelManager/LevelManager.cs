using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BounceHeros
{
    public class LevelManager : ILevelChangeNotifier, IInitializable
    {
        //게임 메인 씬 로직. 라운드 종료 -> 아이템 선택창 -> 레벨 업 => 상태로 정의 가능 (특수 상태는 일시정지 => 기존 상태를 기억해야됨, 게임 오버)
        //레벨이 변하면  1. 레벨 UI 업데이트, 2. 새로운 스포닝 데이터 세팅 후 스폰 시작    

        private readonly List<ILevelObserver> observers = new();
        public int CurrentLevel { get; private set; }
        public int MaxLevel { get; private set; }

        public LevelManager(int maxLevel)
        {
            MaxLevel = maxLevel;
        }

        public void Initialize()
        {
            CurrentLevel = 0;
        }

        public void Subscribe(ILevelObserver observer) => observers.Add(observer);
        public void Unsubscribe(ILevelObserver observer) => observers.Remove(observer);

        public void NotifyLevelChanged(int nextLevel)
        {
            if (nextLevel < MaxLevel)
                CurrentLevel = nextLevel;

            foreach (var observer in observers)
                observer.OnLevelChanged(nextLevel);
        }

        
    }
}
