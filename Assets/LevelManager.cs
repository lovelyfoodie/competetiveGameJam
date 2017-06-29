using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public MusicLooper music;

    private void Start()
    {
		//Check for high scores, and set to default if doesnt exist
		string hiScore1=PlayerPrefs.GetString("HIScore1","---");
		string initials1 = PlayerPrefs.GetString ("Initials1", "---");
		string hiScore2=PlayerPrefs.GetString("HIScore2","---");
		string initials2 = PlayerPrefs.GetString ("Initials2", "---");
		string hiScore3=PlayerPrefs.GetString("HIScore3","---");
		string initials3 = PlayerPrefs.GetString ("Initials3", "---");
		string hiScore4=PlayerPrefs.GetString("HIScore4","---");
		string initials4 = PlayerPrefs.GetString ("Initials4", "---");
		string hiScore5=PlayerPrefs.GetString("HIScore5","---");
		string initials5 = PlayerPrefs.GetString ("Initials5", "---");
		//Populate hiscores with defaults if empty
		if (hiScore1.Equals ("---")) {
			PlayerPrefs.SetString ("HIScore1", "01:00:00");
			PlayerPrefs.SetString ("Initials1", "AAA");
			PlayerPrefs.SetString ("HIScore2", "00:40:00");
			PlayerPrefs.SetString ("Initials2", "AAA");
			PlayerPrefs.SetString ("HIScore3", "00:25:00");
			PlayerPrefs.SetString ("Initials3", "AAA");
			PlayerPrefs.SetString ("HIScore4", "00:15:00");
			PlayerPrefs.SetString ("Initials4", "AAA");
			PlayerPrefs.SetString ("HIScore5", "00:05:00");
			PlayerPrefs.SetString ("Initials5", "AAA");
		}

        if (music == null)
        {
            //Note: suuuuuper breakable, but I'm tired and this will work well enough
            GameObject go = GameObject.Find("Music");
            if (go)
                music = go.GetComponent<MusicLooper>();
        }
    }

	public void GoToLevel(string levelName){
        if (music != null)
        {
            music.PlayGameplayMusic();
            music.EnableBirdSounds(true);
        }

        SceneManager.LoadScene (levelName);
	}

	public void Quit(){
		Application.Quit ();
	}
}
