using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        buttonClickSound = GetComponent<AudioSource>();
    }
    public void RestartGame()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        SceneManager.LoadScene("Start_Menu");
    }
}
