using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour {

    public Text hiScore1;
    public Text hiScore2;
    public Text hiScore3;
    public Text hiScore4;
    public Text hiScore5;
    public Text hiScore6;
    public Text hiScore7;
    public Text hiScore8;
    public Text hiScore9;
    public Text hiScore10;

    void Start () {
        hiScore1.text = " 1) " + PlayerPrefs.GetString("score1");
        hiScore2.text = " 2) " + PlayerPrefs.GetString("score2");
        hiScore3.text = " 3) " + PlayerPrefs.GetString("score3");
        hiScore4.text = " 4) " + PlayerPrefs.GetString("score4");
        hiScore5.text = " 5) " + PlayerPrefs.GetString("score5");
        hiScore6.text = " 6) " + PlayerPrefs.GetString("score6");
        hiScore7.text = " 7) " + PlayerPrefs.GetString("score7");
        hiScore8.text = " 8) " + PlayerPrefs.GetString("score8");
        hiScore9.text = " 9) " + PlayerPrefs.GetString("score9");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
