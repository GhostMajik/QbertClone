using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UILivesScript : MonoBehaviour {

    public Image livesImage1;
    public Image livesImage2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManagerScript._lives == 2)
        {
            livesImage1.enabled = false;
        }
        else if (GameManagerScript._lives == 1) {
            livesImage1.enabled = false;
            livesImage2.enabled = false;
        }
	}
}
