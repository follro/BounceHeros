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

            // ���ϴ� �ӵ�(hero.RushSpeed)�� ���� �ణ�̶� �ٸ���, �ӵ��� 0�� �ƴ϶�� (�������� �ʴٸ�)
            // �߿�: float�� �̼��� ������ ���� �� �����Ƿ� '==' �� ��� Mathf.Approximately ���
            if (currentSpeed > 0 && !Mathf.Approximately(currentSpeed, hero.RushSpeed))
            {
                // ���� ���� ����(velocity.normalized)�� ������ ä�� �ӷ¸� ����
                hero.Rigid2D.velocity = hero.Rigid2D.velocity.normalized * hero.RushSpeed;
            }

        }

        public override void OnCollisionEnter(Collision2D collision)
        {
            base.OnCollisionEnter(collision);

            int layer = collision.gameObject.layer;

            // Hitable ������Ʈ ó��
            if (((hero.HitableLayerMask.value & (1 << layer)) > 0) &&
                collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
            {
                HandleIHitableCollision(hitable);
            }
            else // Hitable�� �ƴ� �Ϲ� �� ��� �ε����� ��
            {
                // ���� Rush Ƚ���� ���ҽ�Ű��, Idle ���·� ��ȯ���� ����
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