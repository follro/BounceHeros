using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class LevelInitializer : ILevelObserver
    {
        public ILevelNotifier Notifier { get; set; }

        //1. 현재 레벨 데이터 업데이트 ( 적 => 적 스포너를 알아야됨)
        //2. 
        public LevelInitializer(ILevelNotifier notifier)
        {
            if (notifier == null)
                Debug.LogError($"{this.GetType()}: {notifier.GetType()} is null");
            notifier.Subscribe(this);   
        }


        public void Dispose()
        {

        }

        public void OnLevelChanged(int newLevel)
        {
            
        }
    }
}