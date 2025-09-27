using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TestAdressableLoader : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject heroObj;
    [SerializeField] private AssetReferenceGameObject[] enemyObj;
    [SerializeField] private AssetReferenceGameObject mapObj;

    [SerializeField] private AssetReferenceSprite[] sprites;

    private List<GameObject> clearGameObjects = new List<GameObject>();
    private async void Start()
    {
        Debug.Log("Addressables �ʱ�ȭ ����...");
        try
        {
            await InitAddressable();
            Debug.Log("Addressables �ʱ�ȭ �Ϸ�.");

            // �ʱ�ȭ�� ���� �� ���� ���� ���� (��: ���� �ε�)
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Addressables �ʱ�ȭ �� ���� �߻�: {ex.Message}");
        }
    }

    UniTask InitAddressable()
    {
        var initializeOperation = Addressables.InitializeAsync();
        return initializeOperation.ToUniTask();
    }

    private async void Update()
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

        if (testObj != null) Instantiate(testObj, Vector3.zero, Quaternion.identity);

    }


    public async UniTask<GameObject> LoadAssetAsync(string path)
    {
        AssetReferenceGameObject targetRef = null;

        // 1. ��ο� ���� AssetReference ����
        switch (path.ToLower()) // ��ҹ��� ������ ���ֱ� ���� toLower() ��� ����
        {
            case "hero":
                targetRef = heroObj;
                break;
            case "enemy":
                // ���� ���� ���, ���÷� ù ��° ��Ҹ� ����մϴ�.
                if (enemyObj != null && enemyObj.Length > 0)
                {
                    targetRef = enemyObj[0];
                }
                break;
            case "map":
                targetRef = mapObj;
                break;
            default:
                Debug.LogError($"���� �ε� ����: �� �� ���� ��� '{path}'");
                return null;
        }

        // 2. AssetReference�� ��ȿ���� Ȯ��
        if (targetRef == null || !targetRef.RuntimeKeyIsValid())
        {
            Debug.LogError($"���� �ε� ����: {path}�� �ش��ϴ� AssetReference�� ��ȿ���� �ʽ��ϴ�.");
            return null;
        }

        // 3. �񵿱� �ε� ���� �� �Ϸ� ���
        try
        {
            // await�� ����Ͽ� �ε尡 �Ϸ�� ������ ��ٸ��� ���� GameObject ��ü�� ��ȯ�մϴ�.
            // UniTask�� ����ϹǷ� Addressables �ڵ��� �ٷ� await �� �� �ֽ��ϴ�.
            GameObject loadedAsset = await targetRef.LoadAssetAsync<GameObject>();

            // ���� �ε尡 �Ϸ�Ǹ� ��ȯ�մϴ�.
            return loadedAsset;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"���� '{path}' �ε� �� �񵿱� ���� �߻�: {ex.Message}");
            return null;
        }
    }

    public void UnLoadAsset(Object asset)
    {
        throw new System.NotImplementedException();
    }
}
