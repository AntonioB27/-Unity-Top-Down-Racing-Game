using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RCManager : MonoBehaviour
{
    public RCDatabase rcDB;

    public Text nameText;

    public SpriteRenderer spriteRenderer;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOptionRC")){
            selectedOption = 0;
        }
        else{
            Load();
        }

        UpdateCharacter(selectedOption);
    }

    public void NextOption(){
        selectedOption++;

        if(selectedOption >= rcDB.characterCount){
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption(){
        selectedOption--;
        if(selectedOption < 0){
            selectedOption = rcDB.characterCount-1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption){
        Character character = rcDB.GetCharacter(selectedOption);
        spriteRenderer.sprite = character.carSprite;
        nameText.text = character.carName;
    }

    private void Load(){
        selectedOption = PlayerPrefs.GetInt("selectedOptionRC");
    }

    private void Save(){
        PlayerPrefs.SetString("selectedGroup", "rc");
        PlayerPrefs.SetInt("selectedOptionRC", selectedOption);
    }
}
