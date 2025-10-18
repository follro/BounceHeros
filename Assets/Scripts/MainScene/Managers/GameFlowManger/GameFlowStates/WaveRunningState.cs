
using UnityEditor;

namespace BounceHeros
{
    public class WaveRunningState : BaseGameFlowState
    {
        public WaveRunningState(GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveCompleted);
        }
    }
}