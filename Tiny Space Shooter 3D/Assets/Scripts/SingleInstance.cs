using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInstance : MonoBehaviour
{
    public static SingleInstance instance = null;

    private void Awake()
    {
        instance = (SingleInstance)FindObjectOfType(this.GetType());
    }
}
