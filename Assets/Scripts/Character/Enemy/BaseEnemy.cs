using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BounceHeros
{
    public class BaseEnemy : Character
    {
        private void Awake()
        {
            hp = MaxHP;
            OnHitEvent += (damage) => { spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo); };
            OnHitEvent += (damage) => { if (HP <= 0) gameObject.SetActive(false); };
        }





    }
}