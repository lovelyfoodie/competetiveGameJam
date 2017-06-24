using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class Tower : MonoBehaviour {

	Rigidbody2D towerShaft;

	void Start() {
		towerShaft = GetComponent<Rigidbody2D> ();

	}

	public void addForce(float xForce){
		towerShaft.AddForce (transform.right * xForce);
	}

}
