using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovementScript : enemyBaseScript {

	int randomDir;
	public cubeNodeScript startCube;
    public GameObject CoilySnake;
    [SerializeField] public AudioSource jumpSound;
    [SerializeField] public Animator dropAnim;

    private float moveTime;

	void Start () {
        dropAnim.enabled = false;
        moveTime = Random.Range(1.0f, 1.25f);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        InvokeRepeating ("Move", 1.5f, moveTime);
	}
	
	// Update is called once per frame
	void Update () {
       
	}

	//public override void StartBehaviour(ref GameObject cube){
	//	base.StartBehaviour (ref cube);
	//}

	void Move(){
        if (!QbertController.isHit && !GameManagerScript._gameOver && !GameManagerScript._victory && !QbertController.greenBallPower)
        {
            randomDir = Random.Range(0, 2);
            moveTime = Random.Range(1.0f, 1.25f);
            if (randomDir == 0)
            {
                if (curCube.frontLeft == null && curCube.frontRight == null)
                {
                    if (gameObject.tag == "Coily")
                    {
                        transform.SetParent(curCube.transform);
                        Destroy(gameObject);
                        GameObject _CoilySnake = Instantiate(CoilySnake, new Vector3(curCube.transform.position.x, curCube.transform.position.y + 0.5f, curCube.transform.position.z), Quaternion.identity);
                        _CoilySnake.transform.parent = curCube.transform;
                    }
                    else if (gameObject.tag == "RedBall" || gameObject.tag == "GreenBall")
                    {
                        gameObject.transform.position += new Vector3(-1.0f, 0.0f, -2.0f);
                        gameObject.GetComponent<Rigidbody>().useGravity = true;
                        Destroy(gameObject, 1.0f);
                    }
                }
                else if (curCube.frontLeft == null)
                {
                    
                    curCube = curCube.frontRight;
                    transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 1.5f, curCube.transform.position.z);
                    transform.SetParent(curCube.frontRight.transform);
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                  //  dropAnim.SetBool("playBallDrop", true);
                   
                }
                else 
                {
                    
                    curCube = curCube.frontLeft;
                    transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 1.5f, curCube.transform.position.z);
                    //transform.SetParent(curCube.frontLeft.transform);
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                   // dropAnim.SetBool("playBallDrop", true);

                }
            }
            else if (randomDir == 1)
            {
                if (curCube.frontLeft == null && curCube.frontRight == null)
                {
                    if (gameObject.tag == "Coily")
                    {
                        transform.SetParent(curCube.transform);
                        Destroy(gameObject);
                        GameObject _CoilySnake = Instantiate(CoilySnake, new Vector3(curCube.transform.position.x, curCube.transform.position.y + 0.5f, curCube.transform.position.z), Quaternion.identity);
                        _CoilySnake.transform.parent = curCube.transform;

                    }
                    else if (gameObject.tag == "RedBall" || gameObject.tag == "GreenBall")
                    {
                        gameObject.transform.position += new Vector3(1.0f, 0.0f, -2.0f);
                        gameObject.GetComponent<Rigidbody>().useGravity = true;
                        Destroy(gameObject, 1.0f);
                    }
                }
                else if (curCube.frontRight == null)
                {

                    curCube = curCube.frontLeft;
                    transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 1.5f, curCube.transform.position.z);
                    transform.SetParent(curCube.frontLeft.transform);
                    gameObject.GetComponent<Rigidbody>().useGravity = true;

                }
                else 
                {
                    
                    curCube = curCube.frontRight;
                    transform.position = new Vector3(curCube.transform.position.x, curCube.transform.position.y + 1.5f, curCube.transform.position.z);
                   // transform.SetParent(curCube.frontRight.transform);
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                    
                }
            }
        }
	}

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.tag == "Cube") {
            jumpSound.Play();
        }
    }
}
