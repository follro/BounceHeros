using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class RushState : BaseHeroState
    {
        private float originalGravityScale;
        private Vector2 currentRushDirection;
        private int currentRushableCount;

        public RushState(BaseHero hero, HeroStateMachine stateMachine) : base(hero, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            originalGravityScale = hero.Rigid2D.gravityScale;
            hero.Rigid2D.gravityScale = 0;
            currentRushableCount = hero.RushableCount;

            hero.Rigid2D.velocity = hero.RushDirection * hero.RushSpeed;
            hero.HeroAnimator.SetBool("IsRushState", true);
        }

        public override void Exit() 
        { 
            base.Exit();
            hero.Rigid2D.gravityScale = originalGravityScale;
            hero.HeroAnimator.SetBool("IsRushState", false);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            float currentSpeed = hero.Rigid2D.velocity.magnitude;
            Vector2 moveDirection = hero.Rigid2D.velocity.normalized;
            if (currentSpeed > 0 && !Mathf.Approximately(currentSpeed, hero.RushSpeed))
            {
                //Vector2 moveDirection = hero.Rigid2D.velocity.normalized;
                hero.Rigid2D.velocity = moveDirection * hero.RushSpeed;

            }
                hero.HeroAnimator.SetFloat("MoveDirX", moveDirection.x);
                hero.HeroAnimator.SetFloat("MoveDirY", moveDirection.y);

        }

        public override void OnCollisionEnter(Collision2D collision)
        {
            base.OnCollisionEnter(collision);

            int layer = collision.gameObject.layer;

            // Hitable 오브젝트 처리
            if (((hero.HitableLayerMask.value & (1 << layer)) > 0) &&
                collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
            {
                HandleIHitableCollision(hitable);
            }
            else 
            {
                currentRushableCount--;
                if (currentRushableCount <= 0)
                {
                    stateMachine.RequestTransition(HeroStateMachine.HeroState.Idle);
                }
            }
        }

        private void HandleIHitableCollision(IHitable enemy)
        {
            hero.Attack(enemy, hero.AttackDamage);
        }

     
    }
}