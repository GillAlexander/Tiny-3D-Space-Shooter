using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> ServicesList = new Dictionary<Type, object>();

    //public static readonly UnityContainer serviceContainer = new UnityContainer();

    //public static void RegisterInstance<T>(object instance)
    //{
    //    ServicesList.Add(T, instance);
    //}

    // UNITYCONTAINER FUNGERAR INTE FÖR MONOHBEHAVIOR
    
    //public static void RegisterContainer<T>(object instance)
    //{
    //    ServicesList
    //}
    //public static void RegisterContainer<T>(string name, object instance)
    //{
    //    // Registrerar instance till typen T och castar instance till T
    //    serviceContainer.RegisterInstance(name, instance); 
    //}

    //public static IEnumerable<T> ResolveAllContainer<T>()
    //{
    //    // Resolverar allt med typen T i containern
    //    return serviceContainer.ResolveAll<T>();
    //}

    //public static T ResolveContainer<T>()
    //{
    //    return serviceContainer.Resolve<T>();
    //}

    //public static void ResetContainer()
    //{
    //    serviceContainer.Dispose();
    //}
}