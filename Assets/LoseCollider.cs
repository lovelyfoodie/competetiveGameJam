using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseCollider : MonoBehaviour {

	private TimeDisplay timer;

	void Start () {
		timer = FindObjectOfType<TimeDisplay> ();
	}	

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log (other.transform.name);
		if (other.transform.name.Equals ("CenterOfGravity")) {
			timer.Stop ();
			//TODO check if top score against PlayerPrefs
			string playerScore=timer.GetComponent<Text>().text;
			float numericScore = CalculateScore(playerScore);
			PlayerPrefs.SetString("FinalScore",playerScore);
			//Find other scores on leaderboard
			float hiScore1 = CalculateScore(PlayerPrefs.GetString("HIScore1"));
			float hiScore2 = CalculateScore(PlayerPrefs.GetString("HIScore2"));
			float hiScore3 = CalculateScore(PlayerPrefs.GetString("HIScore3"));
			float hiScore4 = CalculateScore(PlayerPrefs.GetString("HIScore4"));
			float hiScore5 = CalculateScore(PlayerPrefs.GetString("HIScore5"));
			if (numericScore <= hiScore5) {
				//Not a high score
			}
			else{
				//Find rank
				if (numericScore > hiScore1) {
					//Rank 1
					Debug.Log("First Place");
					SetHighScore(1,playerScore);
				} else if (numericScore > hiScore2) {
					//Rank 2
					Debug.Log("Second Place");
					SetHighScore(2,playerScore);
				} else if (numericScore > hiScore3) {
					//Rank 3
					Debug.Log("Third Place");
					SetHighScore(3,playerScore);
				} else if (numericScore > hiScore4) {
					//Rank 4
					Debug.Log("Fourth Place");
					SetHighScore(4,playerScore);
				} else {
					//Rank 5
					Debug.Log("Fifth Place");
					SetHighScore(5,playerScore);
				}
			}
            //Switch to end scene
            StartCoroutine(StartSceneLoad(2f));
		}
	}

    IEnumerator StartSceneLoad(float delay)
    {
        UpdateMusic();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LoseScreen");
    }

	void SetHighScore(int rank, string score){
		if (rank == 1) {
			PlayerPrefs.SetString ("HIScore5", PlayerPrefs.GetString ("HIScore4"));
			PlayerPrefs.SetString ("Initials5", PlayerPrefs.GetString ("Initials4"));
			PlayerPrefs.SetString ("HIScore4", PlayerPrefs.GetString ("HIScore3"));
			PlayerPrefs.SetString ("Initials4", PlayerPrefs.GetString ("Initials3"));
			PlayerPrefs.SetString ("HIScore3", PlayerPrefs.GetString ("HIScore2"));
			PlayerPrefs.SetString ("Initials3", PlayerPrefs.GetString ("Initials2"));
			PlayerPrefs.SetString ("HIScore2", PlayerPrefs.GetString ("HIScore1"));
			PlayerPrefs.SetString ("Initials2", PlayerPrefs.GetString ("Initials1"));
			PlayerPrefs.SetString ("HIScore1", score);
			//PlayerPrefs.SetString ("Initials1", GetInitials ());
		} else if (rank == 2) {
			PlayerPrefs.SetString ("HIScore5", PlayerPrefs.GetString ("HIScore4"));
			PlayerPrefs.SetString ("Initials5", PlayerPrefs.GetString ("Initials4"));
			PlayerPrefs.SetString ("HIScore4", PlayerPrefs.GetString ("HIScore3"));
			PlayerPrefs.SetString ("Initials4", PlayerPrefs.GetString ("Initials3"));
			PlayerPrefs.SetString ("HIScore3", PlayerPrefs.GetString ("HIScore2"));
			PlayerPrefs.SetString ("Initials3", PlayerPrefs.GetString ("Initials2"));
			PlayerPrefs.SetString ("HIScore2", score);
			//PlayerPrefs.SetString ("Initials2", GetInitials ());
		} else if (rank == 3) {
			PlayerPrefs.SetString ("HIScore5", PlayerPrefs.GetString ("HIScore4"));
			PlayerPrefs.SetString ("Initials5", PlayerPrefs.GetString ("Initials4"));
			PlayerPrefs.SetString ("HIScore4", PlayerPrefs.GetString ("HIScore3"));
			PlayerPrefs.SetString ("Initials4", PlayerPrefs.GetString ("Initials3"));
			PlayerPrefs.SetString ("HIScore3", score);
			//PlayerPrefs.SetString ("Initials3", GetInitials ());
		} else if (rank == 4) {
			PlayerPrefs.SetString ("HIScore5", PlayerPrefs.GetString ("HIScore4"));
			PlayerPrefs.SetString ("Initials5", PlayerPrefs.GetString ("Initials4"));
			PlayerPrefs.SetString ("HIScore4", score);
			//PlayerPrefs.SetString ("Initials4", GetInitials ());
		} else {
			PlayerPrefs.SetString ("HIScore5", score);
			//PlayerPrefs.SetString ("Initials5", GetInitials ());
		}
	}

	float CalculateScore (string time){
		string[]timeSplit = time.Split(':');
		float numericScore = 0f;
		try{
			string minutes = timeSplit [0];
			numericScore += 60f*Int32.Parse(minutes);
			string seconds = timeSplit [1];
			numericScore += Int32.Parse(seconds) * 1f;
			string millis = timeSplit [2];
			numericScore += Int32.Parse(millis) / 100f;
			Debug.Log(numericScore);
		}
		catch(Exception e){
			Debug.Log (e.Message);
		}
		return numericScore;
	}

    void UpdateMusic()
    {
        GameObject go = GameObject.Find("Music");
        if (!go)
            return;

        MusicLooper music = go.GetComponent<MusicLooper>(); //Bad design
        if (music != null)
        {
            music.PlayGameoverMusic();
            music.EnableBirdSounds(false);
        }
    }
}
