using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackSelectionHandler : MonoBehaviour
{

    public void LoadTrackSelection(){
        SceneManager.LoadScene("SelectionTrack");
    }

    public void LoadBelgium(){
        PlayerPrefs.SetString("track", "Belgium");
        SceneManager.LoadScene("Belgium");
    }

    public void LoadBahrein(){
        PlayerPrefs.SetString("track", "Bahrein");
        SceneManager.LoadScene("Bahrein");
    }

    public void LoadAustria(){
        PlayerPrefs.SetString("track", "Austria");
        SceneManager.LoadScene("Austria");
    }
}
