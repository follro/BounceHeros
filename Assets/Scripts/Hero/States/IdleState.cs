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

        public override void Update()
        {
            base.Update();

            //임시 코드
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                hero.Rigid2D.velocity = new Vector2(0, 0);
                hero.transform.position = new Vector3(0,8,0);   
            }
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
        }

        private void HandleIHitableCollision(IHitable enemy)
        {
            hero.Attack(enemy);
        }
    }
}