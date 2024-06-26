using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Canvas canvas1;
    [SerializeField] Canvas canvas2;

    void Start()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            canvas1.enabled = true;
            canvas2.enabled = false;
        }
        if (PlayerPrefs.GetInt("Mode") == 2)
        {
            canvas1.enabled = false;
            canvas2.enabled = true;
        }
    }
}
