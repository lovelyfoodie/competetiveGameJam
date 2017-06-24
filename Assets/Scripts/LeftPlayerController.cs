using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

[RequireComponent(typeof(ThrowableSpawner))]

public class LeftPlayerController : MonoBehaviour {

	private GameObject item;
	private ThrowableSpawner spawner;
	private const float ACCELERATION = 210.172341f; 

	void Start() {

		spawner = GetComponent<ThrowableSpawner> ();
	}

	void Update() {

		GameObject thrownItem;
		Rigidbody2D thrownRigidBody;

		if (Input.GetKeyDown (KeyCode.A)) { //left key press
			item = spawner.GetThrowable ();
			thrownItem = Instantiate (item, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			thrownRigidBody = thrownItem.GetComponent <Rigidbody2D>();
			float objMass = thrownRigidBody.mass;
			float thrust = objMass * ACCELERATION; 
			thrownRigidBody.AddForce (transform.up * thrust);
		}


			

	}
}
