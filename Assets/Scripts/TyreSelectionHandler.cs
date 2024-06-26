using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TyreSelectionHandler : MonoBehaviour
{

    public void LoadTyreSelection(){
            SceneManager.LoadScene("SelectionTyre");
    }

    public void SelectSoft(){
        PlayerPrefs.SetString("selectedTyre", "soft");
        LoadTrackSelection();
    }

    public void SelectMedium(){
        PlayerPrefs.SetString("selectedTyre", "medium");
        LoadTrackSelection();
    }

    public void SelectHard(){
        PlayerPrefs.SetString("selectedTyre", "hard");
        LoadTrackSelection();
    }

    public void LoadTrackSelection(){
        SceneManager.LoadScene("SelectionTrack");
    }

    

}
