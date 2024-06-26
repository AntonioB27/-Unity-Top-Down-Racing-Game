using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPreformacneHandler2P : MonoBehaviour
{
    public float acceleration;
    public float turnFactor;
    public float driftFactor;
    public float maxSpeed;
    private string group;

    private string tyreSelected;

    private float randomPreformanceDifference = 0;

    private CarController2P carController;

    private float stepSize = 0.02f;

    List<string> tyres = new List<string> { "soft", "medium", "hard" };

    private void Awake()
    {
        carController = GetComponent<CarController2P>();
    }

    private void Start()
    {
        tyreSelected = PlayerPrefs.GetString("selectedTyre");
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
        Debug.Log(tyreString);
        if (tyreString.Equals("soft"))
        {
            driftFactor = 0.2f;
            acceleration += 1f;
        }
        if (tyreString.Equals("medium"))
        {
            driftFactor = 0.4f;
            acceleration += 0.5f;
            turnFactor -= 0.3f;
        }
        if (tyreString.Equals("hard"))
        {
            driftFactor = 0.6f;
            turnFactor -= 0.6f;
        }
    }

    void AITyreSelection()
    {
        int random = UnityEngine.Random.Range(0, tyres.Count);

        string aiTyre = tyres[random];

        TyreSelectionPerformace(aiTyre);
    }
}
