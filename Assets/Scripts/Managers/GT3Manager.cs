using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GT3Manager : MonoBehaviour
{
    public GT3Database gt3DB;

    public Text nameText;

    public SpriteRenderer spriteRenderer;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOptionGt3")){
            selectedOption = 0;
        }
        else{
            Load();
        }

        UpdateCharacter(selectedOption);
    }

    public void NextOption(){
        selectedOption++;

        if(selectedOption >= gt3DB.characterCount){
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption(){
        selectedOption--;
        if(selectedOption < 0){
            selectedOption = gt3DB.characterCount-1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption){
        Character character = gt3DB.GetCharacter(selectedOption);
        spriteRenderer.sprite = character.carSprite;
        nameText.text = character.carName;
    }

    private void Load(){
        selectedOption = PlayerPrefs.GetInt("selectedOptionGt3");
    }

    private void Save(){
        PlayerPrefs.SetString("selectedGroup", "gt3");
        PlayerPrefs.SetInt("selectedOptionGt3", selectedOption);
    }
}
