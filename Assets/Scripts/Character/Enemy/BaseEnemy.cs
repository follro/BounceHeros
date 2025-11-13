using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BounceHeros.Enemy
{
    public enum EnemyState
    {
        Idle,
        

    }

    public class BaseEnemy : Character
    {
        [SerializeField] EnemyType enemyType;
        //원하는 상태를 추가해서 사용할 수 있도록 만든 상태 패턴 BaseEnemy에서는 공통적인 것만 할당하고 하위 클래스에서 원하는 클래스를 할당시키기


        private void Awake()
        {
            hp = MaxHP;
            //OnHitEvent += (damage) => { if (HP <= 0) gameObject.SetActive(false); };
        }




    }
}