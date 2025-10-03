
using UnityEditor;

namespace BounceHeros
{
    public class WaveRunningState : BaseGameFlowState
    {
        public WaveRunningState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveCompleted);
        }
    }
}