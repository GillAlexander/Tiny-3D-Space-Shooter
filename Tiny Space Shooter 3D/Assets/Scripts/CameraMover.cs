using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speedMultiplier = 1;

    void Update()
    {
        var value = (Vector3.up * Time.deltaTime * GameManager.GAMESPEED) * speedMultiplier;
        transform.Translate(value);
    }
}
