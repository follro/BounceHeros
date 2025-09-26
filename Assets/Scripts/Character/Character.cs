using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public abstract class Character : MonoBehaviour, IHitable, IAttackable
    {
        [SerializeField] protected float maxHP;
        [SerializeField] protected float hp;
        [SerializeField] protected float totalDamage;
        [SerializeField] protected SpriteRenderer spriteRenderer;

        public event Action<IHitable> OnAttackEvent;
        public event Action<float> OnHitEvent;

        #region Property
        public float AttackDamage { get => totalDamage; }

        public float MaxHP { get => maxHP; }

        public float HP { get => hp; }

        #endregion

        public virtual void Attack(IHitable hitableObject, float damage)
        {
            hitableObject.OnHit(totalDamage);
            OnAttackEvent?.Invoke(hitableObject);
        }

        void IHitable.OnHit(float damage)
        {
            hp -= damage;
            if (hp <= 0)
                hp = 0;
            OnHitEvent?.Invoke(damage);
        }
    }
}
