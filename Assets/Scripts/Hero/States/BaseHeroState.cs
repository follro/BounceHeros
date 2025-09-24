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
            Debug.Log($"{hero.name} <color=green>Enter</color>. Current State: <color=yellow>{this.GetType().Name}</color>.");
            
        }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnCollisionEnter(Collision2D collision)
        {
            Debug.Log($"{hero.name} <color=yellow>CollisionEnter</color>. Current State: <color=yellow>{this.GetType().Name}</color>");
        }

    }
}