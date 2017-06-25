﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFinalTime : MonoBehaviour {

	Text displayText;

	// Use this for initialization
	void Start () {
		displayText = GetComponent<Text> ();
		string text = PlayerPrefs.GetString ("FinalScore");
		if (text != null) {
			displayText.text = text;
		} else {
			displayText.text = "0:00";
		}
	}

}
