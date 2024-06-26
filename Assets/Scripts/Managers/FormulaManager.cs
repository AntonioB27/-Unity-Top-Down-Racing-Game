using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormulaManager : MonoBehaviour
{
    public FormulaDatabase formulaDB;

    public Text nameText;

    public SpriteRenderer spriteRenderer;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOptionF1")){
            selectedOption = 0;
        }
        else{
            Load();
        }

        UpdateCharacter(selectedOption);
    }

    public void NextOption(){
        selectedOption++;

        if(selectedOption >= formulaDB.characterCount){
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption(){
        selectedOption--;
        if(selectedOption < 0){
            selectedOption = formulaDB.characterCount-1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption){
        Character character = formulaDB.GetCharacter(selectedOption);
        spriteRenderer.sprite = character.carSprite;
        nameText.text = character.carName;
    }

    private void Load(){
        selectedOption = PlayerPrefs.GetInt("selectedOptionF1");
    }

    private void Save(){
        PlayerPrefs.SetString("selectedGroup", "f1");
        PlayerPrefs.SetInt("selectedOptionF1", selectedOption);
    }

}
