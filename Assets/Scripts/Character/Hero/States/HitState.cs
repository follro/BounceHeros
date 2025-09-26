using System.Collections;
using System.Collections.Generic;

namespace BounceHeros
{
    public class HitState : BaseHeroState
    {
        public HitState(BaseHero hero, HeroStateMachine stateMachine) : base(hero, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            hero.HeroAnimator.SetBool("IsHit", true);
        }

        public override void OnCollisionEnter(UnityEngine.Collision2D collision)
        {
            base.OnCollisionEnter(collision);

            stateMachine.RequestTransition(HeroStateMachine.HeroState.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            hero.HeroAnimator.SetBool("IsHit", false);
        }
    }
}