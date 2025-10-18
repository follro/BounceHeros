
using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class WaveSetupState : BaseGameFlowState
    {
        private LevelSystem levelSystem;
        public WaveSetupState(GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
            levelSystem = gameContext.LevelController;
        }

        public override void Enter()
        {
            base.Enter();
            levelSystem.NotifyLevelChanged(levelSystem.CurrentLevel + 1);
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveRunning);
        }
    }
}