
namespace BounceHeros 
{
    public class ItemSelectionState : BaseGameFlowState
    {
        public ItemSelectionState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveSetup);
        }
    }
}
