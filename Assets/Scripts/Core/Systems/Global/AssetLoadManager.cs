using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetLoadManager : IAssetLoadManager
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
       
    }

    public UniTask<T> LoadAsync<T>(string key) where T : Object
    {
        throw new System.NotImplementedException();
    }

    public UniTask<T> LoadAsync<T>(AssetReference assetReference) where T : Object
    {
        throw new System.NotImplementedException();
    }

    public void Release<T>(T loadedObject) where T : Object
    {
        throw new System.NotImplementedException();
    }
}
