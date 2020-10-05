using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCarsAtClosestWaypoint : MonoBehaviour
{
    public WaypointManager waypointManager;
    public Vector3 spawnOffset = Vector3.up * 10.0f;

    public void OnTriggerEnter(Collider other)
    {
        ArcadeCarController car = other.gameObject.GetComponent<ArcadeCarController>();
        if (car)
        {
            Rigidbody rb = car.GetMotionTarget();
            Waypoint closestWaypoint = waypointManager.GetClosestWaypoint(rb.position);
            if (closestWaypoint)
            {
                rb.position = closestWaypoint.transform.position + (spawnOffset);
                rb.velocity = Vector3.zero;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            Destroy(other.gameObject);
    }
}
