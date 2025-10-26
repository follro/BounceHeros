using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BounceHeros
{
    public class IdleState : BaseHeroState
    {
        public IdleState(BaseHero hero, HeroStateMachine stateMachine) : base(hero, stateMachine)
        {
        }

        public override void Exit()
        {
            base.Exit();
            hero.HeroAnimator.SetBool("IsHit", false); //임시

        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnCollisionEnter(Collision2D collision)
        {
            base.OnCollisionEnter(collision);

            int layer = collision.gameObject.layer;
            hero.HeroAnimator.SetBool("IsHit", false); //임시

            if (((hero.HitableLayerMask.value & (1 << layer)) > 0) )
            {
                if(collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
                    HandleIHitableCollision(hitable);
                if (collision.gameObject.TryGetComponent<IAttackable>(out IAttackable attackable))
                    HandleIAttackableCollision(attackable, collision.transform.position);
            }
        }

        private void HandleIHitableCollision(IHitable enemy)
        {
            hero.Attack(enemy, hero.AttackDamage * hero.RushDamageMultiplier);
            hero.HeroAnimator.SetBool("IsHit", true); // 임시 
        }
        private void HandleIAttackableCollision(IAttackable enemy, Vector3 enemyPos)
        {
            Vector3 heroPos = hero.transform.position;

            Vector2 knockbackDirection = (heroPos - enemyPos).normalized;

            Vector2 powerVector = knockbackDirection * enemy.AttackDamage;

            hero.Rigid2D.AddForce(powerVector, ForceMode2D.Impulse);
        }


    }
}