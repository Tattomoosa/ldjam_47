using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapBeginning : MonoBehaviour
{
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 20.0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        ArcadeCarController car = other.transform.parent.GetComponent<ArcadeCarController>();
        if (!car)
            return;
        // Rigidbody rb = car.GetComponent<Rigidbody>();
        car.CurrentLap += 1;
    }
}
