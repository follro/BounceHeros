using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class LevelInitializer : ILevelObserver
    {
        //1. 현재 레벨 데이터 업데이트 ( 적 => 적 스포너를 알아야됨)
        //2. 
        public LevelInitializer(ILevelNotifier notifier)
        {
            notifier.Subscribe(this);   
        }

        public void OnLevelChanged(int newLevel)
        {
            
        }
    }
}