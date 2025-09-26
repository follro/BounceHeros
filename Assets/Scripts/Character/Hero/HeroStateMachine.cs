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

            End
        }

        private class StateEnumArray
        {
            private readonly BaseHeroState[] states;
            public BaseHeroState this[HeroState key]
            {
                get => states[(int)key];
                set => states[(int)key] = value;
            }

            public BaseHeroState this[int key]
            {
                get => states[key];
                set => states[key] = value;
            }

            public StateEnumArray(int size)
            {
                states = new BaseHeroState[size];
            }

        }
        #endregion

        public HeroState CurrentStateType { get; private set; }
        private StateEnumArray states;
        private BaseHeroState currentState;
        private BaseHeroState nextState;
        private HeroState pendingStateType;

        public HeroStateMachine(BaseHero hero)
        {
            currentState = null;
            pendingStateType = HeroState.End;

            states = new StateEnumArray((int)HeroState.End);
            states[HeroState.Idle] = new IdleState(hero, this);
            states[HeroState.Catched] = new CatchedState(hero, this);
            states[HeroState.Rush] = new RushState(hero, this);
            states[HeroState.Hit] = new HitState(hero, this);


            TransitionTo(HeroState.Idle);
            //states[HeroState.Dead] = new DeadState(hero, this);*/

        }

        public void RequestTransition(HeroState nextStateType)
        {
            if (states[nextStateType] == null || currentState == states[nextStateType]) return;

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

            if (pendingStateType != HeroState.End)
            {
                TransitionTo(pendingStateType);
                pendingStateType = HeroState.End;
            }
        }

        public void StateOnCollisionEnter(UnityEngine.Collision2D collision) => currentState?.OnCollisionEnter(collision); 
    }
}
