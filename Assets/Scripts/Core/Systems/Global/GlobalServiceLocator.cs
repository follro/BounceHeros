using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalServiceLocator 
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();
    public static bool IsInitialized { get; private set; }

    public static T Get<T>() where T : class
    {
        Type type = typeof(T);
        if (services.TryGetValue(type, out object obj))
            return obj as T;

        throw new ArgumentException($"ServiceManager.Get: Service of type {type.FullName} is NOT registered.");
    }

    public static bool TryGet<T>(out T service) where T : class
    {
        Type type = typeof(T);
        if (services.TryGetValue(type, out object obj))
        {
            service = obj as T;
            return true;
        }

        service = null;
        return false;
    }

    public static void Register<T>(T service) where T : class
    {
        Type type = typeof(T);

        if (!services.TryAdd(type, service))
            Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");
    }

    public static void Unregister<T> () where T : class
    {
        services.Remove(typeof(T)); 
    }

    public static void Initialize()
    {
        IsInitialized = true;
        if (services == null) services = new Dictionary<Type, object>();
        services.Clear();
    }
    
}
