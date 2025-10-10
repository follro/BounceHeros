using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class ObjectPool<TPoolableObject> where TPoolableObject : PoolableObject
    {
        private const int ExpansionDivisor = 2;         
        private const int MinimumExpansionAmount = 10;  

        private readonly TPoolableObject prefab;
        private Transform parentObject;
        private int currentPoolSize;
        
        private Queue<TPoolableObject> pool;

        private string poolName;

        public ObjectPool(TPoolableObject prefab, int poolSize, Transform parentObject)
        {
            poolName = typeof(TPoolableObject).Name;
            if (prefab == null)
            {
                Debug.LogError($"Pool({poolName}): prefab is null. 풀 생성 불가");
                return;
            }

            currentPoolSize = 0;

            this.parentObject =  parentObject;

            pool = new Queue<TPoolableObject>();
            CreateObject(poolSize);
            
        }

        private void CreateObject(int objectsNum)
        {
            if (objectsNum < 0)
            { 
                Debug.LogWarning($"Pool({poolName}): 생성할 오브젝트 개수가 0이하여서 아무것도 생성하지 않습니다. (Count: {objectsNum})");
                return;
            }

            for (int i = 1; i <= objectsNum; i++)
            {
                TPoolableObject createdObj = Object.Instantiate(prefab, parentObject);
                createdObj.InitializeFromPool(typeof(TPoolableObject).Name + (currentPoolSize + i).ToString());
                createdObj.gameObject.SetActive(false);
                pool.Enqueue(createdObj);
            }
            currentPoolSize += objectsNum;
            
        }

        public TPoolableObject Spawn(Vector3 position, Quaternion rotation)
        {
            TPoolableObject obj = null;
            if (!pool.TryDequeue(out obj))
            {
                int expansionAmount = Mathf.Max(currentPoolSize / ExpansionDivisor, MinimumExpansionAmount);

                Debug.LogWarning(
                    $"Pool({poolName}): 풀이 비었습니다. 현재 총 용량: {currentPoolSize}. " +
                    $"자동으로 {expansionAmount}개의 새 인스턴스를 추가 생성합니다."
                );

                CreateObject(expansionAmount);

                if (!pool.TryDequeue(out obj))
                {
                    Debug.LogError($"Pool({poolName}): 오브젝트 확장 생성에 실패했습니다. 오브젝트를 가져올 수 없음.");
                    return null;
                }
            }

            obj.OnReturnToPoolRequest += ReturnToPool;
            obj.SetTransform(position, rotation);
            obj.gameObject.SetActive(true);
            return obj;
        }

        private void ReturnToPool(PoolableObject poolableObject)
        {
            TPoolableObject obj = poolableObject as TPoolableObject;

            if (obj == null)
            {
                Debug.LogError($"Pool({poolName}): 오브젝트 풀에 잘못된 타입의 객체 또는 null이 반환되었습니다.");
                return;
            }

            obj.OnReturnToPoolRequest -= ReturnToPool;


            if (obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(parentObject); 

                pool.Enqueue(obj);
            }
            else
            {
                Debug.LogWarning($"Pool({poolName}): 이미 비활성화된 객체({obj.name})를 풀에 다시 반환하려 했습니다. 중복 처리 방지.");
                Object.Destroy(obj.gameObject);
            }
        }
       
    }
}
