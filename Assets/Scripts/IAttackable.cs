using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public interface IAttackable 
    {
        public event Action OnAttackEvent;
        event Action<float> OnHitEvent;

        public float AttackDamage { get; }
        public void Attack(IHitable hitableObject);
    }
}