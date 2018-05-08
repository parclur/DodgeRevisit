using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2LoginManager : MonoBehaviour {

    int numberOfPlayers;
    public static int p2CharacterClass = 0;

    int p1CharacterClass;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    public GameObject player3Panel;
    public GameObject player4Panel;

    public GameObject player2Panel;
    public bool p2IsStriker;
    public GameObject p2StrikerCharacter;
    public GameObject p2BlockerCharacter;
    public GameObject p2CharacterRightSelectButton;
    public GameObject p2CharacterLeftSelectButton;
    public UnityEngine.UI.Button p2CharacterSelectButton;
    public GameObject p2ReadyButton;
    public UnityEngine.UI.Button p2ReadyScreenButton;
    public GameObject p2NextButton;
    public UnityEngine.UI.Button p2NextScreenButton;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        numberOfPlayers = PlayerLoginManager.numberOfPlayers;
        p1CharacterClass = PlayerLoginManager.p1CharacterClass;
        p2IsStriker = true;

        player2Panel.SetActive(true);

        p2CharacterSelectButton.Select();
        p2CharacterSelectButton.OnSelect(null);
        p2StrikerCharacter.SetActive(true);
        p2BlockerCharacter.SetActive(false);
        p2NextButton.SetActive(false);

        buttonClickSound = GetComponent<AudioSource>();

        if (p1CharacterClass == 0)
        {
            p1StrikerCharacter.SetActive(true);
            p1BlockerCharacter.SetActive(false);
        }

        if (p1CharacterClass == 1)
        {
            p1StrikerCharacter.SetActive(false);
            p1BlockerCharacter.SetActive(true);
        }

        if (numberOfPlayers == 2)
        {
            player3Panel.SetActive(false);
            player4Panel.SetActive(false);
        }

        if (numberOfPlayers == 3)
        {
            player3Panel.SetActive(true);
            player4Panel.SetActive(false);
        }

        if (numberOfPlayers == 4)
        {
            player3Panel.SetActive(true);
            player4Panel.SetActive(true);
        }
    }

    public void Player2CharacterSelect()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        if (p2IsStriker)
        {
            p2IsStriker = false;
            p2CharacterClass = 1;
            p2StrikerCharacter.SetActive(false);
            p2BlockerCharacter.SetActive(true);
        }

        else
        {
            p2IsStriker = true;
            p2CharacterClass = 0;
            p2StrikerCharacter.SetActive(true);
            p2BlockerCharacter.SetActive(false);
        }
    }

    public void Player2Ready()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        p2CharacterRightSelectButton.SetActive(false);
        p2CharacterLeftSelectButton.SetActive(false);
        p2ReadyButton.SetActive(false);
        p2NextButton.SetActive(true);
        p2NextScreenButton.Select();
    }
    
    public void Next()
    {
        Debug.Log(numberOfPlayers);

        buttonClickSound.PlayOneShot(buttonClick, 1f);

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerClass("Player2", p2CharacterClass);

        if (numberOfPlayers == 2)
        {
            SceneManager.LoadScene("Level_Select");
        }

        if (numberOfPlayers > 2)
        {
            SceneManager.LoadScene("Player_3_Login_Scene");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Player_1_Login_Scene");
    }
}
