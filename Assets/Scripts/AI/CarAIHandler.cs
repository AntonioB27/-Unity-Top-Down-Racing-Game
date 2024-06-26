using System;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CarAIHandler : MonoBehaviour
{
    CarController carController;

    Vector3 targetPosition = Vector3.zero;

    WaypointNode currentWaypoint = null;
    WaypointNode[] allWaypoints;

    [SerializeField] Transform carTransform;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        FollowWaypoints();

        inputVector.x = TurnTowardTarget();
        inputVector.y = ThrottleControl(inputVector.x);

        carController.SetInputVector(inputVector);
    }

    void FollowWaypoints()
    {
        if (currentWaypoint == null)
        {
            currentWaypoint = GameObject.FindGameObjectWithTag("Node1").GetComponent<WaypointNode>();
        }

        if (currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWaypoint = (transform.position - targetPosition).magnitude;

            if (distanceToWaypoint <= currentWaypoint.minDistaceToReachWaypoint)
            {
                if (currentWaypoint.isPitStop && carController.getIsOldRubber())
                {
                    currentWaypoint = currentWaypoint.nextWaypointNode[1];
                }
                if (currentWaypoint.isPitStop)
                {
                    currentWaypoint = currentWaypoint.nextWaypointNode[0];
                }
                else
                {
                    currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
                }
            }
        }
    }

    WaypointNode FindClosestWaypoint()
    {
        return allWaypoints.OrderBy(w => Vector3.Distance(transform.position, w.transform.position)).FirstOrDefault();
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 30.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ThrottleControl(float inputX)
    {
        return 1.05f - Math.Abs(inputX) / 1.0f;
    }
}
