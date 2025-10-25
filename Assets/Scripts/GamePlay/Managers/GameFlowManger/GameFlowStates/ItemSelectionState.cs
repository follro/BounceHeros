
namespace BounceHeros 
{
    public class ItemSelectionState : BaseGameFlowState
    {
        public ItemSelectionState(GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveSetup);
        }
    }
}
