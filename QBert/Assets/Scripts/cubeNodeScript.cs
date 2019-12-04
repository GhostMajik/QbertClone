using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeNodeScript : MonoBehaviour {

	public cubeNodeScript frontLeft;
	public cubeNodeScript frontRight;
	public cubeNodeScript backLeft;
	public cubeNodeScript backRight;
	private int count;

	MeshRenderer mesh;

	void Start () {
		mesh = gameObject.GetComponent<MeshRenderer> ();
		count = 0;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && count == 0) {
			mesh.material.color = Color.yellow;
			count++;
            GameManagerScript._score += 25;
            GameManagerScript._tilesToConvert--;
        }
	}

    //kinda shitty but works for color change
    void FlashColors() { 
        InvokeRepeating("ChangeColors", 0.0f, 1.0f);       
    }
    void ChangeColors() {
        mesh.material.color = Color.blue;
        Invoke("Delay", 0.25f);
    }

    void Delay() {
        mesh.material.color = Color.yellow;
    }

    void Update() {
        if (GameManagerScript._tilesToConvert <= 0 && GameManagerScript._lives >0)
        {
            GameManagerScript._victory= true;
            FlashColors();
        }

    }
}
	