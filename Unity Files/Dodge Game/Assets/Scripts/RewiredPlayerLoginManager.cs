using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class RewiredPlayerLoginManager : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;

    public static int numberOfPlayers = 0;
    public static int p1CharacterClass = 0, p2CharacterClass = 0, p3CharacterClass = 0, p4CharacterClass = 0;
    int numberOfReadyPlayers;

    public int p1State, p2State, p3State, p4State;

    public GameObject p1JoinText, p2JoinText, p3JoinText, p4JoinText;
    public GameObject p1Panel, p2Panel, p3Panel, p4Panel;
    public GameObject p1ReadyText, p2ReadyText, p3ReadyText, p4ReadyText;

    public GameObject p1StrikerCharacter, p2StrikerCharacter, p3StrikerCharacter, p4StrikerCharacter;
    public GameObject p1BlockerCharacter, p2BlockerCharacter, p3BlockerCharacter, p4BlockerCharacter;
    public Graphic p1CharacterRightSelectButton, p2CharacterRightSelectButton, p3CharacterRightSelectButton, p4CharacterRightSelectButton;
    public Graphic p1CharacterLeftSelectButton, p2CharacterLeftSelectButton, p3CharacterLeftSelectButton, p4CharacterLeftSelectButton;

    Color32 p1Color, p1SelectedColor;
    Color32 p2Color, p2SelectedColor;
    Color32 p3Color, p3SelectedColor;
    Color32 p4Color, p4SelectedColor;

    bool p1Join, p2Join, p3Join, p4Join;
    bool p1Ready, p2Ready, p3Ready, p4Ready;

    public GameObject startTextPanel;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        p1State = 0;
        p2State = 0;
        p3State = 0;
        p4State = 0;

        numberOfReadyPlayers = 0;

        //Sets colors for the character select "buttons"
        p1Color = new Color32(103, 88, 255, 255);
        p1SelectedColor = new Color32(217, 214, 255, 255);

        p2Color = new Color32(255, 143, 0, 255);
        p2SelectedColor = new Color32(255, 220, 176, 255);

        p3Color = new Color32(0, 244, 255, 255);
        p3SelectedColor = new Color32(218, 252, 253, 255);

        p4Color = new Color32(249, 17, 17, 255);
        p4SelectedColor = new Color32(238, 172, 172, 255);

        p1Join = false;
        p2Join = false;
        p3Join = false;
        p4Join = false;

        p1Ready = false;
        p2Ready = false;
        p3Ready = false;
        p4Ready = false;

        startTextPanel.SetActive(false);

        buttonClickSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Advances the player through the menu
        if (player.GetButtonDown("A"))
        {
            buttonClickSound.PlayOneShot(buttonClick, 1f);

            //Debug.Log(playerId + " Advance");
            switch (playerId)
            {
                //Player 1
                case 0:
                    p1State++;
                    break;

                //Player 2
                case 1:
                    p2State++;
                    break;

                //Player 3
                case 2:
                    p3State++;
                    break;

                //Player 4
                case 3:
                    p4State++;
                    break;
            }
            Player1State();
            Player2State();
            Player3State();
            Player4State();
        }

        //Regresses the player through the menu
        if (player.GetButtonDown("B"))
        {
            buttonClickSound.PlayOneShot(buttonClick, 1f);

            //Debug.Log(playerId + " Back");
            switch (playerId)
            {
                //Player 1
                case 0:
                    p1State--;
                    break;

                //Player 2
                case 1:
                    p2State--;
                    break;

                //Player 3
                case 2:
                    p3State--;
                    break;

                //Player 4
                case 3:
                    p4State--;
                    break;
            }
            Player1State();
            Player2State();
            Player3State();
            Player4State();
        }

        //Starts the game once players are ready
        if (CheckReadyPlayers() && player.GetButtonDown("Start"))
        {
            Debug.Log("Start Game with " + numberOfPlayers + " Players");
            SceneManager.LoadScene("Level_Select");
        }

        //Returns "buttons" to their original color
        if (player.GetAxis("LSH") == 0)
        {
            p1CharacterLeftSelectButton.GetComponent<Graphic>().color = p1Color;
            p1CharacterRightSelectButton.GetComponent<Graphic>().color = p1Color;

            p2CharacterLeftSelectButton.GetComponent<Graphic>().color = p2Color;
            p2CharacterRightSelectButton.GetComponent<Graphic>().color = p2Color;

            p3CharacterLeftSelectButton.GetComponent<Graphic>().color = p3Color;
            p3CharacterRightSelectButton.GetComponent<Graphic>().color = p3Color;

            p4CharacterLeftSelectButton.GetComponent<Graphic>().color = p4Color;
            p4CharacterRightSelectButton.GetComponent<Graphic>().color = p4Color;
        }

        //character select right
        if (player.GetAxis("LSH") == -1 || player.GetAxis("LSH") == 1)
        {
            //Debug.Log(playerId + " Switch Character");
            switch (playerId)
            {
                //Player 1
                case 0:
                    //Debug.Log("State: " + p1State);
                    //Only let the player change their character in the character select state
                    if (p1State == 1)
                    {
                        //Changes the color of the correct button
                        if (player.GetAxis("LSH") == -1 && playerId == 0)
                        {
                            p1CharacterLeftSelectButton.GetComponent<Graphic>().color = p1SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }
                        if (player.GetAxis("LSH") == 1 && playerId == 0)
                        {
                            p1CharacterRightSelectButton.GetComponent<Graphic>().color = p1SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }

                        //Changes the player's character
                        if (p1CharacterClass == 0) //switching from the striker to the blocker
                        {
                            p1CharacterClass = 1;
                            p1StrikerCharacter.SetActive(false);
                            p1BlockerCharacter.SetActive(true);
                        }
                        else if (p1CharacterClass == 1) //switching from the blocker to the striker
                        {
                            p1CharacterClass = 0;
                            p1StrikerCharacter.SetActive(true);
                            p1BlockerCharacter.SetActive(false);
                        }
                    }
                    break;

                //Player 2
                case 1:
                    //Only let the player change their character in the character select state
                    if (p2State == 1)
                    {
                        //Changes the color of the correct button
                        if (player.GetAxis("LSH") == -1 && playerId == 1)
                        {
                            p2CharacterLeftSelectButton.GetComponent<Graphic>().color = p2SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }
                        if (player.GetAxis("LSH") == 1 && playerId == 1)
                        {
                            p2CharacterRightSelectButton.GetComponent<Graphic>().color = p2SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }

                        //Changes the player's character
                        if (p2CharacterClass == 0) //switching from the striker to the blocker
                        {
                            p2CharacterClass = 1;
                            p2StrikerCharacter.SetActive(false);
                            p2BlockerCharacter.SetActive(true);
                        }
                        else if (p2CharacterClass == 1) //switching from the blocker to the striker
                        {
                            p2CharacterClass = 0;
                            p2StrikerCharacter.SetActive(true);
                            p2BlockerCharacter.SetActive(false);
                        }
                    }
                    break;

                //Player 3
                case 2:
                    //Only let the player change their character in the character select state
                    if (p3State == 1)
                    {
                        //Changes the color of the correct button
                        if (player.GetAxis("LSH") == -1 && playerId == 2)
                        {
                            p3CharacterLeftSelectButton.GetComponent<Graphic>().color = p3SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }
                        if (player.GetAxis("LSH") == 1 && playerId == 2)
                        {
                            p3CharacterRightSelectButton.GetComponent<Graphic>().color = p3SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }

                        //Changes the player's character
                        if (p3CharacterClass == 0) //switching from the striker to the blocker
                        {
                            p3CharacterClass = 1;
                            p3StrikerCharacter.SetActive(false);
                            p3BlockerCharacter.SetActive(true);
                        }
                        else if (p1CharacterClass == 1) //switching from the blocker to the striker
                        {
                            p3CharacterClass = 0;
                            p3StrikerCharacter.SetActive(true);
                            p3BlockerCharacter.SetActive(false);
                        }
                    }
                    break;

                //Player 4
                case 3:
                    //Only let the player change their character in the character select state
                    if (p4State == 1)
                    {
                        //Changes the color of the correct button
                        if (player.GetAxis("LSH") == -1 && playerId == 3)
                        {
                            p4CharacterLeftSelectButton.GetComponent<Graphic>().color = p4SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }
                        if (player.GetAxis("LSH") == 1 && playerId == 3)
                        {
                            p4CharacterRightSelectButton.GetComponent<Graphic>().color = p4SelectedColor;
                            buttonClickSound.PlayOneShot(buttonClick, 1f);
                        }

                        //Changes the player's character
                        if (p4CharacterClass == 0) //switching from the striker to the blocker
                        {
                            p4CharacterClass = 1;
                            p4StrikerCharacter.SetActive(false);
                            p4BlockerCharacter.SetActive(true);
                        }
                        else if (p4CharacterClass == 1) //switching from the blocker to the striker
                        {
                            p4CharacterClass = 0;
                            p4StrikerCharacter.SetActive(true);
                            p4BlockerCharacter.SetActive(false);
                        }
                    }
                    break;
            }
        }

        //Hot Key to start game
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Level_Select");
        }
    }

    void Player1State()
    {
        switch (p1State)
        {
            //Sets a minimum for the states so that the player can advance immediately
            case -1:
                p1State = 0;
                SceneManager.LoadScene("Start_Menu");
                break;

            //Waiting to join state
            case 0:
                //Debug.Log("State: " + p1State);
                //show "Press A to Join"
                p1JoinText.SetActive(true);
                p1Panel.SetActive(false);
                p1Join = false;
                break;

            //Character select state
            case 1:
                //Debug.Log("State: " + p1State);
                //show character panel
                p1JoinText.SetActive(false);
                p1Panel.SetActive(true);
                p1ReadyText.SetActive(false);
                p1Join = true;
                p1Ready = false;
                break;

            //Ready state
            case 2:
                //Debug.Log("State: " + p1State);
                //show ready text and advance if all joined players are
                p1ReadyText.SetActive(true);
                numberOfPlayers = 1;
                p1Ready = true;
                GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerAmount(numberOfPlayers);
                break;

            //Sets a maximum for the states so that the player can regress immediately
            case 3:
                p1State = 2;
                break;
        }
    }

    void Player2State()
    {
        switch (p2State)
        {
            //Sets a minimum for the states so that the player can advance immediately
            case -1:
                p2State = 0;
                break;

            //Waiting to join state
            case 0:
                //Debug.Log("State: " + p1State);
                //show "Press A to Join"
                p2JoinText.SetActive(true);
                p2Panel.SetActive(false);
                p2Join = false;
                break;

            //Character select state
            case 1:
                //Debug.Log("State: " + p1State);
                //show character panel
                p2JoinText.SetActive(false);
                p2Panel.SetActive(true);
                p2ReadyText.SetActive(false);
                p2Join = true;
                p2Ready = false;
                break;

            //Ready state
            case 2:
                //Debug.Log("State: " + p1State);
                //show ready text and advance if all joined players are
                p2ReadyText.SetActive(true);
                p2Ready = true;
                numberOfPlayers = 2;
                GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerAmount(numberOfPlayers);
                break;

            //Sets a maximum for the states so that the player can regress immediately
            case 3:
                p1State = 2;
                break;
        }
    }

    void Player3State()
    {
        switch (p3State)
        {
            //Sets a minimum for the states so that the player can advance immediately
            case -1:
                p3State = 0;
                break;

            //Waiting to join state
            case 0:
                //Debug.Log("State: " + p1State);
                //show "Press A to Join"
                p3JoinText.SetActive(true);
                p3Panel.SetActive(false);
                p2Join = false;
                break;

            //Character select state
            case 1:
                //Debug.Log("State: " + p1State);
                //show character panel
                p3JoinText.SetActive(false);
                p3Panel.SetActive(true);
                p3ReadyText.SetActive(false);
                p2Join = true;
                p2Ready = false;
                break;

            //Ready state
            case 2:
                //Debug.Log("State: " + p1State);
                //show ready text and advance if all joined players are
                p3ReadyText.SetActive(true);
                p2Ready = true;
                numberOfPlayers = 3;
                GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerAmount(numberOfPlayers);
                break;

            //Sets a maximum for the states so that the player can regress immediately
            case 3:
                p3State = 2;
                break;
        }
    }

    void Player4State()
    {
        switch (p4State)
        {
            //Sets a minimum for the states so that the player can advance immediately
            case -1:
                p4State = 0;
                break;

            //Waiting to join state
            case 0:
                //Debug.Log("State: " + p1State);
                //show "Press A to Join"
                p4JoinText.SetActive(true);
                p4Panel.SetActive(false);
                p4Join = false;
                break;

            //Character select state
            case 1:
                //Debug.Log("State: " + p1State);
                //show character panel
                p4JoinText.SetActive(false);
                p4Panel.SetActive(true);
                p4ReadyText.SetActive(false);
                p4Join = true;
                p4Ready = false;
                break;

            //Ready state
            case 2:
                //Debug.Log("State: " + p1State);
                //show ready text and advance if all joined players are
                p4ReadyText.SetActive(true);
                p4Ready = true;
                numberOfPlayers = 4;
                GameObject.Find("GameManager").GetComponent<ManagerScript>().SetPlayerAmount(numberOfPlayers);
                break;

            //Sets a maximum for the states so that the player can regress immediately
            case 3:
                p4State = 2;
                break;
        }
    }

    bool CheckReadyPlayers()
    {
        //if numplayersready == numplayers, then show "Press start to play"
        if (p1Join == p1Ready && p2Join == p2Ready && p3Join == p3Ready && p4Join == p4Ready)
        {
            startTextPanel.SetActive(true);
            return true;
        }

        return false;
    }
}