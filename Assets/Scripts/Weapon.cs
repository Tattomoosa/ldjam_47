﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Sprite weaponImage;
    public int damage;

    virtual public void fire(Vector3 spawnPosition) { }
}
