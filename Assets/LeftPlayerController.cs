using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class LeftPlayerController : MonoBehaviour {

	public GameObject item;

	void Update() {
		if (Input.GetKeyDown (KeyCode.A)) { //left key press
			Instantiate (item, gameObject.transform.position, gameObject.transform.rotation);
		}
			

	}
}
