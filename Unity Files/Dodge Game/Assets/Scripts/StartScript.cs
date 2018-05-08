using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        buttonClickSound = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);
        SceneManager.LoadScene("Player_1_Login_Scene");
    }

    public void QuitGame()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);
        Application.Quit();
    }
}
