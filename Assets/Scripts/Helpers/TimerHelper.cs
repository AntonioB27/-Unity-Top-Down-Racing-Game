using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerHelper : MonoBehaviour
{
    float timeRemaining = 0f;
    // Start is called before the first frame update
    public static IEnumerator displayTextForSeconds(string text, TextMeshProUGUI textMeshProUGUI, float displayDuration)
    {
        textMeshProUGUI.text = text;

        yield return new WaitForSeconds(displayDuration);

        textMeshProUGUI.text = "";
    }

    public bool AfterSecondsReturnTrue(float waitDuration)
    {
        timeRemaining = waitDuration;
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0){
            return true;
        }

        return false;
    }

    public static IEnumerator WaitForSeconds(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
    }
}
