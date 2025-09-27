using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour, IProduct
{
    private readonly string poolTypeName;
    private readonly string productTypeName;

    private const int ExpansionDivisor = 2;
    private const int MinimumExpansionAmount = 10;

    private Transform poolParent;
    private Queue<T> pool;
    private Factory<T> factory;

    public int TotalPoolCapacity { get; private set; }
    public int ActiveObjectCount { get => TotalPoolCapacity - pool.Count; }
    public int AvailableObjectCount { get => pool.Count; }

    public ObjectPool(Transform parent, Factory<T> factory, int poolInitialSize)
    {
        poolTypeName = typeof(ObjectPool<T>).Name;
        productTypeName = typeof(T).Name;

        if (parent == null)
            Debug.LogError($"{poolTypeName}: 풀 오브젝트의 부모 Transform이 null입니다. 풀 오브젝트가 씬의 최상위 계층에 생성됩니다.");

        Initialize();
        this.factory = factory;
        poolParent = new GameObject($"{productTypeName}Pool: {factory.ProductName}").transform;
        
        if(parent != null)
            poolParent.SetParent(parent);
        if(poolInitialSize < 0)
            poolInitialSize = 0;

        if (!CreateObject(poolInitialSize))
            Debug.LogError($"{poolTypeName}: 초기 오브젝트 ({poolInitialSize}개) 생성에 실패했습니다. 풀이 비정상적으로 초기화될 수 있습니다.");
    }

    private void Initialize()
    {
        pool = new Queue<T>();
        TotalPoolCapacity = 0;
    }

    public bool CreateObject(int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning($"{poolTypeName}: 생성할 오브젝트 개수가 0이하여서 아무것도 생성하지 않습니다. (Count: {count})");
            return false;
        }

        for (int i = 0; i < count; i++)
        {
            T createdObj = factory.CreateProduct(poolParent, poolParent.transform.position);
            createdObj.gameObject.SetActive(false);
            pool.Enqueue(createdObj);
        }
        TotalPoolCapacity += count;
        return true;
    }

    public T GetFromPool()
    {
        T obj = null;
        if (pool.TryDequeue(out obj))
        {
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            int expansionAmount = Mathf.Max(TotalPoolCapacity / ExpansionDivisor, MinimumExpansionAmount);
            Debug.LogWarning($"{poolTypeName}: Pool({factory.ProductName})이 비었습니다. 현재 총 용량: {TotalPoolCapacity}. {expansionAmount}개의 새 인스턴스를 추가로 생성합니다.");

            if (!CreateObject(expansionAmount))
            {
                Debug.LogError($"{poolTypeName}: 오브젝트 ({expansionAmount}개) 확장 생성에 실패했습니다. 오브젝트를 가져올 수 없습니다.");
                return null;
            }
            if (pool.TryDequeue(out obj))
                return obj;
        }
        return null;
    }

    public void ReturnToPool(T obj)
    {
        if (obj == null) return;

        // 중복 반환 방지
        if (!obj.gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"이미 비활성화된 객체를 반환하려 했습니다: {obj.name}");
            return;
        }

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(poolParent);
        pool.Enqueue(obj);
    }

    
}
