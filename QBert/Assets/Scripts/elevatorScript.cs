using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorScript : cubeNodeScript {

	[SerializeField] cubeNodeScript moveToCube;
    AudioSource elevatorAudio;
	Animator elevatorAnim;
	QbertController QbertPlayer;
	private Vector3 originScale;

	void Start () {
		elevatorAnim = gameObject.GetComponent<Animator> ();
        elevatorAudio = GetComponent<AudioSource>();
		elevatorAnim.enabled = false;
		QbertPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<QbertController> ();
		//originScale = QbertPlayer.transform.lossyScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (elevatorAnim.enabled) {
			Invoke ("moveElevator", 1.2f);
		}
	}

	void moveElevator(){
		QbertPlayer.CurrentCube = moveToCube;
		QbertPlayer.transform.SetParent (moveToCube.transform, false);// = moveToCube.transform;
		QbertPlayer.cubePosition = new Vector3 (moveToCube.transform.position.x, moveToCube.transform.position.y + 0.5f, moveToCube.transform.position.z);
		QbertPlayer.transform.localRotation = Quaternion.Euler (-90.0f, -180.0f, 0.0f);
		QbertPlayer.transform.localScale = new Vector3 (0.015f, 0.015f, 0.015f);
		Destroy (gameObject);
	}

	//weird bug caused by attempting to scale to elevator parent, caused by transform.localrotation in QbertMovement
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			QbertPlayer.transform.rotation = transform.rotation;
			QbertPlayer.transform.SetParent (this.gameObject.transform, false);
			//QbertPlayer.transform.localScale = originScale;
			QbertPlayer.transform.position = transform.position;
			elevatorAnim.enabled = true;
            elevatorAudio.Play();
		}
	}
}
