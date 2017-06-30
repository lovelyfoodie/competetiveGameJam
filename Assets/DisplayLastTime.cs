using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLastTime : MonoBehaviour {

	Text displayText;

	// Use this for initialization
	void Start () {
		displayText = GetComponent<Text> ();
        if (!displayText)
            return;

		string text = PlayerPrefs.GetString ("HIScore1");
		if (text != null) {
			displayText.text = text;
		} else {
			displayText.text = "None";
		}
	}

}
