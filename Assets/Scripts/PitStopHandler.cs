using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PitStopHandler : MonoBehaviour
{
    private TyreWearHandler tyreWearHandler;
    private CarController carController;

    private bool timerFinished = true;

    public float pitStopStopTime = 2f;

    private float timeRemaining;

    public TextMeshProUGUI pitStopTimer;

    private void Awake()
    {
        tyreWearHandler = GetComponent<TyreWearHandler>();
        carController = GetComponent<CarController>();
        NoDisplay();
        timeRemaining = pitStopStopTime;
    }

    void FixedUpdate()
    {
        PitStopTimer();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Pit-Stop"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (collision.gameObject.tag.Equals("Stop") && (tag == "Player" || tag == "AI"))
        {
            timerFinished = false;
            tyreWearHandler.resetDistanceTraveled();
            carController.resetTyreWear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Pit-Stop"))
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void PitStopTimer()
    {
        if (!timerFinished)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                carController.setIsInPitStop(true);
                if (tag == "Player")
                {
                    Display(timeRemaining);
                }
            }
            if (timeRemaining < 0)
            {
                timerFinished = true;
                timeRemaining = pitStopStopTime;
                carController.setIsInPitStop(false);
                NoDisplay();
            }
        }
    }

    private void Display(float time)
    {
        pitStopTimer.text = time.ToString("0.00");
    }

    private void NoDisplay()
    {
        pitStopTimer.text = "";
    }
}
