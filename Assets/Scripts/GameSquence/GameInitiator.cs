using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;


namespace BounceHeros
{
    public class GameInitiator : MonoBehaviour
    {
        [SerializeField] private Camera mainCameraPrefab; 
        [SerializeField] private Light directionalLightPrefab;
        [SerializeField] private EventSystem eventSystemPrefab;
        [SerializeField] private GameObject mapColliderPrefab;
        [SerializeField] private LoadingScreen loadingScreenPrefab;
        [SerializeField] private GameObject slingshotControllerPrefab;

        [SerializeField] private GameObject mapPrefab;

        private Camera mainCameraInstance;
        private EventSystem eventSystemInstance;
        private LoadingScreen loadingScreenInstance;
        private GameObject mapColliderInstance;
        private GameObject mapInstance;


        public async UniTask GameSetting()
        {
            BindingObject();
            
            using (var loadingScreenDisposable = new ShowLoadingScreenDisposable(loadingScreenInstance))
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


        private void BindingObject()
        {
            /* mainCamera =            Instantiate(mainCamera);
             mainDirectionalLight =  Instantiate(mainDirectionalLight);
             mainEventSystem =       Instantiate(mainEventSystem);
             mapCollider =           Instantiate(mapCollider);
             map =                   Instantiate(map);
             loadingScreen =         Instantiate(loadingScreen);
             slingShotController =   Instantiate(slingShotController);*/

            mainCameraInstance = Instantiate(mainCameraPrefab);
            Instantiate(directionalLightPrefab); // 라이트는 인스턴스를 저장하지 않아도 될 수 있음
            eventSystemInstance = Instantiate(eventSystemPrefab);
            mapColliderInstance = Instantiate(mapColliderPrefab);
            mapInstance = Instantiate(mapPrefab);   
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
            Instantiate(slingshotControllerPrefab);
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
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        private async UniTask BeginGame()
        {
            //await levelUI.ShowLevelAnimation();
            //enemiesSpawner.ShowAllEnemies();
            await UniTask.Delay(TimeSpan.FromSeconds(3));

        }


    }
}