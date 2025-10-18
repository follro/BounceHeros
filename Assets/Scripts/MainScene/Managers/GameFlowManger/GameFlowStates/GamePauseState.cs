using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class GamePauseState : BaseGameFlowState
    {
        private PauseSystem pauseSystem;
        public GamePauseState(GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
            pauseSystem = gameContext.PauseController;
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