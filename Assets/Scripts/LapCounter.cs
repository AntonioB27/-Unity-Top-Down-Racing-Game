using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class LapCounter : MonoBehaviour
{
    GameObject EndOfTheRace;
    int passedWaypointNumber = 0;
    float timeAtLastPassedCheckpoint = 0;
    int numberOfPassedWaypoints = 0;

    int lapsCompleted = 0;

    private int lapsToComplete = 10;

    public bool isRaceFinished = false;

    int carPosition = 0;
    public event Action<LapCounter> OnPassWaypoint;

    void Start()
    {
        EndOfTheRace = GameObject.FindGameObjectWithTag("EOTR"); ;
        EndOfTheRace.GetComponentsInChildren<Image>().ToList().ForEach(image => image.enabled = false);
        EndOfTheRace.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

        DecideRacingLaps();
    }

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int GetNumberOfPassedWaypoints()
    {
        return numberOfPassedWaypoints;
    }

    public float GetTimeAtLastPassedCheckpoint()
    {
        return timeAtLastPassedCheckpoint;
    }

    public int GetLapsCompleted()
    {
        return lapsCompleted;
    }

    public int GetLapsToComplete()
    {
        return lapsToComplete;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (isRaceFinished)
        {
            if (tag.Equals("Player"))
            {
                EndOfTheRace.GetComponentsInChildren<Image>().ToList().ForEach(image => image.enabled = true);
                EndOfTheRace.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
            return;
        }

        if (other.CompareTag("Waypoint"))
        {

            Waypoint waypoint = other.GetComponent<Waypoint>();

            if (waypoint.waypointNumber == passedWaypointNumber + 1)
            {
                passedWaypointNumber = waypoint.waypointNumber;

                numberOfPassedWaypoints++;

                if (waypoint.isFinishLine)
                {
                    passedWaypointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceFinished = true;
                    }
                }

                timeAtLastPassedCheckpoint = Time.time;

                OnPassWaypoint?.Invoke(this);
            }
        }
    }

    void DecideRacingLaps()
    {
        string track = PlayerPrefs.GetString("track");
        string group = PlayerPrefs.GetString("selectedGroup");

        switch (track)
        {
            case "Belgium":
                switch (group)
                {
                    case "rc":
                        lapsToComplete = 2;
                        break;
                    case "gt3":
                        lapsToComplete = 8;
                        break;
                    case "f1":
                        lapsToComplete = 5;
                        break;
                    default:
                        Debug.LogError("Unknown group: " + group);
                        break;
                }
                break;
            case "Bahrein":
                switch (group)
                {
                    case "rc":
                        lapsToComplete = 3;
                        break;
                    case "gt3":
                        lapsToComplete = 10;
                        break;
                    case "f1":
                        lapsToComplete = 7;
                        break;
                    default:
                        Debug.LogError("Unknown group: " + group);
                        break;
                }
                break;
            case "Austria":
                switch (group)
                {
                    case "rc":
                        lapsToComplete = 3;
                        break;
                    case "gt3":
                        lapsToComplete = 12;
                        break;
                    case "f1":
                        lapsToComplete = 7;
                        break;
                    default:
                        Debug.LogError("Unknown group: " + group);
                        break;
                }
                break;
            default:
                Debug.LogError("Unknown track: " + track);
                break;
        }

    }
}
