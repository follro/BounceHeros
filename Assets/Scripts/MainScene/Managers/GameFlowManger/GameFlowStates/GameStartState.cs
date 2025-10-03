
using System.Transactions;

namespace BounceHeros
{
    public class GameStartState : BaseGameFlowState
    {
        public GameStartState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.ItemSelection);
        }
    }
}
