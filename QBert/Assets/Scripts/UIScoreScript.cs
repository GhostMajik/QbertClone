using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScoreScript : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = "0000";
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = GameManagerScript._score.ToString();
	}
}
