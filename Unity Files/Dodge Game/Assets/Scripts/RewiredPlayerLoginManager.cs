using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredPlayerLoginManager : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;

    public bool waitingToJoin, characterSelect, readyToPlay;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (player.GetAxis("Joy1Axis1") != 0.0f)
        {
            Debug.Log("Move Horizontal!");
        }

        if (player.GetButtonDown("Fire"))
        {
            Debug.Log("Fire!");
        }
    }
}