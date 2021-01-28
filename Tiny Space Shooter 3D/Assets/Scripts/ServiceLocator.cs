using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> ServicesList = new Dictionary<Type, object>();

    private static readonly UnityContainer serviceContainer = null;
    
    public static void RegisterContainer<T>(object instance)
    {
        // Registrerar instance till typen T och castar instance till T
        serviceContainer.RegisterInstance<T>((T)instance); 
    }

    public static IEnumerable ResolveAllContainer<T>()
    {
        // Resolverar allt med typen T i containern
        return serviceContainer.ResolveAll<T>();
    }

    public static T ResolveContainer<T>()
    {
        return serviceContainer.Resolve<T>();
    }

    public static void ResetContainer()
    {
        serviceContainer.Dispose();
    }
}