using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredPlayerLoginManager : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;

    //public bool waitingToJoin, characterSelect, readyToPlay;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (player.GetButtonDown("Submit"))
        {
            Debug.Log(playerId + "Fire!");
        }
    }
}