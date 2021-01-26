using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceRegistrator : MonoBehaviour
{
    private void Start()
    {
        List<object> testList = new List<object>();
        testList.Add(FindObjectOfType<Level>());
        testList.Add(FindObjectOfType<EnemySpawner>());

        //ServiceLocator.Register<IPause>();
        //ServiceLocator.Register<IPause>(FindObjectOfType<EnemySpawner>());
        ServiceLocator.RegisterContainer<IPause>(FindObjectOfType<Level>());
        ServiceLocator.RegisterContainer<IPause>(FindObjectOfType<EnemySpawner>());

        //var con = new UnityContainer();
        //con.RegisterInstance<IPause>(FindObjectOfType<Level>());
        //con.RegisterInstance<IPause>(FindObjectOfType<EnemySpawner>());

        //IEnumerable<IPause> list = con.ResolveAll<IPause>();
        //foreach (var item in list)
        //{
        //    item.Pause();
        //}
    }
}