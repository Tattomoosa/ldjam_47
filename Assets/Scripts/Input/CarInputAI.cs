using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarInputAI : CarInput 
{
    public float steeringRaycastLength = 10.0f;
    public WaypointManager waypointManager;
    
    public float waypointMaximumOffset = 40.0f;
    public float speedRange = 0.1f;
    public float mineSpawnBase = 7.0f;
    public float mineSpawnRange = 4.0f;

    public bool debugDraw = true;
        
    private InputState _input;

    private Vector3 _waypointOffset;
    private float _speedOffset;
    private bool _weaponCooldownActive;

    private Waypoint _targetWaypoint;
    public Waypoint TargetWaypoint
    {
        get => _targetWaypoint;
        set
        {
            _targetWaypoint = value;
            // random to make them a bit dumber
            _waypointOffset = new Vector3(
                Random.Range(-waypointMaximumOffset, waypointMaximumOffset),
                0.0f,
                Random.Range(-waypointMaximumOffset, waypointMaximumOffset)
            );
        }
    }

    public void Start()
    {
        _input = new InputState();
        TargetWaypoint = waypointManager.GetFirstWaypoint();
        _speedOffset = Random.Range(-speedRange, speedRange);
    }

    IEnumerator WaitToSpawnMine()
    {
        float spawnTime = mineSpawnBase + Random.Range(-mineSpawnRange, mineSpawnRange);
        _weaponCooldownActive = true;
        yield return new WaitForSeconds(spawnTime);
        _weaponCooldownActive = false;
    }

    public override InputState GetInput()
    {
        return _input;
    }

    public void Update()
    {
        if (!TargetWaypoint)
        {
            Debug.LogError("[AI Error] No Waypoints!");
            return;
        }

        Vector3 waypoint = (TargetWaypoint.transform.position + _waypointOffset) - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, waypoint, Vector3.up);
        // Debug.Log(angle);
        if (debugDraw)
        {
            Debug.DrawRay(TargetWaypoint.transform.position, _waypointOffset);
            Debug.DrawRay(transform.position, waypoint, Color.green);
            Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.red);
            Debug.DrawRay(transform.position + (transform.forward * 100.0f), transform.right * (angle * 2.0f),
                Color.yellow);
        }

        _input.VerticalSteeringAxis = 1.0f + _speedOffset;
        if (Mathf.Abs(angle) > 5.0f)
            _input.HorizontalSteeringAxis = Mathf.Clamp(angle, -1.0f, 1.0f);
        else
            _input.HorizontalSteeringAxis = 0.0f;
        if (!_weaponCooldownActive)
        {
            _input.Fire = true;
            StartCoroutine(WaitToSpawnMine());
        }
        else
            _input.Fire = false;
    }

    public void FixedUpdate()
    {
        // TODO this can be simplified a lot i bet...
        /*
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
        if (Mathf.Abs(steerDirection) > 0.5f)
        {
            _input.HorizontalSteeringAxis = Mathf.Clamp(steerDirection * cross.magnitude, -1.0f, 1.0f);
        }
        */
    }
}
