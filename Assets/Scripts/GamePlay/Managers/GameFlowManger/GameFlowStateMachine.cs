using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BounceHeros.HeroStateMachine;


namespace BounceHeros
{
    public class GameFlowStateMachine
    {
        #region StateSetting
        public enum GameFlowState
        {
            GameInitialize,

            ItemSelection,
            WaveSetup,
            WaveRunning,
            WaveCompleted,

            GameStart,
            GamePause,
            GameOver,
            End
        }

        #endregion
        public GameFlowState CurrentStateType { get; private set; }
        public GameFlowState PreviousStateType { get; private set; }
        
        private HakSeung.Util.StateEnumArray<BaseGameFlowState, GameFlowState> states;
        private BaseGameFlowState currentState;
        private Queue<GameFlowState> pendingTransitions;

        public GameFlowStateMachine(GameInitializationDataSO initializationDataSO, GameContext gameContext)
        {
            pendingTransitions = new Queue<GameFlowState>();
            states = new HakSeung.Util.StateEnumArray<BaseGameFlowState, GameFlowState>();

            states[GameFlowState.GameInitialize]    = new InitializeState(initializationDataSO, gameContext, this);    

            states[GameFlowState.GameStart]         = new GameStartState(gameContext, this);
            
            states[GameFlowState.ItemSelection]     = new ItemSelectionState(gameContext, this);
            states[GameFlowState.WaveSetup]         = new WaveSetupState(gameContext, this);
            states[GameFlowState.WaveRunning]       = new WaveRunningState(gameContext, this);
            states[GameFlowState.WaveCompleted]     = new WaveCompletedState(gameContext, this);

            states[GameFlowState.GamePause]         = new GamePauseState(gameContext, this);
            states[GameFlowState.GameOver]          = new GameEndState(gameContext, this);

            TransitionTo(GameFlowState.GameInitialize);
        }

        public void RequestTransition(GameFlowState nextStateType)
        {
            if (states[nextStateType] == null || currentState == states[nextStateType]) return;
            pendingTransitions.Enqueue(nextStateType);
        }

        private void TransitionTo(GameFlowState nextStateType)
        {
            PreviousStateType = CurrentStateType;

            currentState?.Exit();
            currentState = states[nextStateType];
            currentState?.Enter();

            CurrentStateType = nextStateType;
        }


        public void Update()
        {
            currentState?.Update();
            if(pendingTransitions.Count > 0)
                TransitionTo(pendingTransitions.Dequeue());
        }


    }
}