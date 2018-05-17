using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredPlayerLoginManager : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;

    public int p1State, p2State, p3State, p4State;

    public GameObject p1JoinText;
    public GameObject p1Panel;
    public GameObject p1ReadyText;

    public bool p1IsStriker;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    public GameObject p1CharacterRightSelectButton;
    public GameObject p1CharacterLeftSelectButton;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        p1State = 0;
        p2State = 0;
        p3State = 0;
        p4State = 0;
    }

    void Update()
    {
        if (player.GetButtonDown("AButton"))
        {
            Debug.Log(playerId + " Advance");
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
            PlayerState();
        }

        if (player.GetButtonDown("BButton"))
        {
            Debug.Log(playerId + " Back");
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
            PlayerState();
        }
    }

    void PlayerState()
    {
        switch (p1State)
        {
            case -1:
                p1State = 0;
                break;
            //waiting to join
            case 0:
                Debug.Log("State: " + p1State);
                //show "Press A to Join"
                p1JoinText.SetActive(true);
                p1Panel.SetActiveRecursively(false);
                break;
            //character select
            case 1:
                Debug.Log("State: " + p1State);
                //show character panel
                p1JoinText.SetActive(false);
                p1Panel.SetActiveRecursively(true);
                p1ReadyText.SetActive(false);
                break;
            //ready
            case 2:
                Debug.Log("State: " + p1State);
                //show ready text and advance if all joined players are
                p1ReadyText.SetActive(true);
                break;
            case 3:
                p1State = 2;
                break;
        }
    }
}