using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class TrackTimeHandler2P : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentTimerText2;
    [SerializeField] TextMeshProUGUI fastestTimerText2;
    [SerializeField] TextMeshProUGUI sectorDiffTimerText2;
    [SerializeField] TextMeshProUGUI lapDiffTimerText2;

    private GhostHandler ghostHandler2;

    private bool firstLap2 = true;
    private float elapsedTime2;
    private float fastestTime2;
    private float fastestSectorTime1_2;
    private float fastestSectorTime2_2;
    private float fastestSectorTime3_2;
    private float sectorTime1_2;
    private float sectorTime2_2;
    private float sectorTime3_2;

    float diff2;
    float lapDiff2;

    void Start(){
        lapDiffTimerText2.text = "";
        sectorDiffTimerText2.text = "";

        ghostHandler2 = GameObject.FindGameObjectWithTag("GhostCar2").GetComponent<GhostHandler>();
    }

    void FixedUpdate()
    {
        timerControl();
    }

    private void timerControl(){
        elapsedTime2 += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime2 / 60);
        int seconds = Mathf.FloorToInt(elapsedTime2 % 60);
        int miliseconds = Mathf.FloorToInt(elapsedTime2 * 1000) % 1000;
        currentTimerText2.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        if(fastestTimerText2){
            int f_minutes = Mathf.FloorToInt(fastestTime2 / 60);
            int f_seconds = Mathf.FloorToInt(fastestTime2 % 60);
            int f_miliseconds = Mathf.FloorToInt(fastestTime2 * 1000) % 1000;
            fastestTimerText2.text = string.Format("{0:00}:{1:00}:{2:00}", f_minutes, f_seconds, f_miliseconds);
        }
    }

    public void setFastestLap()
    {
        displayLapDiff();
        if(!firstLap2 && elapsedTime2 < fastestTime2)
        {
            fastestTime2 = elapsedTime2;
            ghostHandler2.SetFastestLapArray();
        }
        if (firstLap2)
        {
            fastestTime2 = elapsedTime2;
            firstLap2 = false;
            ghostHandler2.SetFastestLapArray();
        }
        if(elapsedTime2 > fastestTime2){
            ghostHandler2.ResetCounter();
        }
        ghostHandler2.ResetCounter();
        elapsedTime2 = 0;
        sectorDiffTimerText2.text = "";
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
        if(!firstLap2){
            lapDiff2 = elapsedTime2 - fastestTime2;
            if(lapDiff2 > 0){
                lapDiffTimerText2.color = Color.red;
            }
            else{
                lapDiffTimerText2.color = Color.green;
            }
            StartCoroutine(TimerHelper.displayTextForSeconds(lapDiff2.ToString("0.00"), lapDiffTimerText2, 5));
        }
    }

    private void setFastestSector1(){
        sectorTime1_2 = elapsedTime2;
        diff2 = sectorTime1_2 - fastestSectorTime1_2;
        if(sectorTime1_2 < fastestSectorTime1_2 || firstLap2){
            fastestSectorTime1_2 = sectorTime1_2;
        }
        displaySectorDiff();
    }

    private void setFastestSector2(){
        sectorTime2_2 = elapsedTime2;
        diff2 = sectorTime2_2 - fastestSectorTime2_2;
        if(sectorTime2_2 < fastestSectorTime2_2 || firstLap2){
            fastestSectorTime2_2 = sectorTime2_2;
        }
        displaySectorDiff();
    }

        private void setFastestSector3(){
        sectorTime3_2 = elapsedTime2;
        diff2 = sectorTime3_2 - fastestSectorTime3_2;
        if(sectorTime3_2 < fastestSectorTime3_2 || firstLap2){
            fastestSectorTime3_2 = sectorTime3_2;
        }
        displaySectorDiff();
    }

    private void displaySectorDiff(){
        if(!firstLap2){
            if(diff2 < 0){
                sectorDiffTimerText2.color = Color.green;
            }
            if(diff2 > 0){
                sectorDiffTimerText2.color = Color.red;
            }
            sectorDiffTimerText2.text = diff2.ToString("0.00");
        }
    }
}
