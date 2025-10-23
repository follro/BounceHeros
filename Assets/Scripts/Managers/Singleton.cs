using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    Debug.LogWarning($"씬에 {typeof(T)} 타입의 싱글톤이 없어 새로 생성합니다.");
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning($"중복된 {typeof(T)} 싱글톤이 감지되어 이 인스턴스를 파괴합니다.");
            Destroy(gameObject);
            return;
        }

        instance = this as T;

        DontDestroyOnLoad(gameObject);
    }
}
