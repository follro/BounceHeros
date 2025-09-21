using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BounceHeros
{
    public abstract class BaseHeroState : IState 
    {
        protected HeroStateMachine stateMachine;
        protected BaseHero hero;

        public BaseHeroState(BaseHero hero, HeroStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            this.hero = hero;
        }

        public virtual void Enter()
        {
            Debug.Log($"<color=green>{hero.name}</color> Enter. Current State: {this.GetType().Name}");
            
        }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"<color=green>{hero.name}</color> CollisionEnter. Current State: {this.GetType().Name}");
        }

    }
}