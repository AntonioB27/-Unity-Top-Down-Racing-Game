using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FormulaDatabase : ScriptableObject
{
    public Character[] character;

    public int characterCount{
        get { return character.Length; }
    }

    public Character GetCharacter(int index){
        return character[index];
    }
}
