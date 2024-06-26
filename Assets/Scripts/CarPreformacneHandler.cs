using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPreformacneHandler : MonoBehaviour
{
    public float acceleration;
    public float turnFactor;
    public float driftFactor;
    public float maxSpeed;
    private string group;

    private string tyreSelected;

    private float randomPreformanceDifference = 0;

    private CarController carController;

    private float stepSize = 0.02f;

    List<string> tyres = new List<string> { "soft", "medium", "hard" };

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("mode") == 2)
        {
            tyreSelected = "soft";
        }
        else
        {
            tyreSelected = PlayerPrefs.GetString("selectedTyre");
        }
        RandomPerformanceGenerator();
    }

    private void RandomPerformanceGenerator()
    {
        double random = UnityEngine.Random.Range(-0.2f, 0.2f);
        double numSteps = Math.Floor(random / stepSize);
        random = numSteps * stepSize;
        randomPreformanceDifference = (float)Math.Round(random, 2);
    }

    public void GetPreformacneParameters()
    {
        tyreSelected = PlayerPrefs.GetString("selectedTyre");
        group = PlayerPrefs.GetString("selectedGroup");
        GroupPreformanceCalculator();
        if (tag != "AI")
        {
            TyreSelectionPerformace(tyreSelected);
        }
        else
        {
            AITyreSelection();
        }
    }

    private void GroupPreformanceCalculator()
    {
        switch (group)
        {
            case "rc":
                acceleration = 2f;
                turnFactor = 3.5f + randomPreformanceDifference;
                maxSpeed = 4.5f + randomPreformanceDifference;
                break;
            case "gt3":
                acceleration = 2.5f;
                turnFactor = 4f + randomPreformanceDifference;
                maxSpeed = 6.5f + randomPreformanceDifference;
                break;
            case "f1":
                acceleration = 3f;
                turnFactor = 4.2f + randomPreformanceDifference;
                maxSpeed = 7.3f + randomPreformanceDifference;
                break;
            default:
                Debug.LogError("Unknown vehicle group: " + group);
                break;
        }
    }

    private void TyreSelectionPerformace(string tyreString)
    {
        if (tyreString.Equals("soft"))
        {
            driftFactor = 0.2f;
            acceleration += 1f;
            carController.distanceToNewRubber = 0.2f;
            carController.distanceToOldRubber = 0.5f;
            carController.distanceToNoRubber = 0.7f;
        }
        if (tyreString.Equals("medium"))
        {
            driftFactor = 0.4f;
            acceleration += 0.5f;
            turnFactor -= 0.3f;
            carController.distanceToNewRubber = 0.5f;
            carController.distanceToOldRubber = 0.7f;
            carController.distanceToNoRubber = 1f;
        }
        if (tyreString.Equals("hard"))
        {
            driftFactor = 0.6f;
            turnFactor -= 0.6f;
            carController.distanceToNewRubber = 0.8f;
            carController.distanceToOldRubber = 1.1f;
            carController.distanceToNoRubber = 1.5f;
        }
    }

    void AITyreSelection()
    {
        int random = UnityEngine.Random.Range(0, tyres.Count);

        string aiTyre = tyres[random];

        TyreSelectionPerformace(aiTyre);
    }
}
