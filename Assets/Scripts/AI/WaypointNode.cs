using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    public float minDistaceToReachWaypoint = 5;

    public WaypointNode[] nextWaypointNode;

    public bool isPitStop = false;
}
