using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyMovement : enemyBaseScript{

    public GameObject startingCube;
    public cubeNodeScript moveToCube;
    public cubeNodeScript prevCube;
    public cubeNodeScript leftElevatorGap;
    public cubeNodeScript rightElevatorGap;
    public GameObject Coily;
    [SerializeField] public AudioSource coilyJumpSFX;

    QbertController qBert;

    private int randomDir;
    private float moveTime;
    private Vector3 distToQbert;
    private bool coilyDead;

    public Vector3 CubePosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    // Use this for initialization
    void Start()
    {
       curCube = transform.parent.gameObject.GetComponent<cubeNodeScript>();
       qBert = GameObject.FindGameObjectWithTag("Player").GetComponent<QbertController>();
       moveToCube = qBert.CurrentCube;
       coilyDead = false;
       randomDir = Random.Range(0, 2);
       InvokeRepeating("Seek", 0.35f, 0.75f);
    }

    public override void StartBehaviour(ref GameObject cube)
    {
        base.StartBehaviour(ref cube);
    }

    // Update is called once per frame
    void Update()
    {
        if (coilyDead)
        {
            Fall();
        }
    }

    void Seek()
    {
        randomDir = Random.Range(0, 2);
        prevCube = moveToCube;
        moveToCube = qBert.CurrentCube;

        LookAtQbert();

        distToQbert = moveToCube.transform.position - transform.position;
        //Debug.Log(curCube);
        if (!QbertController.isHit && !GameManagerScript._gameOver && !GameManagerScript._victory && !QbertController.greenBallPower)
        {
            if (curCube == null)
                moveToCube = qBert.CurrentCube;
            if (moveToCube.gameObject.tag == "rightElevator")
            {
               moveToCube = rightElevatorGap;
                Debug.Log(moveToCube);
            }
            
            if (moveToCube.gameObject.tag == "leftElevator")
            {
                moveToCube = leftElevatorGap;
            }

            //go left and up
            if (moveToCube.transform.position.x < curCube.transform.position.x && moveToCube.transform.position.y > curCube.transform.position.y)
            {
                curCube = curCube.backLeft;
                MoveCoily();
            }
            //go right and up
            else if (moveToCube.transform.position.x > curCube.transform.position.x && moveToCube.transform.position.y > curCube.transform.position.y)
            {
                curCube = curCube.backRight;
                MoveCoily();
            }
            //choose left or right and up
            else if (moveToCube.transform.position.x == curCube.transform.position.x && moveToCube.transform.position.y > curCube.transform.position.y)
            {
                if (randomDir == 0 && curCube.backLeft != null)
                {
                    curCube = curCube.backLeft;
                    MoveCoily();
                }
                else if (randomDir == 1 && curCube.backRight != null)
                {
                    curCube = curCube.backRight;
                    MoveCoily();
                }
            }

            //go down pyramid
            //down and left
            else if (moveToCube.transform.position.x < curCube.transform.position.x && moveToCube.transform.position.y < curCube.transform.position.y)
            {
                curCube = curCube.frontLeft;
                MoveCoily();
            }
            //dowwn and right
            else if (moveToCube.transform.position.x > curCube.transform.position.x && moveToCube.transform.position.y < curCube.transform.position.y)
            {
                curCube = curCube.frontRight;
                MoveCoily();
            }
            //down and choose left or right
            else if (moveToCube.transform.position.x == curCube.transform.position.x && moveToCube.transform.position.y < curCube.transform.position.y)
            {
                if (randomDir == 0)
                {
                    curCube = curCube.frontLeft;
                    MoveCoily();
                }
                else if (randomDir == 1)
                {
                    curCube = curCube.frontLeft;
                    MoveCoily();
                }
            }
            //resolve vertical conflicts
            //if moving left and height is equal
            else if (moveToCube.transform.position.x < curCube.transform.position.x && moveToCube.transform.position.y == curCube.transform.position.y)
            {
                if (randomDir == 0 && curCube.frontLeft != null)
                {
                    curCube = curCube.frontLeft;
                    MoveCoily();
                }
                else if (randomDir == 1 && curCube.backLeft != null)
                {
                    curCube = curCube.backLeft;
                    MoveCoily();
                }
            }
            //if moving right and height is equal
            else if (moveToCube.transform.position.x > curCube.transform.position.x && moveToCube.transform.position.y == curCube.transform.position.y)
            {
                if (randomDir == 0 && curCube.frontRight != null)
                {
                    curCube = curCube.frontRight;
                    MoveCoily();
                }
                else if (randomDir == 1 && curCube.backRight != null)
                {
                    curCube = curCube.backRight;
                    MoveCoily();
                }
            }
        }
    }

    void LookAtQbert() {
        Vector3 lookPos = qBert.CurrentCube.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 1000);
       
    }

    void MoveCoily()
    {  
       // Debug.Log(moveToCube);
        if (curCube == null)
            return;

        if (curCube.gameObject.tag == "rightElevator" && moveToCube == rightElevatorGap)
        {
            curCube = rightElevatorGap;
            transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 0.5f, curCube.transform.position.z);
            coilyDead = true;
            Debug.Log("DEAD");
            killCoily();
        }
        if (curCube.gameObject.tag == "leftElevator" && moveToCube == leftElevatorGap)
        {
            curCube = leftElevatorGap;
            transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 0.5f, curCube.transform.position.z);
            coilyDead = true;
            Debug.Log("DEAD");
            killCoily();
        }
        else
        {
            transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 0.5f, curCube.transform.position.z);
        }

        coilyJumpSFX.Play();
    }

    void killCoily() {
        if (coilyDead) {
            Fall();
            Destroy(gameObject,1.0f);
            GameManagerScript._score += 500;
            enemySpawnScript.WipeScreen();
            GameManagerScript.canSpawn = false;
        }
    }
    void Fall() {
        gameObject.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
    }
}
