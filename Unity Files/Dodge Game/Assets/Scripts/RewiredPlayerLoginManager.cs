using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredPlayerLoginManager : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;

    public bool p1WaitingToJoin, p1CharacterSelect, p1ReadyToPlay;
    public bool p2WaitingToJoin, p2CharacterSelect, p2ReadyToPlay;
    public bool p3WaitingToJoin, p3CharacterSelect, p3ReadyToPlay;
    public bool p4WaitingToJoin, p4CharacterSelect, p4ReadyToPlay;

    public GameObject player1Panel;
    public bool p1IsStriker;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    public GameObject p1CharacterRightSelectButton;
    public GameObject p1CharacterLeftSelectButton;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (player.GetButtonDown("Submit"))
        {
            Debug.Log(playerId + " Fire!");
            //switch
        }
    }
}