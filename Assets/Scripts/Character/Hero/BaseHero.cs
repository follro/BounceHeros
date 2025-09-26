using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace BounceHeros
{
    public class BaseHero : Character
    {
        [SerializeField] private LayerMask hitableLayerMask;

        [SerializeField] private float baseDamage;
        [SerializeField] private int rushableCount;
        [SerializeField] private float rushDamageMultiplier;
        

        private HeroStateMachine stateMachine;

        #region Property
        public Rigidbody2D Rigid2D { get; private set; }

        public Animator HeroAnimator { get; private set; }
        public float MinRushForce { get; private set; }
        public float RushSpeed { get; private set; }
        public float RushDamageMultiplier { get => rushDamageMultiplier; }
        public Vector2 RushDirection { get; private set; }
        public int RushableCount { get => rushableCount; }  

        public LayerMask HitableLayerMask { get => hitableLayerMask; }

        #endregion

        private void Awake()
        {
            Rigid2D = GetComponent<Rigidbody2D>();
            HeroAnimator = GetComponentInChildren<Animator>();

            Initialize(null);
        }

        public void Initialize(HeroDataSO heroData)
        {
            stateMachine = new HeroStateMachine(this);
            hp = maxHP;
            totalDamage = baseDamage;
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
    }
}