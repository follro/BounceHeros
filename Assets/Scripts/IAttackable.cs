using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public interface IAttackable 
    {
        public event Action<IHitable> OnAttackEvent;

        public float AttackDamage { get; }
        public void Attack(IHitable hitableObject, float damage);
    }
}