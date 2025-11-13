using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace BounceHeros
{
    public abstract class BaseHeroState : HakSeung.Util.IState 
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
            
            
        }

        public virtual void Exit() { }

        public virtual void Update() { }
        
        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnCollisionEnter(UnityEngine.Collision2D collision)
        {
            
        }

    }
}