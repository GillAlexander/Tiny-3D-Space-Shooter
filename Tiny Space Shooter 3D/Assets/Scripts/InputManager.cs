using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool LeftMousePressed { get; private set; }

    private void Update()
    {
        LeftMousePressed = Input.GetMouseButtonDown(0);
    }
}
