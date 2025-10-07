using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class GamePauseState : BaseGameFlowState
    {
        public GamePauseState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            gameFlowManager.pauseSystem.Pause();
        }

        public override void Exit()
        {
            base.Exit();   
            gameFlowManager.pauseSystem.Resume();
        }


    }
}