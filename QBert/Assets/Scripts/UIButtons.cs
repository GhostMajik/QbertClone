using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour {

	[SerializeField] public string sceneToLoad;
	[SerializeField] public GameObject menuPanel;
	[SerializeField] public GameObject leaderboardPanel;

	void Awake(){
		menuPanel.SetActive (true);
		leaderboardPanel.SetActive (false);
	}

	public void loadPlayScene(){
		SceneManager.LoadScene (sceneToLoad);
		Debug.Log ("Loading");
	}

	public void loadLeaderboard(){
		Debug.Log ("LeaderBoard");
		menuPanel.SetActive (false);
		leaderboardPanel.SetActive (true);
	}

	public void quitGame(){
		Debug.Log ("Shutting Down Game");
		Application.Quit ();
	}

	public void goBack(){
		Debug.Log ("BACK");
		leaderboardPanel.SetActive (false);
		menuPanel.SetActive (true);
	}
}
