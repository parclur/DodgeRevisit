using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class OptionsManager : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;
    
    int menuState;
    bool axisActive;
    float axisValue;

    public GameObject arrow;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        menuState = 0;
        axisActive = false;
        axisValue = 0f;

        buttonClickSound = GetComponent<AudioSource>();

        //(-261.4, -61.9) (-261.4, -222.5) (-261.4, -387)
    }

    void Update()
    {
        //Makes the axis like a button and only moved once
        if (player.GetAxis("LSV") != 0)
        {
            if (!axisActive)
            {
                //MoveCursor();
                axisActive = true;
            }
        }
        if (player.GetAxis("LSV") == 0)
        {
            axisActive = false;
            axisValue = 0f;
        }

        if (player.GetButtonDown("A"))
        {
            switch (menuState)
            {
                case 0:
                    //PlayGame();
                    break;

                case 1:
                    //GameOptions();
                    break;

                case 2:
                    //QuitGame();
                    break;
            }
        }
    }
}
