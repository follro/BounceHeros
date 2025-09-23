using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class BaseHero : MonoBehaviour, IHitable
    {
        [SerializeField] private LayerMask hitableLayerMask;
        [SerializeField] private float maxHP;
        [SerializeField] private float hp;
        [SerializeField] private int rushableCount;

        private HeroStateMachine stateMachine;
        public event Action<float> OnHitEvent;

        #region Property
        public Rigidbody2D Rigid2D { get; private set; }
        float IHitable.MaxHP { get => maxHP; }
        float IHitable.HP { get => hp; }
        public float MinSpeed { get; private set; }
        public float RushSpeed { get; private set; }
        public Vector2 RushDirection { get; private set; }
        public int RushableCount { get => rushableCount; }  

        public LayerMask HitableLayerMask { get => hitableLayerMask; }

       event Action<float> IHitable.OnHitEvent
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        } //���߿� �̺�Ʈ �߰�

        #endregion


        private void Awake()
        {
            Rigid2D = GetComponent<Rigidbody2D>();
            stateMachine = new HeroStateMachine(this);
            hp = maxHP;

            //�ӽ� �� ��
            MinSpeed = 10;
        }

        private void Update() => stateMachine.Update();
        private void FixedUpdate() => stateMachine.FixedUpdate();
        private void LateUpdate() => stateMachine.LateUpdate();
        private void OnCollisionEnter2D(Collision2D collision) => stateMachine.StateOnCollisionEnter(collision);

        public void Catched() => stateMachine.RequestTransition(HeroStateMachine.HeroState.Catched);
        public void Rush(Vector2 direction, float forceAmount)
        {
            RushSpeed = forceAmount + MinSpeed;
            RushDirection = direction;
            stateMachine.RequestTransition(HeroStateMachine.HeroState.Rush);
        }

        void IHitable.OnHit(float damage)
        {
            hp -= damage;
            if(hp < 0) hp = 0;      
            //OnHitEvent(damage)
        }
    }
}