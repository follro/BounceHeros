
using System.Transactions;

namespace BounceHeros
{
    public class GameStartState : BaseGameFlowState
    {
        public GameStartState(GameFlowManager gameFlowManager, GameFlowStateMachine gameFlowStateMachine) : base(gameFlowManager, gameFlowStateMachine)
        {
        }

        public override async void Enter()
        {
            base.Enter();
            await gameFlowManager.gameInitiator.GameSetting();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.ItemSelection);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
