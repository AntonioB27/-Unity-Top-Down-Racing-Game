using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PositionHandler2P : MonoBehaviour
{
    public List<LapCounter> lapCounters;
    public TextMeshProUGUI lapCounterTextPlayer1;
    public TextMeshProUGUI lapCounterTextPlayer2;

    void Start()
    {
        lapCounters = FindObjectsOfType<LapCounter>().ToList();

        foreach (LapCounter counter in lapCounters)
        {
            counter.OnPassWaypoint += OnPassWaypoint;
        }
    }

    void OnPassWaypoint(LapCounter lapCounter)
    {
        lapCounters = lapCounters.OrderByDescending(s => s.GetNumberOfPassedWaypoints())
                      .ThenBy(s => s.GetTimeAtLastPassedCheckpoint()).ToList();

        int carPosition = lapCounters.IndexOf(lapCounter) + 1;

        lapCounter.SetCarPosition(carPosition);

        if (lapCounter.CompareTag("Player") || lapCounter.CompareTag("Player2"))
        {
            int lapsCompletedPlayer1 = lapCounters[0].GetLapsCompleted();
            int lapsCompletedPlayer2 = lapCounters[1].GetLapsCompleted();

            if (lapCounter.GetLapsCompleted() <= lapCounter.GetLapsToComplete())
            {
                lapCounterTextPlayer1.text = string.Format($"{lapsCompletedPlayer1 + 1}/{lapCounter.GetLapsToComplete()}");
                lapCounterTextPlayer2.text = string.Format($"{lapsCompletedPlayer2 + 1}/{lapCounter.GetLapsToComplete()}");
            }
        }
    }
}
