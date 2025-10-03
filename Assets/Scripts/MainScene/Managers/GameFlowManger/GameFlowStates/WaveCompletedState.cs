
namespace BounceHeros
{
    public class WaveCompletedState : BaseGameFlowState
    {
        public WaveCompletedState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.GameOver);
        }
    }
}