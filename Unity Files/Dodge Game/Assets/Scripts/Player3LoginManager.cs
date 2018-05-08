using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player3LoginManager : MonoBehaviour {

    int numberOfPlayers;
    public static int p3CharacterClass = 0;
    int p1CharacterClass;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    int p2CharacterClass;
    public GameObject p2StrikerCharacter;
    public GameObject p2BlockerCharacter;
    public GameObject player4Panel;

    public GameObject player3Panel;
    public bool p3IsStriker;
    public GameObject p3StrikerCharacter;
    public GameObject p3BlockerCharacter;
    public GameObject p3CharacterRightSelectButton;
    public GameObject p3CharacterLeftSelectButton;
    public UnityEngine.UI.Button p3CharacterSelectButton;
    public GameObject p3ReadyButton;
    public UnityEngine.UI.Button p3ReadyScreenButton;
    public GameObject p3NextButton;
    public UnityEngine.UI.Button p3NextScreenButton;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        numberOfPlayers = PlayerLoginManager.numberOfPlayers;
        p1CharacterClass = PlayerLoginManager.p1CharacterClass;
        p2CharacterClass = Player2LoginManager.p2CharacterClass;
        p3IsStriker = true;

        player3Panel.SetActive(true);

        p3CharacterSelectButton.Select();
        p3CharacterSelectButton.OnSelect(null);
        p3StrikerCharacter.SetActive(true);
        p3BlockerCharacter.SetActive(false);
        p3NextButton.SetActive(false);

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

        if (numberOfPlayers == 3)
        {
            player4Panel.SetActive(false);
        }

        if (numberOfPlayers == 4)
        {
            player4Panel.SetActive(true);
        }
    }

    public void Player3CharacterSelect()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        if (p3IsStriker)
        {
            p3IsStriker = false;
            p3CharacterClass = 1;
            p3StrikerCharacter.SetActive(false);
            p3BlockerCharacter.SetActive(true);
        }

        else
        {
            p3IsStriker = true;
            p3CharacterClass = 0;
            p3StrikerCharacter.SetActive(true);
            p3BlockerCharacter.SetActive(false);
        }
    }

    public void Player3Ready()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        p3CharacterRightSelectButton.SetActive(false);
        p3CharacterLeftSelectButton.SetActive(false);
        p3ReadyButton.SetActive(false);
        p3NextButton.SetActive(true);
        p3NextButton.SetActive(true);
        p3NextScreenButton.Select();
    }

    public void Next()
    {
        Debug.Log(numberOfPlayers);

        buttonClickSound.PlayOneShot(buttonClick, 1f);

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerClass("Player3", p3CharacterClass);

        if (numberOfPlayers == 3)
        {
            SceneManager.LoadScene("Level_Select");
        }

        if (numberOfPlayers > 3)
        {
            SceneManager.LoadScene("Player_4_Login_Scene");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Player_1_Login_Scene");
    }
}
