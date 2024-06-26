using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public List<LapCounter> lapCounters = new List<LapCounter>();

    public TextMeshProUGUI positionText;
    public TextMeshProUGUI lapCounterText;
    public TextMeshProUGUI finalPositionText;

    void Start()
    {
        foreach(LapCounter counter in lapCounters){
            counter.OnPassWaypoint += OnPassWaypoint;
        }
    }

    void OnPassWaypoint(LapCounter lapCounter){
        lapCounters = lapCounters.OrderByDescending(s => s.GetNumberOfPassedWaypoints())
                      .ThenBy(s => s.GetTimeAtLastPassedCheckpoint()).ToList();

        int carPosition = lapCounters.IndexOf(lapCounter) + 1;

        lapCounter.SetCarPosition(carPosition);

        if(lapCounter.CompareTag("Player")){
            positionText.text = string.Format($"{carPosition}/{lapCounters.Count}");
            if(lapCounter.GetLapsCompleted() <= lapCounter.GetLapsToComplete()){
                lapCounterText.text = string.Format($"{lapCounter.GetLapsCompleted() + 1}/{lapCounter.GetLapsToComplete()}");
                finalPositionText.text = carPosition.ToString();
            }
        }
    }
}
