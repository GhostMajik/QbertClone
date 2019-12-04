using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	public Transform canvas;
	public static bool isPaused = false;

	void Awake(){
		Time.timeScale = 1;
		Cursor.visible = false;
		AudioListener.pause = false;
	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	public void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Cursor.visible = true;
			Time.timeScale = 0;

			//cheap way to pause all audio
			AudioListener.pause = true;
			isPaused = true;
		} else {
			canvas.gameObject.SetActive (false);
			Cursor.visible = false;
			Time.timeScale = 1;
			AudioListener.pause = false;
			isPaused = false;
		}
	}

	public void Resume(){
		Time.timeScale = 1;
		canvas.gameObject.SetActive (false);
		Cursor.visible = false;
		AudioListener.pause = false;
		isPaused = false;
	}
}
