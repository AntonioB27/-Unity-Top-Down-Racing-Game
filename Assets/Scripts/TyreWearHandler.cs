using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreWearHandler : MonoBehaviour
{
    CarController carController;

    Vector2 oldPossition;
    Vector2 distanceVector;

    public float distanceTraveled = 0;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        oldPossition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceVector = new Vector2(transform.position.x - oldPossition.x, transform.position.y - oldPossition.y);
        distanceTraveled += distanceVector.magnitude / 1000;
        oldPossition = transform.position;
    }

    public float getDistanceTraveled() { return this.distanceTraveled; }

    public void resetDistanceTraveled()
    {
        distanceTraveled= 0;
    }
}
