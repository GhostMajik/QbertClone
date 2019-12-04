using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QbertController : MonoBehaviour {

	cubeNodeScript currentCubePos;
    cubeNodeScript startCubePos;
    private BoxCollider qBertBoxColl;
    private float yOffset;
    private float moveTimer;
    private bool canMove;
    private bool isFalling;

    [SerializeField] public AudioSource greenBallSound;
    [SerializeField] public AudioSource qbertJumpSound;
    [SerializeField] public AudioSource qbertHitSound;
    [SerializeField] public SpriteRenderer curseImg;
    
    public static bool isHit = false;
    public static bool greenBallPower = false;

	public cubeNodeScript CurrentCube{
		get{ return currentCubePos;}
		set{ currentCubePos = value;}
	}

	public Vector3 cubePosition{
		get{ return transform.position;}
		set{ transform.position = value;}
	}

    public cubeNodeScript spawnCube;

	void Start () {
		currentCubePos = transform.GetComponentInParent<cubeNodeScript> ();
        startCubePos = currentCubePos;
        qBertBoxColl = gameObject.GetComponent<BoxCollider>();
        qBertBoxColl.enabled = false;
        isHit = false;
        isFalling = false;
        moveTimer = 0;
        canMove = true;
        greenBallPower = false;
        curseImg.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        InputCheck ();
        Fall();
	}

    //local rotation causing a bug with elevators, need to move to anim states likely
    void InputCheck()
    {
        if (!isHit && !GameManagerScript._gameOver && !GameManagerScript._victory)
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7))&& canMove)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, -90.0f, -90.0f);
                if (CurrentCube.backLeft == null)
                {
                    qbertJumpSound.Play();
                    isHit = true;
                    isFalling = true;
                    gameObject.transform.position += new Vector3(-1.0f, 0.0f, 1.0f);
                    Invoke("MoveToTop", 1.0f);
                    Invoke("Respawn", 1.5f);
                }
                else
                {
                    Move(currentCubePos.backLeft);
                    moveTimer = 0;
                }
                qBertBoxColl.enabled = true;
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad1))&& canMove)
            {
                gameObject.transform.localRotation = Quaternion.Euler(-90.0f, -180.0f, 0.0f);
                if (CurrentCube.frontLeft == null)
                {
                    qbertJumpSound.Play();
                    isHit = true;
                    isFalling = true;
                    gameObject.transform.position += new Vector3(-1.0f, 0.0f, -1.0f);
                    Invoke("MoveToTop", 1.0f);
                    Invoke("Respawn", 1.5f);
                }
                else
                {
                    Move(currentCubePos.frontLeft);
                    moveTimer = 0;
                }
                qBertBoxColl.enabled = true;

            }
            else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad9))&& canMove)
            {
                gameObject.transform.localRotation = Quaternion.Euler(90.0f, 0, 0);
                if (CurrentCube.backRight == null)
                {
                    qbertJumpSound.Play();
                    isHit = true;
                    isFalling = true;
                    gameObject.transform.position += new Vector3(1.0f, 0.0f, 1.0f);
                    Invoke("MoveToTop", 1.0f);
                    Invoke("Respawn", 1.5f);
                }
                else
                {
                    Move(currentCubePos.backRight);
                    moveTimer = 0;
                }
                qBertBoxColl.enabled = true;
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad3))&& canMove)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, -270.0f, 90.0f);
                if (CurrentCube.frontRight == null)
                {
                    qbertJumpSound.Play();
                    isHit = true;
                    isFalling = true;
                    gameObject.transform.position += new Vector3(1.0f, 0.0f, -1.0f);
                    Invoke("MoveToTop", 1.0f);
                    Invoke("Respawn", 1.5f);
                }
                else
                {
                    Move(currentCubePos.frontRight);
                    moveTimer = 0;
                }
                qBertBoxColl.enabled = true;
            }
           TimerCheck();
        }
    }

    void TimerCheck() {
        moveTimer += Time.deltaTime;
        if (moveTimer >= 0.35f)
        {
            canMove = true;
        }
        else
            canMove = false;
      
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "GreenBall") {
            GameManagerScript._score += 100;
            Destroy(c.gameObject);
            greenBallPower = true;
            greenBallSound.Play();
            Invoke("GreenBallPowerDelay", 3.0f);
        }
        if (c.gameObject.tag == "RedBall") {
            Destroy(c.gameObject);
            isHit = true;
            gameObject.transform.localRotation = Quaternion.Euler(-90.0f, -180.0f, 0.0f);
            curseImg.enabled = true;
            qbertHitSound.Play();
            Invoke("Respawn", 1.0f);      
        }
        if (c.gameObject.tag == "Coily") {
            Destroy(c.gameObject);
            isHit = true;
            gameObject.transform.localRotation = Quaternion.Euler(-90.0f, -180.0f, 0.0f);
            curseImg.enabled = true;
            qbertHitSound.Play();
            Invoke("Respawn", 1.0f);
        }
    }

	void Move(cubeNodeScript moveToCube){
        //failed to lerp between cubes
      //  float timerStart = Time.time; 
		CurrentCube = moveToCube;
		transform.parent = CurrentCube.transform;
        //float distanceMoved = (Time.time - timerStart) * 1;
       // float fracDistance = distanceMoved / Vector3.Distance(currentCubePos.transform.position, moveToCube.transform.position);
       //  = Vector3.Lerp(currentCubePos.transform.position, moveToCube.transform.position, fracDistance);
      //  Debug.Log(timerStart);
		cubePosition = new Vector3 (CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.5f, CurrentCube.transform.position.z);
        qbertJumpSound.Play();
    }

    void GreenBallPowerDelay() {
        greenBallPower = false;
    }

    void MoveToTop() {
        isFalling = false;
        gameObject.transform.localRotation = Quaternion.Euler(-90.0f, -180.0f, 0.0f);
        Move(startCubePos);
        curseImg.enabled = true;
        qbertHitSound.Play();
    }

    void Fall() {
        if (isFalling) {
            gameObject.transform.position -= new Vector3(0.0f, 0.1f, 0.0f);
        }
    }

    //needs to be updated for when Qbert jumps off
    void Respawn() {
        GameManagerScript._lives--;
        if (GameManagerScript._lives <= 0)
        {
            GameManagerScript._gameOver = true;
        }
        else {
            //play cursing animation, then resume
            isHit = false;
            curseImg.enabled = false;
        }
        
    }
}
