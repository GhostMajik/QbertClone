using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAudio : MonoBehaviour {

	//doesnt play after loading scene from pause menu
	void Awake(){
        Cursor.visible = true;
		AudioListener.pause = false;
		AudioSource menuMusic = GetComponent<AudioSource> ();
		menuMusic.Play ();
		Debug.Log ("PLAYING MUSIC");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
