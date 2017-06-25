using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

	private TimeDisplay timer;
	// Use this for initialization
	void Start () {
		timer = FindObjectOfType<TimeDisplay> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.transform.name);
		if (other.transform.name.Equals ("PlayerPlatform")) {
			timer.Stop ();
		}
	}
}
