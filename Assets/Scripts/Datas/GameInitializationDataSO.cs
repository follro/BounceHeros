using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BounceHeros
{
    [CreateAssetMenu(fileName = "GameInitializationData", menuName = "ScriptableObjects/Game Initialization Data")]
    public class GameInitializationDataSO : ScriptableObject
    {
        [Header("Core Systems")]
        public Camera mainCameraPrefab;
        public Light directionalLightPrefab;
        public EventSystem eventSystemPrefab;

        [Header("Map")]
        public GameObject mapPrefab;

        [Header("UI")]
        public LoadingScreenController loadingScreenPrefab;
        public InfoBarController infoBarController;
        //public GameObject gameInfoBarPrefab;

        [Header("Controllers")]
        public GameObject slingshotControllerPrefab;

        [Header("Gameplay Objects")] // 히어로와 적 스포너로 변경필요
        public GameObject heroPrefab;
        public GameObject enemyPrefab;

        // 초기화 설정값들도 추가 가능
        [Header("Settings")]
        public float bindingDelay = 1f;
        public float initializationDelay = 2f;
        public float creationDelay = 2f;
        public float preparationDelay = 1f;
    }
}