using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BounceHeros
{
    public abstract class BaseGameFlowState : HakSeung.Util.IState
    {
        protected GameFlowManager gameFlowManager;
        protected GameFlowStateMachine stateMachine;

        public BaseGameFlowState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine)
        {
            this.gameFlowManager = gameFlowManager;
            this.stateMachine = gameFlowStateMachine;
        }

        public async virtual void Enter()
        {
            UnityEngine.Debug.Log($"{GetType(): Enter}");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        public virtual  void Exit()
        {

        }

        public virtual void Update()
        {

        }
    }
}