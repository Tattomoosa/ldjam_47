﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputPlayer : MonoBehaviour
{
    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
