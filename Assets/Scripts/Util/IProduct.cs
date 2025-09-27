using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProduct 
{
    string ProductId { get; set; }
    bool IsActive { get; }

    void OnSpawnFromPool();

    void OnReturnToPool();

    void SetTransform(Vector3 position, Quaternion rotation);

    void ResetToDefault();
}
