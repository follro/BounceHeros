using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;


namespace BounceHeros
{
    public class GameInitiator : MonoBehaviour
    {
       
        [SerializeField] private Light mainDirectionalLight;
        [SerializeField] private EventSystem mainEventSystem;
        [SerializeField] private GameObject background;
        //[SerializeField] private EnemiesSpawner enemiesSpawner;
        //[SerializeField] private LevelUI levelUI;
        //[SerializeField] private LevelManager levelManager;
        [SerializeField] private LoadingScreen loadingScreen;

        //임시 오브젝트들
        [SerializeField] private BaseHero hero;
        [SerializeField] private BaseEnemy enemy;
        [SerializeField] private GameObject map;

        private DragInputHandler dragInputHandler;
        private HeroCatcher heroCatcher;
        private SlingShotVisualizer slingShotVisualizer;

        [Inject]
        public void Construct(
            DragInputHandler dragInputHandler,
            HeroCatcher heroCatcher,
            SlingShotVisualizer slingShotVisualizer)
        {
            this.dragInputHandler = dragInputHandler;
            this.heroCatcher = heroCatcher;
            this.slingShotVisualizer = slingShotVisualizer;
        }

        private async void Start()
        {
            BindingObject();
            using (var loadingScreenDisposable = new ShowLoadingScreenDisposable(loadingScreen))
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
            
            mainDirectionalLight = Instantiate(mainDirectionalLight);
            mainEventSystem = Instantiate(mainEventSystem);
            background = Instantiate(background);
            map = Instantiate(map);
            loadingScreen = Instantiate(loadingScreen);

        }

        private async UniTask InitializeObjects()
        {
            heroCatcher.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(3));
        }

        private async UniTask CreateObjects()
        {
            /*// 어드레서블 로드과정 필요
             * var bundle = await AssetBundle.LoadFromFileAsync("BundlePath");
            var obj = bundle.LoadAsset<GameObject>("MyObject");
            var obj2 = awiat Addressable.LoadAssetAsync<GameObject>("AddressableKey");
             */
            hero = Instantiate(hero, new Vector3(0, 8, 0), Quaternion.identity);
            enemy = Instantiate(enemy, new Vector3(1, 1, 0), Quaternion.identity);
            hero.gameObject.SetActive(false);
            enemy.gameObject.SetActive(false);

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
            hero.gameObject.SetActive(true);
            enemy.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(3));

        }


    }
}