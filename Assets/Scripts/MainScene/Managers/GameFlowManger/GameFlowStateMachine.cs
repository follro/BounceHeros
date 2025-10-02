using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    public class GameFlowStateMachine
    {
        #region StateSetting
        public enum GameFlowState
        {
            SelectItem,
            WaveSetting,
            WaveStart,
            WaveOver,

            GamePause,
            GameOver,
            End
        }

        #endregion
        public GameFlowState CurrentStateType { get; private set; }
        private HakSeung.Util.StateEnumArray<BaseGameFlowState, GameFlowState> states;
        private BaseGameFlowState currentState;

        public GameFlowStateMachine(GameFlowManager gameFlowManager)
        {
            states = new HakSeung.Util.StateEnumArray<BaseGameFlowState, GameFlowState>();


            TransitionTo(GameFlowState.WaveSetting);
        }

        private void TransitionTo(GameFlowState nextStateType)
        {
            currentState?.Exit();
            currentState = states[nextStateType];
            currentState?.Enter();

            CurrentStateType = nextStateType;
        }

        public void Update() => currentState?.Update();

    }
}