using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class TestAdressableLoader 
{
    private AssetReferenceGameObject heroObj;
    private AssetReferenceGameObject[] enemyObj;
    private AssetReferenceGameObject mapObj;

    private AssetReferenceSprite[] sprites;

    private List<GameObject> clearGameObjects = new List<GameObject>();
    public async void Initialize()
    {
        Debug.Log("Addressables 초기화 시작...");
        try
        {
            await InitAddressable();
            Debug.Log("Addressables 초기화 완료.");

            // 초기화가 끝난 후 다음 로직 진행 (예: 에셋 로드)
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Addressables 초기화 중 오류 발생: {ex.Message}");
        }
    }

    private UniTask InitAddressable()
    {
        var initializeOperation = Addressables.InitializeAsync();
        return initializeOperation.ToUniTask();
    }

    public async void Update()
    {
        var keyboard = Keyboard.current;
        GameObject testObj = null;
        if (keyboard.qKey.wasPressedThisFrame) 
        {
            testObj = await LoadAssetAsync("hero");
        }
        if (keyboard.wKey.wasPressedThisFrame)
        {
            testObj = await LoadAssetAsync("enemy");
        }
        if (keyboard.eKey.wasPressedThisFrame)
        {
            testObj =await LoadAssetAsync("map");
        }

       // if (testObj != null) Instantiate(testObj, Vector3.zero, Quaternion.identity);

    }


    public async UniTask<GameObject> LoadAssetAsync(string path)
    {
        AssetReferenceGameObject targetRef = null;

        // 1. 경로에 따라 AssetReference 선택
        switch (path.ToLower()) // 대소문자 구분을 없애기 위해 toLower() 사용 권장
        {
            case "hero":
                targetRef = heroObj;
                break;
            case "enemy":
                // 여러 개인 경우, 예시로 첫 번째 요소를 사용합니다.
                if (enemyObj != null && enemyObj.Length > 0)
                {
                    targetRef = enemyObj[0];
                }
                break;
            case "map":
                targetRef = mapObj;
                break;
            default:
                Debug.LogError($"에셋 로드 실패: 알 수 없는 경로 '{path}'");
                return null;
        }

        // 2. AssetReference가 유효한지 확인
        if (targetRef == null || !targetRef.RuntimeKeyIsValid())
        {
            Debug.LogError($"에셋 로드 실패: {path}에 해당하는 AssetReference가 유효하지 않습니다.");
            return null;
        }

        // 3. 비동기 로드 시작 및 완료 대기
        try
        {
            // await를 사용하여 로드가 완료될 때까지 기다리고 최종 GameObject 객체를 반환합니다.
            // UniTask를 사용하므로 Addressables 핸들을 바로 await 할 수 있습니다.
            GameObject loadedAsset = await targetRef.LoadAssetAsync<GameObject>();

            // 에셋 로드가 완료되면 반환합니다.
            return loadedAsset;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"에셋 '{path}' 로드 중 비동기 오류 발생: {ex.Message}");
            return null;
        }
    }

    public void UnLoadAsset(Object asset)
    {
        throw new System.NotImplementedException();
    }
}
