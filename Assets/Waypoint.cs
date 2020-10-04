using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private WaypointManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        // meshRenderer.enabled = false;
        _manager = transform.parent.GetComponent<WaypointManager>();
        if (!_manager)
            Debug.LogError("Waypoints must be children of a waypoint manager!");
    }
    
    public void OnTriggerEnter(Collider other)
    // public void OnCollisionEnter(Collision collision)
    {
        var aiController = other.transform.parent.GetComponent<CarInputAI>();
        if (aiController)
        {
            aiController.targetWaypoint = _manager.GetNextWaypoint(this);
        }
    }
}
