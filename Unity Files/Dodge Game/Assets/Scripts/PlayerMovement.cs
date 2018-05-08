using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    string playerType; // this will be the type the player is (ex: blocer or striker)

    Vector2 spawn;

    float playerSpeed = 10f;
    float jumpForce = 800f;
	float throwSpeed = 100f;

    public bool ableToThrow = false;
    public bool ableToPickUp = true;
    public bool ableToShield = true;
    bool rightFacing;
    bool ableToJump = true;
    bool canBeHit = true;

    public bool onGround;
	public LayerMask ground;
	float groundTimer = 0f;

    Rigidbody2D rig;

    string playerHor;
    string playerVer;
    string playerJump;
	string playerPickup;
	string playerAimHor;
	string playerAimVer;
	string playerThrow;
    string playerShield;

	public CircleCollider2D pickupRad;

    string ballSavedName;
	int numBalls = 0;
	int maxBalls = 1;
    int shieldHealth = 1;
    int dashAmount = 1;
    float speedMultiplier = 1.0f;

    public bool isOut = false;
    bool canCheckDash = true;

	public GameObject ballPrefab;
    public GameObject shieldPrefab;
    public GameObject cursorPrefab;
	public GameObject explosionPrefab;

	public int team;

	Color color;

	Animator anim;
	SpriteRenderer sr;

    bool ableToSpawn = true;

	enum State{
		IDLE = 0, JUMPING, RUNNING	
	};
	public int characterClass;

	public GameObject ballUI;
	public GameObject rechargeUI;

	bool aDown = false;

	// Use this for initialization
	void Start () {
		
		InitPlayer();

		if (ableToSpawn) {
			characterClass = GameObject.Find ("GameManager").GetComponent<ManagerScript> ().GetPlayerClass (gameObject.name);

			cursorPrefab = Instantiate (cursorPrefab);
			shieldPrefab = Instantiate (shieldPrefab);
			cursorPrefab.transform.parent = gameObject.transform;
			shieldPrefab.transform.parent = gameObject.transform;
			cursorPrefab.transform.position = gameObject.transform.position;
			shieldPrefab.transform.position = gameObject.transform.position;
			cursorPrefab.name = "Cursor";
			shieldPrefab.name = "Shield";
			shieldPrefab.SetActive (false);

			ballUI.SetActive (false);

			rig = GetComponent<Rigidbody2D> ();
			onGround = false;
			color = GetComponent<SpriteRenderer> ().color;

			anim = GetComponent<Animator> ();
			sr = GetComponent<SpriteRenderer> ();
			anim.SetInteger ("CharacterClass", characterClass);


			if (characterClass == 1) {
				gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (1.22f, 1.44f);
				gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (-0.089f, -0.28f);
			}
		}
    }
	

	// Update is called once per frame
	void Update () {

        if (!isOut && ableToSpawn)
        {
			anim.SetBool ("Throwing", false);
			anim.SetBool ("Catching", false);
			anim.SetBool ("Dashing", false);
            gameObject.SetActive(true);
            SetCursor();
            CheckGrounded();
            CheckPickup();
            CheckMove();

            if(Input.GetAxis(playerShield) < 1)
            {
                CheckThrow();
            }

            if (characterClass == 0)
                Dash();
            else if (characterClass == 1)
                CheckShield2();

			rechargeUI.SetActive (canCheckDash && ableToShield);
        }
        else
        {
            gameObject.SetActive(false);
        }

	}
		

    public void NotOutAnymore()
    {
        isOut = false;
    }


    public void ResetPos()
    {
        gameObject.transform.position = spawn;
    }


    public void ResetPlayer()
    {
        if(ableToSpawn)
        {
            gameObject.transform.position = spawn;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            shieldHealth = 1;
            sr.flipX = gameObject.transform.position.x > 0;

            ballUI.SetActive(false);

            if (numBalls > 0)
            {
                GameObject ball = Instantiate(ballPrefab);
                ball.name = ballSavedName;
                numBalls = 0;
                ball.GetComponent<BallScript>().ResetPos();
            }

            isOut = false;
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
            playerHor = "P1LSH";
            playerVer = "P1LSV";
            playerJump = "P1A";
            playerPickup = "P1X";
            playerAimHor = "P1RSH";
            playerAimVer = "P1RSV";
            playerThrow = "P1RT";
            playerShield = "P1LT";
        }
        else if (gameObject.tag == "Player2")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 2 || GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() == 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Red_1").transform.position;
            playerHor = "P2LSH";
            playerVer = "P2LSV";
            playerJump = "P2A";
            playerPickup = "P2X";
            playerAimHor = "P2RSH";
            playerAimVer = "P2RSV";
            playerThrow = "P2RT";
            playerShield = "P2LT";

        }
        else if (gameObject.tag == "Player3")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 3 || GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() == 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Blue_2").transform.position;
            playerHor = "P3LSH";
            playerVer = "P3LSV";
            playerJump = "P3A";
            playerPickup = "P3X";
            playerAimHor = "P3RSH";
            playerAimVer = "P3RSV";
            playerThrow = "P3RT";
            playerShield = "P3LT";

        }
        else if (gameObject.tag == "Player4")
        {
            if (GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() >= 4 || GameObject.Find("GameManager").GetComponent<ManagerScript>().GetNumPlayers() == 1)
            {
                ableToSpawn = true;
            }
            else
            {
                ableToSpawn = false;
            }

            spawn = GameObject.Find("Player_Start_Point_Red_2").transform.position;
            playerHor = "P4LSH";
            playerVer = "P4LSV";
            playerJump = "P4A";
            playerPickup = "P4X";
            playerAimHor = "P4RSH";
            playerAimVer = "P4RSV";
            playerThrow = "P4RT";
            playerShield = "P4LT";

        }
    }


    void CheckMove()
    {
        float xMove = Input.GetAxis(playerHor);
        float yMove = Input.GetAxis(playerJump);
        
		rig.velocity = new Vector2(xMove * playerSpeed * speedMultiplier, rig.velocity.y);

        if (xMove > 0)
        {
			sr.flipX = false;
            rightFacing = true;
        }
		if (xMove < 0)
        {
			sr.flipX = true;
            rightFacing = false;
        }


		if (yMove != 0 && onGround && !aDown)
		{
			onGround = false;
			groundTimer = 0.1f;

			anim.SetInteger ("State", (int)State.JUMPING);

			rig.AddForce (Vector2.up * 1.25f * jumpForce);
		}
		else if (xMove != 0 && onGround)
		{
			anim.SetInteger ("State", (int)State.RUNNING);
		}
		else if (onGround)
		{
			anim.SetInteger ("State", (int)State.IDLE);
		}

		aDown = yMove != 0;

    }


	void CheckGrounded()
	{
		if (groundTimer == 0f)
		{
			RaycastHit2D rcLeft, rcRight, rcCenter;

            rcLeft = Physics2D.Raycast (new Vector2 (transform.position.x - 0.5f, transform.position.y - 1.05f), Vector2.down, 0.05f, ground);
			rcRight = Physics2D.Raycast (new Vector2 (transform.position.x + 0.5f, transform.position.y - 1.05f), Vector2.down, 0.05f, ground);
			rcCenter = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - 1.05f), Vector2.down, 0.05f, ground);


            if (rcLeft.transform != null || rcRight.transform != null || rcCenter.transform != null)
            {
                onGround = true;
                anim.SetInteger("State", (int)State.IDLE);
            }
            else
			{
				onGround = false;
				if (anim.GetInteger("State") == (int)State.RUNNING)
				{
					anim.SetInteger ("State", (int)State.IDLE);
				}
			}
		}
		else
		{
			groundTimer -= Time.deltaTime;
			if (groundTimer <= 0f)
			{
				groundTimer = 0f;
			}
		}
	}


	void CheckPickup()
	{
		if (Input.GetAxis (playerPickup) != 0 && ableToPickUp) {
			ableToPickUp = false;
			ableToThrow = false;


			Collider2D[] hits = Physics2D.OverlapCircleAll (pickupRad.bounds.center, pickupRad.radius, LayerMask.GetMask ("Ball"));
			for (int i = 0; i < hits.GetLength (0) && numBalls < maxBalls; i++) {
				ballSavedName = hits [i].gameObject.name;
				numBalls++;
				Destroy (hits [i].gameObject);
				anim.SetBool ("Catching", true);
				ballUI.SetActive (true);
			}
		} else if (Input.GetAxis (playerPickup) == 0 && !ableToPickUp) {
			StartCoroutine(AbleToPickUpAgain());
		}

	}


    void SetCursor()
    {
        float spawnX;
        float spawnY;

        if(Mathf.Abs(Input.GetAxis(playerAimHor)) > 0 || Mathf.Abs(Input.GetAxis(playerAimVer)) > 0)
        {
            spawnX = Input.GetAxis(playerAimHor);
            spawnY = Input.GetAxis(playerAimVer);
        }
        else
        {
            spawnX = Input.GetAxis(playerHor);
            spawnY = Input.GetAxis(playerVer);
        }

        cursorPrefab.transform.position = new Vector2(gameObject.transform.position.x + spawnX, gameObject.transform.position.y + spawnY);
    }

	void CheckThrow()
	{

        if (Input.GetAxis(playerThrow) != 0 && numBalls > 0 && ableToThrow)
		{
            Debug.Log("Thowing");
			anim.SetBool ("Throwing", true);
			ballUI.SetActive (false);

            float xMag = Input.GetAxis(playerAimHor);
            float yMag = Input.GetAxis(playerAimVer);
            float xMag2 = Input.GetAxis(playerHor);
            float yMag2 = Input.GetAxis(playerVer);

            GameObject ball = Instantiate (ballPrefab) as GameObject;

            float spawnX = gameObject.transform.position.x;
            float spawnY = gameObject.transform.position.y;

            // might have to create a new set for mag2 then mag1 for overriding
            // test to see how things work with this tho
            if (xMag2 > 0)
            {
                spawnX = gameObject.transform.position.x + 1.0f;
            }
            else if (xMag2 < 0)
            {
                spawnX = gameObject.transform.position.x - 1.0f;
            }

            if (yMag2 > 0)
            {
                spawnY = gameObject.transform.position.y + 1.0f;
            }
            else if (yMag2 < 0)
            {
                spawnY = gameObject.transform.position.y - 1.0f;
            }

            if (xMag > 0)
            {
                sr.flipX = false;
                rightFacing = true;
                
                spawnX = gameObject.transform.position.x + 1.0f;
            }
            else if(xMag < 0)
            {
                sr.flipX = true; ;
                rightFacing = false;

                spawnX = gameObject.transform.position.x - 1.0f;

            }

            if (yMag > 0)
            {
                spawnY = gameObject.transform.position.y + 1.0f;
            }
            else if (yMag < 0)
            {
                spawnY = gameObject.transform.position.y - 1.0f;

            }
            

            ball.transform.position = new Vector2(spawnX, spawnY);
            ball.name = ballSavedName;
            ball.GetComponent<BallScript>().possession = team;
			ball.GetComponent<BallScript>().UpdateColor();
            ball.GetComponent<BallScript>().SetThrower(gameObject);

            if (xMag != 0 || yMag != 0)
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed * xMag * 0.5f, throwSpeed * yMag * 0.5f);
            else
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed * xMag2 * 0.5f, throwSpeed * yMag2 * 0.5f);

            numBalls--;
            ableToThrow = false;
		}

        if (numBalls == 0)
        {
            //StartCoroutine(AbleToPickUpAgain());
        }
        else
            StartCoroutine(AbleToShootAgain());
	}

    void CheckShield2()
    {
        if (shieldHealth > 0 && Input.GetAxis(playerShield) > 0)
        {

            Debug.Log("Shielding");
            float xMag = Input.GetAxis(playerAimHor);
            float yMag = Input.GetAxis(playerAimVer);
            float xMag2 = Input.GetAxis(playerHor);
            float yMag2 = Input.GetAxis(playerVer);

            // activate the shield
            shieldPrefab.SetActive(true);

            // putting the shield in place
            float spawnX = gameObject.transform.localPosition.x;
            float spawnY = gameObject.transform.localPosition.y;
            float spawnDist = 0.9f;

            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);

            // positioning the shield and angling the shield
            if (Mathf.Abs(xMag) > 0 || Mathf.Abs(yMag) > 0)
            {
                // setting pos
                if (xMag > 0)
                {
                    spawnX = gameObject.transform.localPosition.x + spawnDist;
                }
                else if (xMag < 0)
                {
                    spawnX = gameObject.transform.localPosition.x - spawnDist;
                }

                if (yMag > 0)
                {
                    spawnY = gameObject.transform.localPosition.y + spawnDist;
                }
                else if (yMag < 0)
                {
                    spawnY = gameObject.transform.localPosition.y - spawnDist;
                }

                if (xMag == 0 && yMag == 0)
                {
                    spawnX = gameObject.transform.localPosition.x + spawnDist;
                }

                // seting the angle
                if (xMag > 0)
                {
                    shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                    if (yMag > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                    }
                    else if (yMag < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 315);
                    }

                }
                else if (xMag < 0)
                {
                    shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 180);
                    if (yMag > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 135);
                    }
                    else if (yMag < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 225);
                    }
                }
                else
                {
                    if (yMag > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else if (yMag < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 270);
                    }
                    else
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }

            }
            else if (Mathf.Abs(xMag2) > 0 || Mathf.Abs(yMag2) > 0)
            {
                // seting the pos
                if (xMag2 > 0)
                {
                    spawnX = gameObject.transform.localPosition.x + spawnDist;
                }
                else if (xMag2 < 0)
                {
                    spawnX = gameObject.transform.localPosition.x - spawnDist;
                }

                if (yMag2 > 0)
                {
                    spawnY = gameObject.transform.localPosition.y + spawnDist;
                }
                else if (yMag2 < 0)
                {
                    spawnY = gameObject.transform.localPosition.y - spawnDist;
                }

                if (xMag2 == 0 && yMag2 == 0)
                {
                    spawnX = gameObject.transform.localPosition.x + spawnDist;
                }

                // seting the angle
                if (xMag2 > 0)
                {
                    shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                    if (yMag2 > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                    }
                    else if (yMag2 < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 315);
                    }

                }
                else if (xMag2 < 0)
                {
                    shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 180);
                    if (yMag2 > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 135);
                    }
                    else if (yMag2 < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 225);
                    }
                }
                else
                {
                    if (yMag2 > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else if (yMag2 < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 270);
                    }
                    else
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
            else
            {
                if(rightFacing)
                {
                    spawnX = gameObject.transform.localPosition.x + spawnDist;
                    spawnY = gameObject.transform.localPosition.y;
                    shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    spawnX = gameObject.transform.localPosition.x - spawnDist;
                    spawnY = gameObject.transform.localPosition.y;
                    shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 180);
                }
            }
            shieldPrefab.transform.position = new Vector2(spawnX, spawnY);
            
        }
        else
        {
            shieldPrefab.SetActive(false);
        }

        if(shieldHealth < 1)
        {
            StartCoroutine(AbleToShieldAgain());
        }
    }

    void Dash()
    {
        if (dashAmount > 0 && Input.GetAxis(playerShield) > 0)
        {
            speedMultiplier = 4f;
            dashAmount--;
			anim.SetBool ("Dashing", true);

            canBeHit = false;
            StartCoroutine(NormalSpeed());
        }

    }

    IEnumerator NormalSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        speedMultiplier = 1.0f;
        canBeHit = true;
        canCheckDash = true;
        StartCoroutine(AbleToDashAgain());

    }

    IEnumerator AbleToShieldAgain()
    {
        yield return new WaitForSeconds(2.0f);
        ableToShield = true;
        shieldHealth = 1;
    }

    IEnumerator AbleToShootAgain()
    {
        yield return new WaitForSeconds(0.3f);
        ableToThrow = true;
    }

    IEnumerator AbleToPickUpAgain()
    {
        yield return new WaitForSeconds(0.3f);
        ableToPickUp = true;
    }

    IEnumerator AbleToDashAgain()
    {
        yield return new WaitForSeconds(1.0f);
        dashAmount = 1;
    }

    void OnCollisionEnter2D(Collision2D col)
	{
        if(col.otherCollider.transform.tag == "Shield")
        {
            if (col.transform.tag == "Ball" && col.transform.GetComponent<BallScript>().possession != 0 && col.transform.GetComponent<BallScript>().possession != team)
            {
                col.gameObject.GetComponent<BallScript>().ChangeTeam();
                col.gameObject.GetComponent<Rigidbody2D>().velocity *= -1;
                shieldHealth--;

                Debug.Log("You got blocked bitch");
            }
        }
        else
        {
            if (col.transform.tag == "Ball" && col.transform.GetComponent<BallScript>().possession != 0 && col.transform.GetComponent<BallScript>().possession != team)
            {
                if(canBeHit)
                {
                    if (numBalls > 0)
                    {
                        GameObject ball = Instantiate(ballPrefab, gameObject.transform);
                        ball.name = ballSavedName;
                        numBalls--;
                    }

                    isOut = true;
                    col.gameObject.GetComponent<BallScript>().SendKillInfo(gameObject);
					Vector3 deathPoint = transform.position;
					Instantiate(explosionPrefab, deathPoint, Quaternion.identity);

                }
                else
                {
                    col.gameObject.GetComponent<BallScript>().ChangeTeam();
                    col.gameObject.GetComponent<Rigidbody2D>().velocity *= -1;
                }

            }
        }

	}

}
