using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player4LoginManager : MonoBehaviour {

    int numberOfPlayers;
    public static int p4CharacterClass = 0;
    int p1CharacterClass;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    int p2CharacterClass;
    public GameObject p2StrikerCharacter;
    public GameObject p2BlockerCharacter;
    int p3CharacterClass;
    public GameObject p3StrikerCharacter;
    public GameObject p3BlockerCharacter;

    public GameObject player4Panel;
    public bool p4IsStriker;
    public GameObject p4StrikerCharacter;
    public GameObject p4BlockerCharacter;
    public GameObject p4CharacterRightSelectButton;
    public GameObject p4CharacterLeftSelectButton;
    public UnityEngine.UI.Button p4CharacterSelectButton;
    public GameObject p4ReadyButton;
    public UnityEngine.UI.Button p4ReadyScreenButton;
    public GameObject p4NextButton;
    public UnityEngine.UI.Button p4NextScreenButton;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        numberOfPlayers = PlayerLoginManager.numberOfPlayers;
        p1CharacterClass = PlayerLoginManager.p1CharacterClass;
        p2CharacterClass = Player2LoginManager.p2CharacterClass;
        p3CharacterClass = Player3LoginManager.p3CharacterClass;
        p4IsStriker = true;

        player4Panel.SetActive(true);

        p4CharacterSelectButton.Select();
        p4CharacterSelectButton.OnSelect(null);
        p4StrikerCharacter.SetActive(true);
        p4BlockerCharacter.SetActive(false);
        p4NextButton.SetActive(false);

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

        if (p2CharacterClass == 0)
        {
            p2StrikerCharacter.SetActive(true);
            p2BlockerCharacter.SetActive(false);
        }

        if (p2CharacterClass == 1)
        {
            p2StrikerCharacter.SetActive(false);
            p2BlockerCharacter.SetActive(true);
        }

        if (p3CharacterClass == 0)
        {
            p3StrikerCharacter.SetActive(true);
            p3BlockerCharacter.SetActive(false);
        }

        if (p3CharacterClass == 1)
        {
            p3StrikerCharacter.SetActive(false);
            p3BlockerCharacter.SetActive(true);
        }
    }

    public void Player4CharacterSelect()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        if (p4IsStriker)
        {
            p4IsStriker = false;
            p4CharacterClass = 1;
            p4StrikerCharacter.SetActive(false);
            p4BlockerCharacter.SetActive(true);
        }

        else
        {
            p4IsStriker = true;
            p4CharacterClass = 0;
            p4StrikerCharacter.SetActive(true);
            p4BlockerCharacter.SetActive(false);
        }
    }

    public void Player4Ready()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        p4CharacterRightSelectButton.SetActive(false);
        p4CharacterLeftSelectButton.SetActive(false);
        p4ReadyButton.SetActive(false);
        p4NextButton.SetActive(true);
        p4NextButton.SetActive(true);
        p4NextScreenButton.Select();
    }

    public void Next()
    {
        Debug.Log(numberOfPlayers);

        buttonClickSound.PlayOneShot(buttonClick, 1f);

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerClass("Player4", p4CharacterClass);

        SceneManager.LoadScene("Level_Select");
    }

    public void Back()
    {
        SceneManager.LoadScene("Player_1_Login_Scene");
    }
}
