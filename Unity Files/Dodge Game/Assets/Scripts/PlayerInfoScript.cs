using System.Collections;
using UnityEngine;
using Rewired;

public class PlayerInfoScript : MonoBehaviour {

    // player info for team and character
    public int infoTeam;
    public int characterClass;
    public int playerNum;

    // info whether a player exists
    public bool ableToSpawn;
    public bool isOut = false;

    // the name for the player's controls
    public string playerHor = "LSH";
    public string playerVer = "LSV";
    public string playerJump = "A";
    public string playerPickup = "X";
    public string playerAimHor = "RSH";
    public string playerAimVer = "RSV";
    public string playerThrow = "RT";
    public string playerShield = "LT";
    

    Vector2 spawn;

    string ballSavedName;
    int numBalls = 0;
    int maxBalls = 1;

    Rewired.Player player;
    CharacterController charController;

    SpriteRenderer spriteRen;
    Animator anim;

    public GameObject ballPrefab;
    public GameObject ballUI;

    // Use this for initialization
    void Start () {
        spriteRen = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        //anim.SetBool("Throwing", false);
        //anim.SetBool("Catching", false);
        //anim.SetBool("Dashing", false);

        //anim.SetBool("Throwing", false);

        if (!isOut && ableToSpawn)
        {

            if (!isOut && ableToSpawn)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }

    void SetPlayerNum()
    {
        if (tag == "Player1")
        {
            playerNum = 0;

        }
        else if (tag == "Player2")
        {
            playerNum = 1;

        }
        else if (tag == "Player3")
        {
            playerNum = 2;

        }
        else if (tag == "Player4")
        {
            playerNum = 3;

        }

        player = ReInput.players.GetPlayer(playerNum);

        charController = GetComponent<CharacterController>();
    }

    public void KillPlayer()
    {
        if (numBalls > 0)
        {
            GameObject ball = Instantiate(ballPrefab, gameObject.transform);
            ball.name = ballSavedName;
            numBalls--;
        }

        isOut = true;
    }

    public void ResetPos()
    {
        gameObject.transform.position = spawn;
    }

    public void ResetPlayer()
    {
        if (ableToSpawn)
        {
            gameObject.transform.position = spawn;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<PlayerSkillScript>().SetShieldHealth(1);
            spriteRen.flipX = gameObject.transform.position.x > 0;

            ballUI.SetActive(false);

            if (numBalls > 0)
            {
                GameObject ball = Instantiate(ballPrefab);
                ball.name = ballSavedName;
                numBalls = 0;
                ball.GetComponent<BallScript>().ResetPos();
            }

            gameObject.SetActive(true);
            anim.SetInteger("CharacterClass", characterClass);

        }

    }

    void InitPlayer() //TODO add multiplayer options
    {
        spawn = gameObject.transform.position;


        if (gameObject.tag == "Player1")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Blue_1").transform.position;

        }
        else if (gameObject.tag == "Player2")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 2 || 
                GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() == 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Red_1").transform.position;


        }
        else if (gameObject.tag == "Player3")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 3 || 
                GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() == 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Blue_2").transform.position;


        }
        else if (gameObject.tag == "Player4")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 4 || 
                GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() == 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Red_2").transform.position;

        }
    }

}
