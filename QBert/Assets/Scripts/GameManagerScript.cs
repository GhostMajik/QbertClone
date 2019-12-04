using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public static int _score;
    public static int _lives;
    public static int _tilesToConvert = 28;
    public static bool _gameOver = false;
    public static bool _victory = false;
    public static bool canSpawn = true;

    [SerializeField] public AudioSource gameMusic;
    [SerializeField] public AudioSource victoryMusic;
    [SerializeField] public AudioSource gameOverMusic;

    public Transform gameOverPanel;
    public Text hiScoreText;
    public Text gameOverText;

    public GameObject[] elevators;

    private bool addedBonus = false;
    private bool isPlayingGMMusic = false;
    // Use this for initialization
    void Start () {
        _score = 0;
        _lives = 3;
        _tilesToConvert = 28;
        _gameOver = false;
        _victory = false;
        hiScoreText.enabled = false;
        addedBonus = false;
        isPlayingGMMusic = false;
       // elevators = GameObject.FindGameObjectsWithTag("Elevator");

    }
	
	// Update is called once per frame
	void Update () {
        if (canSpawn == false) {
            Invoke("SpawnDelay", 2.0f);
        }


        if (_gameOver == true && _lives == 0)
        {
            _victory = false;
            gameOverPanel.gameObject.SetActive(true);
            gameMusic.Pause();
            PlayGameOverMusic();

            //change static 500 to be highscores[9]
            if (_score > PlayerPrefs.GetInt("score10"))
            {
                Cursor.visible = true;
                hiScoreText.enabled = true;
                PlayerPrefs.SetString("score2", "AAA " + _score);
                Debug.Log(PlayerPrefs.GetString("score9"));
                Invoke("ChangeScene", 2.5f);
                //if new high score, change to menu scene and activate leaderboard panel, then ask for player initial input **3 letters max**
                //inserstion sort the high score into high scores array
            }
            else
            {
                hiScoreText.enabled = false;
                Invoke("ChangeScene", 2.5f);
            }
        }
       else if (_victory && _lives >= 1)
        {
            _gameOver = false;
            gameMusic.Pause();
            AddBonusScore();

            //maybe use mass if statement to check each high score 1-10 and if > hiscore, update the value
            //store data from input field, assign it to AAA
            if (_score > PlayerPrefs.GetInt("score9"))
            {
                Cursor.visible = true;
                gameOverPanel.gameObject.SetActive(true);
                gameOverText.text = "Victory";
                hiScoreText.enabled = true;
                PlayerPrefs.SetString("score2", "AAA " + _score);
                Debug.Log(PlayerPrefs.GetString("score9"));
               
            }
            //replace this with input field onEditEnd event
            Invoke("ChangeScene", 3.5f);
        }


	}

    void SpawnDelay() {
        canSpawn = true;
    }

    void PlayGameOverMusic()
    {
        if (!isPlayingGMMusic)
        {
            gameOverMusic.Play();
            isPlayingGMMusic = true;
        }
    }

    void AddBonusScore()
    {
        elevators = GameObject.FindGameObjectsWithTag("Elevator");
        if (!addedBonus)
        {
            victoryMusic.Play();
            _score += 1000;
            foreach (GameObject elevator in elevators)
            {
                _score += 100;
            }
            addedBonus = true;
        }
    }

    void ChangeScene() {
        SceneManager.LoadScene("MenuScene");
    }
}
