using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public abstract class PoolableObject : MonoBehaviour
    {
        public event Action<PoolableObject> OnReturnToPoolRequest;
        public string ObjectID { get; private set; }
   
        public virtual void OnReturnToPool()
        {
            OnReturnToPoolRequest?.Invoke(this);
            // 풀에 반환될 때 처리할 로직 (필요 시)
        }
        public void InitializeFromPool(string id)
        {
            ObjectID = id;
            this.name = id.ToString();
        }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}