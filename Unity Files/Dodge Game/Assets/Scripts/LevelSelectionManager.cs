using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour {

    public GameObject levelSelectionCanvas;
    public GameObject roundSelectionCanvas;

    public static int numberOfRounds = 1;
    public Text numberOfRoundsText;

    public GameObject level1Button;
    public UnityEngine.UI.Button level1SelectionButton;

    public AudioClip buttonClick;
    AudioSource buttonClickSound;

    void Start()
    {
        levelSelectionCanvas.SetActive(false);
        roundSelectionCanvas.SetActive(true);

        buttonClickSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        numberOfRoundsText.text = numberOfRounds.ToString();
        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetMaxRounds(numberOfRounds);

    }

    public void MoreRounds()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        numberOfRounds++;

        if (numberOfRounds > 15)
        {
            numberOfRounds = 1;
        }

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetMaxRounds(numberOfRounds);
    }

    public void LessRounds()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        numberOfRounds--;

        if (numberOfRounds < 1)
        {
            numberOfRounds = 15;
        }

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetMaxRounds(numberOfRounds);
    }

    public void Next()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        levelSelectionCanvas.SetActive(true);
        roundSelectionCanvas.SetActive(false);
        level1SelectionButton.Select();
    }

    public void Level1()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        SceneManager.LoadScene("Test_Level_2");
    }

    public void Level3()
    {
        buttonClickSound.PlayOneShot(buttonClick, 1f);

        SceneManager.LoadScene("Test_Level_3");
    }
}
