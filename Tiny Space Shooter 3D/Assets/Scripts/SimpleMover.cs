using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
