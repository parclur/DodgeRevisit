using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class StartScript : MonoBehaviour {

    public int playerId;
    private Rewired.Player player;

    int menuState;

    public Graphic playButton, quitButton;
    public Color32 normalColor, highlightedColor, pressedColor;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        menuState = 0;

        normalColor = new Color32(97, 125, 139, 255);
        highlightedColor = new Color32(0, 96, 146, 255);
        pressedColor = new Color32(180, 197, 206, 255);

        playButton.GetComponent<Graphic>().color = highlightedColor;

        buttonClickSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (menuState == 0)
        {
            if (player.GetButtonDown("A"))
            {
                PlayGame();
            }

            if (player.GetAxis("LSH") == 1)
            {
                menuState = 1;
                playButton.GetComponent<Graphic>().color = normalColor;
                quitButton.GetComponent<Graphic>().color = highlightedColor;
            }
        }

        if (menuState == 1)
        {
            if (player.GetButtonDown("A"))
            {
                QuitGame();
            }

            if (player.GetAxis("LSH") == -1)
            {
                menuState = 0;
                playButton.GetComponent<Graphic>().color = highlightedColor;
                quitButton.GetComponent<Graphic>().color = normalColor;
            }
        }

    }

    public void PlayGame()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);
        SceneManager.LoadScene("Player_Login_Screen");
    }

    public void QuitGame()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);
        Application.Quit();
    }
}
