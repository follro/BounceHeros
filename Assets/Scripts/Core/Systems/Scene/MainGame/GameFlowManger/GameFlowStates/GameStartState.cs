
using System.Transactions;

namespace BounceHeros
{
    public class GameStartState : BaseGameFlowState
    {
        public GameStartState(GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
        }

        public override  void Enter()
        {
            base.Enter();
            //await gameFlowManager.gameInitiator.GameSetting();
            
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.ItemSelection);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
