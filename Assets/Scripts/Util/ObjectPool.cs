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
            Debug.LogError($"{poolTypeName}: Ǯ ������Ʈ�� �θ� Transform�� null�Դϴ�. Ǯ ������Ʈ�� ���� �ֻ��� ������ �����˴ϴ�.");

        Initialize();
        this.factory = factory;
        poolParent = new GameObject($"{productTypeName}Pool: {factory.ProductName}").transform;
        
        if(parent != null)
            poolParent.SetParent(parent);
        if(poolInitialSize < 0)
            poolInitialSize = 0;

        if (!CreateObject(poolInitialSize))
            Debug.LogError($"{poolTypeName}: �ʱ� ������Ʈ ({poolInitialSize}��) ������ �����߽��ϴ�. Ǯ�� ������������ �ʱ�ȭ�� �� �ֽ��ϴ�.");
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
            Debug.LogWarning($"{poolTypeName}: ������ ������Ʈ ������ 0���Ͽ��� �ƹ��͵� �������� �ʽ��ϴ�. (Count: {count})");
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
            Debug.LogWarning($"{poolTypeName}: Pool({factory.ProductName})�� ������ϴ�. ���� �� �뷮: {TotalPoolCapacity}. {expansionAmount}���� �� �ν��Ͻ��� �߰��� �����մϴ�.");

            if (!CreateObject(expansionAmount))
            {
                Debug.LogError($"{poolTypeName}: ������Ʈ ({expansionAmount}��) Ȯ�� ������ �����߽��ϴ�. ������Ʈ�� ������ �� �����ϴ�.");
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

        // �ߺ� ��ȯ ����
        if (!obj.gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"�̹� ��Ȱ��ȭ�� ��ü�� ��ȯ�Ϸ� �߽��ϴ�: {obj.name}");
            return;
        }

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(poolParent);
        pool.Enqueue(obj);
    }

    
}
