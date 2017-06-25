using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public MusicLooper music;

    private void Start()
    {
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
