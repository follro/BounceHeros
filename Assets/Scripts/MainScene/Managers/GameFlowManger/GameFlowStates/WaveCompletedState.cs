
namespace BounceHeros
{
    public class WaveCompletedState : BaseGameFlowState
    {
        public WaveCompletedState(GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.GameOver);
        }
    }
}