using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace BounceHeros
{
    public class GameFlowManager : MonoBehaviour, IInitializable
    {
        private GameFlowStateMachine stateMachine;


        public void Initialize()
        {
            stateMachine = new GameFlowStateMachine(this);
        }
        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            stateMachine.Update();
        }

        public void GamePause() => stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.GamePause);

        public void GameResume() => stateMachine.RequestTransition(stateMachine.PreviousStateType);
        
    }
}
