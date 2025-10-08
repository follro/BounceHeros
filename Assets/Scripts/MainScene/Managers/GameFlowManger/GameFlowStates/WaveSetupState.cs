
using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class WaveSetupState : BaseGameFlowState
    {
        public WaveSetupState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            gameFlowManager.levelSystem.NotifyLevelChanged(gameFlowManager.levelSystem.CurrentLevel + 1);
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveRunning);
        }
    }
}