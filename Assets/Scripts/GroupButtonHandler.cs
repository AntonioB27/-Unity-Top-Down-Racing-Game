using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroupButtonHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer f1Sprite;
    [SerializeField] private SpriteRenderer gt3Sprite;
    [SerializeField] private SpriteRenderer rcSprite;
    [SerializeField] private SpriteRenderer p2Sprite;

    public void ChangeSceneF1(){
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene("SelectionF1");
    }

    public void ChangeSceneGT3(){
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene("SelectionGT3");
    }

    public void ChangeSceneRC(){
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene("SelectionRC");
    }

    public void Player2Mode(){
        PlayerPrefs.SetString("selectedGroup", "f1");
        PlayerPrefs.SetInt("mode", 2);
        PlayerPrefs.SetString("selectedTyre", "hard");
        SceneManager.LoadScene("BelgiumP2");
    }

    public void OnHoverSetBackgroundF1(){
        f1Sprite.enabled = true;
        gt3Sprite.enabled = false;
        rcSprite.enabled = false;
        p2Sprite.enabled = false;
    }

    public void OnHoverSetBackgroundGT3(){
        gt3Sprite.enabled = true;
        f1Sprite.enabled = false;
        rcSprite.enabled = false;
        p2Sprite.enabled = false;
    }

    public void OnHoverSetBackgroundRC(){
        rcSprite.enabled = true;
        gt3Sprite.enabled = false;
        p2Sprite.enabled = false;
        f1Sprite.enabled = false;
    }

    public void OnHoverSetBackground2P(){
        p2Sprite.enabled = true;
        rcSprite.enabled = false;
        gt3Sprite.enabled = false;
        f1Sprite.enabled = false;
    }

    public void ReturnToHome(){
        SceneManager.LoadScene("GroupSelection");
    }
}
