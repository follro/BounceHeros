using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*[AttributeUsage(AttributeTargets.Constructor)]
public class InjectAttribute : Attribute
{
}
*/
public class ObjectResolver
{
    private readonly HashSet<Type> registrations = new();
    private readonly Dictionary<Type, object> instancePerTypeMap = new();

    public void Register<T>()
    {
        registrations.Add(typeof(T));
    }

    public void RegisterInstance<T>(T instance)
    {
        instancePerTypeMap[typeof(T)] = instance;
        registrations.Add(instance.GetType());
    }

    public void UnregisterType<T>()
    {
        instancePerTypeMap.Remove(typeof(T));
        registrations.Remove(typeof(T));
    }

   /* public T Resolve<T>()
    {
        //return (T)Resolve(typeof(T));   
    }*/

    /*private object Resolve(Type instanceType)
    {
        if(instancePerTypeMap.TryGetValue(instanceType, out var instance))
            return instance;

        if (!registrations.Contains(instanceType))
        {
            Debug.LogError($"Couldn't reslove type {instanceType}");
            return default;
        }

        var constructor = instanceType.GetConstructors().FirstOrDefault(c => Attribute.IsDefined(c, typeof(InjectAttribute)));

        if (constructor == null)
        {
            Debug.LogError($"Type '{instanceType.Name}' does not have a constructor marked with the [Inject] attribute. Please add [Inject] to the constructor you intend to use.'");
            return default;
        }
        var args = constructor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray();
        instance = Activator.CreateInstance(instanceType, args);

        instancePerTypeMap[instanceType] = instance;    
        return instance;
    }*/
}
