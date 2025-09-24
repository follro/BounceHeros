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
           // currentRushDirection = hero.RushDirection;

            hero.Rigid2D.velocity = hero.RushDirection * hero.RushSpeed;
        }

        public override void Exit() 
        { 
            base.Exit();
            hero.Rigid2D.gravityScale = originalGravityScale;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            float currentSpeed = hero.Rigid2D.velocity.magnitude;

            // 원하는 속도(hero.RushSpeed)와 아주 약간이라도 다르고, 속도가 0이 아니라면 (멈춰있지 않다면)
            // 중요: float는 미세한 오차가 있을 수 있으므로 '==' 비교 대신 Mathf.Approximately 사용
            if (currentSpeed > 0 && !Mathf.Approximately(currentSpeed, hero.RushSpeed))
            {
                // 현재 진행 방향(velocity.normalized)을 유지한 채로 속력만 보정
                hero.Rigid2D.velocity = hero.Rigid2D.velocity.normalized * hero.RushSpeed;
            }

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
            else // Hitable이 아닌 일반 벽 등과 부딪혔을 때
            {
                // 남은 Rush 횟수만 감소시키고, Idle 상태로 전환할지 결정
                currentRushableCount--;
                if (currentRushableCount <= 0)
                {
                    stateMachine.RequestTransition(HeroStateMachine.HeroState.Idle);
                }
            }
        }

        private void HandleIHitableCollision(IHitable enemy)
        {
            hero.Attack(enemy);
        }

     
    }
}