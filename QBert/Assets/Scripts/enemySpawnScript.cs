using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnScript : MonoBehaviour {

	[SerializeField] GameObject coilyBall;
	[SerializeField] GameObject redBall;
	[SerializeField] GameObject greenBall;
   

	[SerializeField] GameObject SpawnBox1;
	[SerializeField] GameObject SpawnBox2;

    cubeNodeScript Box1;
    cubeNodeScript Box2;

    private bool Paused = false;
	private int boxToSpawn;

	public int enemySpawnNum;

	//raise spawn delay to infinite when need to stop spawner for X seconds, then reset to normal
	public int spawnDelay;
	
	void Start () {     
        Random.InitState (System.DateTime.Now.Millisecond);
		spawnDelay =  Random.Range (3, 7);
		InvokeRepeating ("Method", 1.0f, spawnDelay);
	}

	// Update is called once per frame
	void Update () {
		//maybe call spawn delay in update to cause invoke to be unable to resolve for greenball power
	}

    public static void WipeScreen() {
        GameObject[] redBalls = GameObject.FindGameObjectsWithTag("RedBall");
        foreach (GameObject redBall in redBalls) {
            GameObject.Destroy(redBall);
        }
    }


    //need to random spawn delay for each spawn
    void Method(){
		if (!QbertController.isHit && !GameManagerScript._gameOver && !GameManagerScript._victory && !QbertController.greenBallPower && GameManagerScript.canSpawn) {
			spawnDelay = Random.Range (3, 7);
            
            //range from 0-10 for 100% total value
			enemySpawnNum = Random.Range (0, 11);

            //range from 0-11 to provide 50/50 spawn chance
			boxToSpawn = Random.Range (0, 12);
			

			//if no coily in the level, spawn a coily
			if (GameObject.FindGameObjectWithTag ("Coily") == null) {
				if(boxToSpawn <= 5){
					GameObject Coily = Instantiate (coilyBall, new Vector3(SpawnBox1.transform.position.x,SpawnBox1.transform.position.y + 4.75f, SpawnBox1.transform.position.z), Quaternion.identity);
					Coily.GetComponent<ballMovementScript> ().StartBehaviour (ref SpawnBox1);
				}
				else if(boxToSpawn >= 6){
					GameObject Coily = Instantiate (coilyBall, new Vector3(SpawnBox2.transform.position.x,SpawnBox2.transform.position.y + 4.75f, SpawnBox2.transform.position.z), Quaternion.identity);
					Coily.GetComponent<ballMovementScript> ().StartBehaviour (ref SpawnBox2);
				}

			}

           // if (roll >= 9) then spawn green ball, 20 % chance
			else if (enemySpawnNum >= 9)
            {
                if (boxToSpawn <= 5)
                {
                    GameObject GreenBall = Instantiate(greenBall, new Vector3(SpawnBox1.transform.position.x, SpawnBox1.transform.position.y + 4.75f, SpawnBox1.transform.position.z), Quaternion.identity);
                    GreenBall.GetComponent<ballMovementScript>().StartBehaviour(ref SpawnBox1);
                }
                else if (boxToSpawn >= 6)
                {
                   GameObject GreenBall = Instantiate(greenBall, new Vector3(SpawnBox2.transform.position.x, SpawnBox2.transform.position.y + 4.75f, SpawnBox2.transform.position.z), Quaternion.identity);
                   GreenBall.GetComponent<ballMovementScript>().StartBehaviour(ref SpawnBox2);
                }
            }

            ////if roll <9, spawn red ball, 80% chance
            else if (enemySpawnNum <= 8)
            {
                if (boxToSpawn <= 5)
                {
                   GameObject RedBall = Instantiate(redBall, new Vector3(SpawnBox1.transform.position.x, SpawnBox1.transform.position.y + 4.75f, SpawnBox1.transform.position.z), Quaternion.identity);
                    RedBall.GetComponent<ballMovementScript>().StartBehaviour(ref SpawnBox1);
                }
                else if (boxToSpawn >= 6)
                {
                   GameObject RedBall = Instantiate(redBall, new Vector3(SpawnBox2.transform.position.x, SpawnBox2.transform.position.y + 4.75f, SpawnBox2.transform.position.z), Quaternion.identity);
                    RedBall.GetComponent<ballMovementScript>().StartBehaviour(ref SpawnBox2);
                }
            }
        }
	}
}
