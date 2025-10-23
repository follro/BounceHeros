using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager 
{
    private Dictionary<Type, object> services = new Dictionary<Type, object>(); 
    public IEnumerable<object> RegisteredServices => services.Values;

    public T Get<T>() where T : class
    {
        Type type = typeof(T);
        if (services.TryGetValue(type, out object obj))
            return obj as T;

        throw new ArgumentException($"ServiceManager.Get: Service of type {type.FullName} already registered");
    }

    public bool TryGet<T>(out T service) where T : class
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

    public ServiceManager Register<T>(T service) where T : class
    {
        Type type = typeof(T);
        
        if(!services.TryAdd(type, service))
            Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");

        return this;
    }

}
