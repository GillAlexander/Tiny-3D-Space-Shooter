using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }


    void Update()
    {
        var temp = Camera.main.transform.position.y * (1 - parallaxEffect);
        var dist = Camera.main.transform.position.y * parallaxEffect;

        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

        if (temp > startPos + length)
        {
            
            startPos += length;
        }
    }
}
