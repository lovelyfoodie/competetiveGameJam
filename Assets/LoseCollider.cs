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
			PlayerPrefs.SetString("FinalScore",timer.GetComponent<Text>().text);

            //Switch to end scene
            StartCoroutine(StartSceneLoad(2f));
		}
	}

    IEnumerator StartSceneLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LoseScreen");
    }
}
