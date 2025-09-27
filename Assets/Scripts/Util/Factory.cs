using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Factory<T> where T : IProduct
{
   /* protected IAssetLoader assetLoader;
    public Factory(IAssetLoader assetLoader = null) 
    {
        this.assetLoader = assetLoader;
    }*/
    public abstract string ProductName { get; protected set; }
    public abstract T CreateProduct(Transform parent, Vector3 pos);


}
