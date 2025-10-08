using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace BounceHeros
{
    public class GameFlowManager : MonoBehaviour, IInitializable
    {
        //각 상태에 필요한 것들을 만들어야됨
        //1. 정지기능
        //2. 웨이브 관리자 (웨이브 세팅 관련 정보들이 필요함)
        //3. 씬 로더(특정 버튼에만 필요하므로 필요없을까나)
        //4. 

        private GameFlowStateMachine stateMachine;
        
        public GameInitiator gameInitiator;
        
        public PauseSystem pauseSystem;

        public LevelSystem levelSystem;
        [SerializeField] private LevelTextUI levelTextUI;
       

        public void Initialize()
        {
            stateMachine = new GameFlowStateMachine(this);
            pauseSystem = new PauseSystem();

            levelSystem = new LevelSystem(100);
            levelSystem.Subscribe(levelTextUI);
        }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            stateMachine.Update();

            //테스트 
            var keycode1 = KeyCode.Space;
            var keycode2 = KeyCode.B;

            if (Input.GetKeyDown(keycode1))
                stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.WaveSetup);
            if(Input.GetKeyDown(keycode2))
                GameResume();
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
