using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAssetLoadManager 
{
    public UniTask<T> LoadAsync<T>(string key) where T : Object;
    public UniTask<T> LoadAsync<T>(AssetReference assetReference) where T : Object;
    public void Release<T>(T loadedObject) where T : Object;

}
