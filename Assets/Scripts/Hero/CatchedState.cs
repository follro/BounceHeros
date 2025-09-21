using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class CatchedState : BaseHeroState
    {
        public CatchedState(BaseHero hero, HeroStateMachine stateMachine) : base(hero, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            hero.Rigid2D.isKinematic = true;
        }

        public override void Exit()
        {
            base.Exit();
            hero.Rigid2D.isKinematic = false;
        }

        public override void Update() 
        {
            base.Update(); 
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }
    }
}