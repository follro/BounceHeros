using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public abstract class BaseHeroState : IState 
    {
        private HeroStateMachine stateMachine;
        private BaseHero hero;

        public BaseHeroState(BaseHero hero, HeroStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            this.hero = hero;
        }

        public virtual void Enter()
        {
            Debug.Log($"<color=green>{hero.name}</color> Enter. Current State: {this.GetType().Name}");
        }

        public virtual void Exit()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }
        public virtual void OnCollisionEnter()
        {
            Debug.Log($"<color=green>{hero.name}</color> CollisionEnter. Current State: {this.GetType().Name}");
        }

    }
}