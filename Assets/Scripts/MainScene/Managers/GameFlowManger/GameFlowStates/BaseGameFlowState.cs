using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    public abstract class BaseGameFlowState : HakSeung.Util.IState
    {
        private GameFlowManager gameFlowManager;
        private GameFlowStateMachine gameFlowStateMachine;

        public BaseGameFlowState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine)
        {
            this.gameFlowManager = gameFlowManager;
            this.gameFlowStateMachine = gameFlowStateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public virtual  void Exit()
        {

        }

        public virtual void Update()
        {

        }
    }
}