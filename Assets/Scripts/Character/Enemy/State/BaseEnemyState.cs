using BounceHeros.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class BaseEnemyState : HakSeung.Util.IState
    {
        public EnemyState StateType { get; }
        protected EnemyStateMachine stateMachine;
        protected BaseEnemy enemy;
        public BaseEnemyState(EnemyStateMachine stateMachine, BaseEnemy enemy)
        {
            this.stateMachine = stateMachine;
            this.enemy = enemy;
        }
        public void Enter() { }
        public void Exit() { }
        public void Update() { }

        public void FixedUpdate() { }
        public virtual void LateUpdate() { }
        public virtual void OnCollisionEnter(UnityEngine.Collision2D collision) { }
    }
}