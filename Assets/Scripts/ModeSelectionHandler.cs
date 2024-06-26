using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectionHandler : MonoBehaviour
{
    public void P1Mode(){
        PlayerPrefs.SetInt("Mode", 1);
        SceneManager.LoadScene("GroupSelection");
    }

    public void P2Mode(){
        PlayerPrefs.SetInt("Mode", 2);
        SceneManager.LoadScene("GroupSelection");
    }
}
