using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDeathZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
