using BounceHeros.Enemy;
using HakSeung.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BounceHeros.HeroStateMachine;

namespace BounceHeros
{

    public class EnemyStateMachine 
    {
        public EnemyState CurrentStateType { get; private set; }
        private Dictionary<EnemyState, BaseEnemyState> states;
        private BaseEnemyState currentState;
        private BaseEnemyState pendingState;
        private bool isTransitionPending;

        public EnemyStateMachine(BaseEnemy hero, BaseEnemyState startState)
        {
            currentState = null;
            pendingState = null;
            isTransitionPending = false;

            TransitionTo(startState);
        }

        public bool HasState(EnemyState state)
        {
            return states.ContainsKey(state);
        }



        public void RequestTransition(BaseEnemyState nextState)
        {
            //if (states[nextStateType] == null || currentState == states[nextStateType]) return;
            if (pendingState == nextState) return;

            if (!HasState(nextState.StateType))
                Debug.LogError("해당 상태를 보유하고 있지 않습니다");
            
            isTransitionPending = true;
            pendingState = nextState;
        }

        private void TransitionTo(BaseEnemyState nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState?.Enter();

            CurrentStateType = nextState.StateType;
        }

        public void Update() => currentState?.Update();
        public void FixedUpdate() => currentState?.FixedUpdate();
        public void LateUpdate()
        {
            currentState?.LateUpdate();

            if (isTransitionPending)
            {
                TransitionTo(pendingState);
                isTransitionPending = false;
            }
        }

        public void StateOnCollisionEnter(UnityEngine.Collision2D collision) => currentState?.OnCollisionEnter(collision);

    }
}