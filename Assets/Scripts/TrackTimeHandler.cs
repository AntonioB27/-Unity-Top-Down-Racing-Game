using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class TrackTimeHandler : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentTimerText;
    [SerializeField] TextMeshProUGUI fastestTimerText;
    [SerializeField] TextMeshProUGUI sectorDiffTimerText;
    [SerializeField] TextMeshProUGUI lapDiffTimerText;

    private GhostHandler ghostHandler;

    private bool firstLap = true;
    private float elapsedTime;
    private float fastestTime;
    private float fastestSectorTime1;
    private float fastestSectorTime2;
    private float fastestSectorTime3;
    private float sectorTime1;
    private float sectorTime2;
    private float sectorTime3;

    float diff;
    float lapDiff;

    void Start(){
        lapDiffTimerText.text = "";
        sectorDiffTimerText.text = "";

        ghostHandler = GameObject.FindGameObjectWithTag("GhostCar").GetComponent<GhostHandler>();
    }

    void FixedUpdate()
    {
        timerControl();
    }

    private void timerControl(){
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int miliseconds = Mathf.FloorToInt(elapsedTime * 1000) % 1000;
        currentTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        if(fastestTimerText){
            int f_minutes = Mathf.FloorToInt(fastestTime / 60);
            int f_seconds = Mathf.FloorToInt(fastestTime % 60);
            int f_miliseconds = Mathf.FloorToInt(fastestTime * 1000) % 1000;
            fastestTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", f_minutes, f_seconds, f_miliseconds);
        }
    }

    public void setFastestLap()
    {
        displayLapDiff();
        if(!firstLap && elapsedTime < fastestTime)
        {
            fastestTime = elapsedTime;
            ghostHandler.SetFastestLapArray();
        }
        if (firstLap)
        {
            fastestTime = elapsedTime;
            firstLap = false;
            ghostHandler.SetFastestLapArray();
        }
        if(elapsedTime > fastestTime){
            ghostHandler.ResetCounter();
        }
        ghostHandler.ResetCounter();
        elapsedTime = 0;
        sectorDiffTimerText.text = "";
    }

    public void setFastestSector(int sector){
        switch(sector){
            case 1:
            setFastestSector1();
            break;
            case 2:
            setFastestSector2();
            break;
            case 3:
            setFastestSector3();
            break;
        }
    }

    public void displayLapDiff(){
        if(!firstLap){
            lapDiff = elapsedTime - fastestTime;
            if(lapDiff > 0){
                lapDiffTimerText.color = Color.red;
            }
            else{
                lapDiffTimerText.color = Color.green;
            }
            StartCoroutine(TimerHelper.displayTextForSeconds(lapDiff.ToString("0.00"), lapDiffTimerText, 5));
        }
    }

    private void setFastestSector1(){
        sectorTime1 = elapsedTime;
        diff = sectorTime1 - fastestSectorTime1;
        if(sectorTime1 < fastestSectorTime1 || firstLap){
            fastestSectorTime1 = sectorTime1;
        }
        displaySectorDiff();
    }

    private void setFastestSector2(){
        sectorTime2 = elapsedTime;
        diff = sectorTime2 - fastestSectorTime2;
        if(sectorTime2 < fastestSectorTime2 || firstLap){
            fastestSectorTime2 = sectorTime2;
        }
        displaySectorDiff();
    }

        private void setFastestSector3(){
        sectorTime3 = elapsedTime;
        diff = sectorTime3 - fastestSectorTime3;
        if(sectorTime3 < fastestSectorTime3 || firstLap){
            fastestSectorTime3 = sectorTime3;
        }
        displaySectorDiff();
    }

    private void displaySectorDiff(){
        if(!firstLap){
            if(diff < 0){
                sectorDiffTimerText.color = Color.green;
            }
            if(diff > 0){
                sectorDiffTimerText.color = Color.red;
            }
            sectorDiffTimerText.text = diff.ToString("0.00");
        }
    }
}
