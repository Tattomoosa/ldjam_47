using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private List<Waypoint> _waypoints;
    
    // Start is called before the first frame update
    void Awake()
    {
        _waypoints = GetComponentsInChildren<Waypoint>().ToList();
        foreach (Waypoint wp in _waypoints)
            wp.gameObject.layer = LayerMask.NameToLayer("AI");
    }

    public Waypoint GetNextWaypoint(Waypoint waypoint)
    {
        int index = _waypoints.IndexOf(waypoint);
        if (index < 0)
            Debug.LogError("INVALID WAYPOINT");
        if (index == _waypoints.Count() - 1)
            return _waypoints[0];
        return _waypoints[index + 1];
    }

    public Waypoint GetFirstWaypoint()
    {
        if (_waypoints.Count() > 0)
            return _waypoints[0];
        return null;
    }
}
