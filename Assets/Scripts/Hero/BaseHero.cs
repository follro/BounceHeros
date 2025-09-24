using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BounceHeros
{
    public class BaseHero : MonoBehaviour, IHitable, IAttackable
    {
        [SerializeField] private LayerMask hitableLayerMask;

        [SerializeField] private float maxHP;
        [SerializeField] private float hp;
        [SerializeField] private int rushableCount;
        [SerializeField] private float attackDamage;
        [SerializeField] private float rushDamageMultiplier;

        private HeroStateMachine stateMachine;

        #region Events
        //나중에 이벤트 넣어서 구현
        public event Action<float> OnHitEvent;
        public event Action OnAttackEvent;
        #endregion

        #region Property
        public Rigidbody2D Rigid2D { get; private set; }
        float IHitable.MaxHP { get => maxHP; }
        float IHitable.HP { get => hp; }
        float IAttackable.AttackDamage { get => attackDamage; }
        public float MinRushForce { get; private set; }
        public float RushSpeed { get; private set; }
        public Vector2 RushDirection { get; private set; }
        public int RushableCount { get => rushableCount; }  

        public LayerMask HitableLayerMask { get => hitableLayerMask; }

        #endregion

        private void Awake()
        {
            Rigid2D = GetComponent<Rigidbody2D>();
            stateMachine = new HeroStateMachine(this);
            hp = maxHP;

            //임시 값 들
            MinRushForce = 10;
        }

        private void Update() => stateMachine.Update();
        private void FixedUpdate() => stateMachine.FixedUpdate();
        private void LateUpdate() => stateMachine.LateUpdate();
        private void OnCollisionEnter2D(Collision2D collision) => stateMachine.StateOnCollisionEnter(collision);

        public void Catched() => stateMachine.RequestTransition(HeroStateMachine.HeroState.Catched);
        public void Rush(Vector2 direction, float forceAmount)
        {
            RushSpeed = forceAmount + MinRushForce;
            RushDirection = direction;
            stateMachine.RequestTransition(HeroStateMachine.HeroState.Rush);
        }

        void IHitable.OnHit(float damage)
        {
            hp -= damage;
            if(hp < 0) hp = 0;
            OnHitEvent?.Invoke(damage);
        }

        public void Attack(IHitable hitableObject)
        {
            switch(stateMachine.CurrentStateType)
            {
                case HeroStateMachine.HeroState.Idle:
                    hitableObject.OnHit(attackDamage);
                    break;
                case HeroStateMachine.HeroState.Rush:
                    hitableObject.OnHit(attackDamage * rushDamageMultiplier);
                    break;
            }
            OnAttackEvent?.Invoke();
        }
    }
}