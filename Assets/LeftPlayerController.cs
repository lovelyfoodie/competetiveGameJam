using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class LeftPlayerController : MonoBehaviour {

	public GameObject item;

	private const float ACCELERATION = 210.172341f; 


	void Update() {

		GameObject thrownItem;
		Rigidbody2D thrownRigidBody;

		if (Input.GetKeyDown (KeyCode.A)) { //left key press
			thrownItem = Instantiate (item, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			thrownRigidBody = thrownItem.GetComponent <Rigidbody2D>();
			float objMass = thrownRigidBody.mass;
			float thrust = objMass * ACCELERATION; 
			thrownRigidBody.AddForce (transform.up * thrust);
		}


			

	}
}
