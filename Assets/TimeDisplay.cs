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
	// Use this for initialization
	void Awake () {
		time = 0f;
		timeDisplay = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		time = time + Time.deltaTime;
		minutes = Mathf.Floor(time / 60).ToString("00");
		seconds = (time % 60).ToString("00");
		timeDisplay.text = minutes + ":" + seconds;
	}
}
