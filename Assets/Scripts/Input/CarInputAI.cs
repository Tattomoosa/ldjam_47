using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarInputAI : CarInput 
{
    public float steeringRaycastLength = 10.0f;
    public WaypointManager waypointManager;
        
    private InputState _input;
    [System.NonSerialized]
    public Waypoint targetWaypoint;

    public void Start()
    {
        _input = new InputState();
        targetWaypoint = waypointManager.GetFirstWaypoint();
    }

    public override InputState GetInput()
    {
        return _input;
    }

    public void FixedUpdate()
    {
        var position = transform.position;
        var wayPoint0Y = targetWaypoint.transform.position;
        wayPoint0Y.y = position.y;
        var toWaypoint = wayPoint0Y - position;
        Debug.DrawRay(position, toWaypoint, Color.red);

        var forward0Y = transform.forward;
        forward0Y.y = 0.0f;
        var forward0YDisplay = forward0Y * 10.0f;
        Debug.DrawRay(position, forward0YDisplay, Color.cyan);
        
        // float differenceAngle = Vector3.Angle(toWaypoint, forward0Y);
        // Debug.DrawRay(position + forward0YDisplay, transform.right * differenceAngle, Color.yellow);

        Vector3 cross = Vector3.Cross(forward0Y, toWaypoint.normalized);
        float steerDirection = Vector3.Dot(cross.normalized, Vector3.up) * 100.0f;
        Debug.DrawRay(position + forward0YDisplay, -transform.right * steerDirection, Color.yellow);

        _input = new InputState();
        _input.VerticalSteeringAxis = 1.0f;
        if (Mathf.Abs(steerDirection) > 0.001f)
        {
            _input.HorizontalSteeringAxis = Mathf.Clamp(steerDirection * cross.magnitude, -1.0f, 1.0f);
        }
    }

    /*
    public void GetNextWaypoint()
    {
        targetWaypoint = waypointManager.GetNextWaypoint(targetWaypoint);
    }
    */

}
