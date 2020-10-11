using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
