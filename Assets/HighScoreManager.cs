using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

	[SerializeField]
	private Text firstText;
	[SerializeField]
	private Text secondText;
	[SerializeField]
	private Text thirdText;
	[SerializeField]
	private Text fourthText;
	[SerializeField]
	private Text fifthText;

	// Use this for initialization
	void Start () {
		string first = PlayerPrefs.GetString ("HIScore1");
		string second = PlayerPrefs.GetString ("HIScore2");
		string third = PlayerPrefs.GetString ("HIScore3");
		string fourth = PlayerPrefs.GetString ("HIScore4");
		string fifth = PlayerPrefs.GetString ("HIScore5");
		firstText.text = first;
		secondText.text = second;
		thirdText.text = third;
		fourthText.text = fourth;
		fifthText.text = fifth;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
