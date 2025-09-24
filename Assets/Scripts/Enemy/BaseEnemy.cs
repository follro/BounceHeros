using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class BaseEnemy : MonoBehaviour, IAttackable, IHitable
    {
        [SerializeField] private float maxHP;
        [SerializeField] private float hp;
        [SerializeField] private float attackDamage;

        public event Action OnAttackEvent;
        public event Action<float> OnHitEvent;

        #region Property
        public float AttackDamage { get => attackDamage; }

        public float MaxHP { get => maxHP; }

        public float HP { get => hp; }
        #endregion
        private void Awake()
        {
            hp = maxHP;
        }

        public void Attack(IHitable hitableObject)
        {
            hitableObject.OnHit(attackDamage);
            OnAttackEvent?.Invoke();
        }

        public void OnHit(float damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                gameObject.SetActive(false);
            }
            OnHitEvent?.Invoke(damage);
        }
    }
}