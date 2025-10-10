using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class GamePauseState : BaseGameFlowState
    {
        private PauseSystem pauseSystem;
        public GamePauseState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
            pauseSystem = gameFlowManager.PauseController;
        }

        public override void Enter()
        {
            base.Enter();
            pauseSystem.Pause();
        }

        public override void Exit()
        {
            base.Exit();
            pauseSystem.Resume();
        }


    }
}