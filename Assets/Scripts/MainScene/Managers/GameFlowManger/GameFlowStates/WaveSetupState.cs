
using Cysharp.Threading.Tasks;
using System;

namespace BounceHeros
{
    public class WaveSetupState : BaseGameFlowState
    {
        public WaveSetupState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public async override void Enter()
        {
            base.Enter();
            await UniTask.Delay(TimeSpan.FromSeconds(10));
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveRunning);
        }
    }
}