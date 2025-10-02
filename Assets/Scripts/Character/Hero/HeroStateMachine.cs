using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace BounceHeros
{
    public class HeroStateMachine
    {
        #region StateSetting
        public enum HeroState
        {
            Idle,
            Catched,
            Rush,
            Hit,
            Dead,

            //추가 상태들 추후 추가 필요
            //skill 1
            //skill 2
            End
        }

        
        #endregion

        public HeroState CurrentStateType { get; private set; }
        private HakSeung.Util.StateEnumArray<BaseHeroState, HeroState> states;
        private BaseHeroState currentState;
        private BaseHeroState nextState;
        private HeroState pendingStateType;
        private bool isTransitionPending;

        public HeroStateMachine(BaseHero hero, HeroState startState)
        {
            currentState = null;
            isTransitionPending = false;
            pendingStateType = HeroState.End;

            states = new HakSeung.Util.StateEnumArray<BaseHeroState, HeroState>((int)HeroState.End);
            states[HeroState.Idle] = new IdleState(hero, this);
            states[HeroState.Catched] = new CatchedState(hero, this);
            states[HeroState.Rush] = new RushState(hero, this);
            states[HeroState.Hit] = new HitState(hero, this);

            TransitionTo(startState);
            //states[HeroState.Dead] = new DeadState(hero, this);*/

        }

        public void RequestTransition(HeroState nextStateType)
        {
            if (states[nextStateType] == null || currentState == states[nextStateType]) return;
            isTransitionPending = true;
            pendingStateType = nextStateType;
        }

        private void TransitionTo(HeroState nextStateType)
        {
            currentState?.Exit();
            currentState = states[nextStateType];
            currentState?.Enter();

            CurrentStateType = nextStateType;
        }

        public void Update() =>  currentState?.Update();
        public void FixedUpdate() => currentState?.FixedUpdate();
        public void LateUpdate()
        {
            currentState?.LateUpdate();

            if (isTransitionPending)
            {
                TransitionTo(pendingStateType);
                isTransitionPending = false;
            }
        }

        public void StateOnCollisionEnter(UnityEngine.Collision2D collision) => currentState?.OnCollisionEnter(collision); 
    }
}
