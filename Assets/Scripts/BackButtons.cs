using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtons : MonoBehaviour
{
    public void ReturnToGroupSelection()
    {
        SceneManager.LoadScene("GroupSelection");
    }

    public void ReturnToCarSelection()
    {
        switch (PlayerPrefs.GetString("selectedGroup"))
        {
            case "f1":
            SceneManager.LoadScene("SelectionF1");
            break;
            case "gt3":
            SceneManager.LoadScene("SelectionGT3");
            break;
            case "rc":
            SceneManager.LoadScene("SelectionRC");
            break;
        }
    }

    public void ReturnToTyreSelection(){
        SceneManager.LoadScene("SelectionTyre");
    }
}
