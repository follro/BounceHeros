using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class RushState : BaseHeroState
    {
        private float originalGravityScale;
        private Vector2 lastVelocity;
        public RushState(BaseHero hero, HeroStateMachine stateMachine) : base(hero, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            originalGravityScale = hero.Rigid2D.gravityScale;
            hero.Rigid2D.gravityScale = 0;
        }

        public override void Exit() 
        { 
            base.Exit();
            hero.Rigid2D.gravityScale = originalGravityScale;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            hero.Rigid2D.velocity = hero.RushDirection.normalized * hero.RushSpeed;
        }

        public override void OnCollisionEnter(Collision2D collision)
        {
            base.OnCollisionEnter(collision);
            
            int layer = collision.gameObject.layer;

            if (((hero.HitableLayerMask.value & (1 << layer)) > 0) &&
                    collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
            {
                HandleIHitableCollision(hitable);
            }
            else
                HandleReflection(collision);

        }

        private void HandleIHitableCollision(IHitable enemy)
        {
            enemy.OnHit(10);// 나중에 데미지 넣어줘야됨
        }

        private void HandleReflection(Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;

            Vector2 reflectDirection = Vector2.Reflect(lastVelocity.normalized, normal);

            hero.Rigid2D.velocity = reflectDirection * hero.RushSpeed;
        }
    }
}