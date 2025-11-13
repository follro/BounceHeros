using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class InitializeState : BaseGameFlowState
    {
        private GameContext gameContext;
        private GameInitializationDataSO initializationData;
        private LoadingScreenController loadingScreenController;

        public InitializeState(GameInitializationDataSO initializationData, GameContext gameContext, GameFlowStateMachine gameFlowStateMachine) : base(gameContext, gameFlowStateMachine)
        {
            this.initializationData = initializationData;
            this.gameContext = gameContext;
        }

        public override async void Enter()
        {
            base.Enter();
            BindingObject(initializationData);

            using (var loadingScreenDisposable = new ShowLoadingScreenDisposable(loadingScreenController))
            {
                loadingScreenDisposable.SetLoadingBarPercent(0);
                await InitializeObjects();
                loadingScreenDisposable.SetLoadingBarPercent(0.33f);
                await CreateObjects();
                loadingScreenDisposable.SetLoadingBarPercent(0.66f);
                await PrepareGame();
                loadingScreenDisposable.SetLoadingBarPercent(1f);
                await UniTask.Delay(TimeSpan.FromSeconds(3));
            }

            await BeginGame();
        }

        public override void Exit()
        {
            base.Exit();    

        }

        private void BindingObject(GameInitializationDataSO gameInitializationData)
        {
            UnityEngine.Object.Instantiate(gameInitializationData.slingshotControllerPrefab);

            loadingScreenController            = UnityEngine.Object.Instantiate(gameInitializationData.loadingScreenPrefab);
            gameContext.infoBarController      = UnityEngine.Object.Instantiate(gameInitializationData.infoBarController);
            gameContext.Map                    = UnityEngine.Object.Instantiate(gameInitializationData.mapPrefab);
        }

        private async UniTask InitializeObjects()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        private async UniTask CreateObjects()
        {
            /*// 어드레서블 로드과정 필요
             * var bundle = await AssetBundle.LoadFromFileAsync("BundlePath");
            var obj = bundle.LoadAsset<GameObject>("MyObject");
            var obj2 = awiat Addressable.LoadAssetAsync<GameObject>("AddressableKey");
             */
            /* heroInstance = Instantiate(heroPrefab, new Vector3(0, 8, 0), Quaternion.identity);
             enemyInstance = Instantiate(enemyPrefab, new Vector3(1, 1, 0), Quaternion.identity);
             heroInstance.gameObject.SetActive(false);
             enemyInstance.gameObject.SetActive(false);
 */
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        private async UniTask PrepareGame()
        {
            //게임 초기세팅 플레이어 위치 같은거 
            //gameContext.LevelController.Subscribe(gameContext.infoBarController);
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }


        private async UniTask BeginGame()
        {
            //await levelUI.ShowLevelAnimation();
            //enemiesSpawner.ShowAllEnemies();
            await UniTask.Yield();
            stateMachine.RequestTransition(GameFlowStateMachine.GameFlowState.GameStart);
        }
    }
}