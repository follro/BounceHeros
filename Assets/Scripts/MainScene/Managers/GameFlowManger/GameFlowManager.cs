using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace BounceHeros
{
    public class GameFlowManager : MonoBehaviour, IInitializable
    {
        private GameFlowStateMachine stateMachine;
        
        [SerializeField] private GameInitializationDataSO gameInitializationData;
        public GameContext Context{ get; private set; }    
       
        
        public void Initialize()
        {
            Context = new GameContext();
            Context.PauseController = new PauseSystem();
            Context.LevelController = new LevelSystem(100);

            stateMachine = new GameFlowStateMachine(gameInitializationData, Context);
            
        }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            stateMachine.Update();

            //Å×½ºÆ® 
         /*   var keycode1 = KeyCode.Space;
            var keycode2 = KeyCode.B;

            if (Input.GetKeyDown(keycode1))
                stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveSetup);
            if(Input.GetKeyDown(keycode2))
                GameResume();*/
        }

        public void GamePause()
        {
            if(stateMachine.CurrentStateType != GameFlowStateMachine.GameFlowState.GamePause)
                stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.GamePause);
        }
        public void GameResume()
        {
            if(stateMachine.CurrentStateType == GameFlowStateMachine.GameFlowState.GamePause)
                stateMachine.RequestTransition(stateMachine.PreviousStateType);
        }
    }
}
