﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    public void CleanUp()
    {
        Destroy(this.gameObject);
    }
}
