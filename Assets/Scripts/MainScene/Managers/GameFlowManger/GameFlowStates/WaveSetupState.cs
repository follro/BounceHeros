
using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class WaveSetupState : BaseGameFlowState
    {
        private LevelSystem levelSystem;
        public WaveSetupState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
            levelSystem = gameFlowManager.LevelController;
        }

        public override void Enter()
        {
            base.Enter();
            levelSystem.NotifyLevelChanged(levelSystem.CurrentLevel + 1);
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveRunning);
        }
    }
}