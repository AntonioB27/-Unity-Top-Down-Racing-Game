using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpriteHandler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private string group;

    private int selectedSprite;
    private int randomSprite;

    public GT3Database gt3DB;
    public RCDatabase rcDB;
    public FormulaDatabase formulaDB;

    private void Start()
    {
        group = PlayerPrefs.GetString("selectedGroup");
        if (group.Equals("rc"))
        {
            if (transform.parent.tag != "AI")
            {
                selectedSprite = PlayerPrefs.GetInt("selectedOptionRC");
                Character ch = rcDB.GetCharacter(selectedSprite);
                spriteRenderer.sprite = ch.carSprite;
            }
            else
            {
                randomSprite = Random.Range(0, rcDB.characterCount);
                Character ch = rcDB.GetCharacter(randomSprite);
                spriteRenderer.sprite = ch.carSprite;
            }
        }
        if (group.Equals("gt3"))
        {
            if (transform.parent.tag != "AI")
            {
                selectedSprite = PlayerPrefs.GetInt("selectedOptionGt3");
                Character ch = gt3DB.GetCharacter(selectedSprite);
                spriteRenderer.sprite = ch.carSprite;
            }
            else
            {
                randomSprite = Random.Range(0, gt3DB.characterCount);
                Character ch = gt3DB.GetCharacter(randomSprite);
                spriteRenderer.sprite = ch.carSprite;
            }
        }
        if (group.Equals("f1"))
        {
            if (transform.parent.tag != "AI")
            {
                selectedSprite = PlayerPrefs.GetInt("selectedOptionF1");
                Character ch = formulaDB.GetCharacter(selectedSprite);
                spriteRenderer.sprite = ch.carSprite;
            }
            else
            {
                randomSprite = Random.Range(0, formulaDB.characterCount);
                Character ch = formulaDB.GetCharacter(randomSprite);
                spriteRenderer.sprite = ch.carSprite;
            }
        }
    }
}
