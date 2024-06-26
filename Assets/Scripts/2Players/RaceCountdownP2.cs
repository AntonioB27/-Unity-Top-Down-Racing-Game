using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class RaceCountdownP2 : MonoBehaviour
{
    List<CarController2P> carControllers;

    public Image lights0;
    public Image lights1;
    public Image lights2;
    public Image lights3;

    public GameObject HUD;

    void Start()
    {
        carControllers = FindObjectsOfType<CarController2P>().ToList();
        StartCoroutine(Countdown(4));
        HUD.SetActive(false);
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            Lights_Active(count);
            yield return new WaitForSeconds(1);
            count--;
        }

        Lights_Active(-1);
        HUD.SetActive(true);

        foreach (var controller in carControllers)
        {
            controller.SetRaceStarted();
        }
    }

    void Lights_Active(int count)
    {
        switch (count)
        {
            case 4:
                lights0.enabled = true;
                lights1.enabled = false;
                lights2.enabled = false;
                lights3.enabled = false;
                break;
            case 3:
                lights0.enabled = true;
                lights1.enabled = false;
                lights2.enabled = false;
                lights3.enabled = false;
                break;
            case 2:
                lights0.enabled = false;
                lights1.enabled = true;
                lights2.enabled = false;
                lights3.enabled = false;
                break;
            case 1:
                lights0.enabled = false;
                lights1.enabled = false;
                lights2.enabled = true;
                lights3.enabled = false;
                break;
            case 0:
                lights0.enabled = false;
                lights1.enabled = false;
                lights2.enabled = false;
                lights3.enabled = true;
                break;
            case -1:
                lights0.enabled = false;
                lights1.enabled = false;
                lights2.enabled = false;
                lights3.enabled = false;
                break;
        }
    }
}
