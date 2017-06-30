using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeDisplay : MonoBehaviour {

	private Text timeDisplay;
	private float time;
	private string minutes;
	private string seconds;
	private string millis;
	private bool runTimer;
	// Use this for initialization
	void Awake () {
		time = 0f;
		runTimer = true;
		timeDisplay = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (runTimer) {
			time = time + Time.deltaTime;
			minutes = Mathf.Floor (time / 60).ToString ("00");
			seconds = Mathf.Floor (time % 60).ToString ("00");
			millis = ((time * 100f) % 100f).ToString ("00");

			timeDisplay.text = minutes + ":" + seconds + ":" + millis;
		}
	}

	public void Stop (){
		runTimer = false;
	}
}
