
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Security.Cryptography;

[RequireComponent(typeof(ThrowableSpawner))]

public class PlayerController : MonoBehaviour {

    public KeyCode fireKey;
    public float throwTorque = 0.2f;
    public Transform throwPoint;
    [Tooltip("1f = left, -1f = right")]
    public float direction = 1f;

	private GameObject item;
	private ThrowableSpawner spawner;
	private const float ACCELERATION = 210.172341f; 
	private Tower tower;

    void Awake()
    {
        if(throwPoint == null)
            throwPoint = GetComponent<Transform>();
    }
	void Start() {

		spawner = GetComponent<ThrowableSpawner> ();
		tower = FindObjectOfType<Tower> ();
	}

	void Update() {

		GameObject thrownItem;
		Rigidbody2D thrownRigidBody;

		if (Input.GetKeyDown (fireKey)) {
            thrownItem = spawner.GetThrowable();

            thrownItem.transform.position = throwPoint.transform.position;
            thrownItem.transform.rotation = gameObject.transform.rotation;
            //thrownItem = Instantiate(item, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

            thrownRigidBody = thrownItem.GetComponent <Rigidbody2D>();
			float objMass = thrownRigidBody.mass;
			float thrust = objMass * ACCELERATION; 
			thrownRigidBody.AddForce (transform.up * thrust);
            thrownRigidBody.AddTorque(Random.Range(-1f * throwTorque * thrust, throwTorque * thrust));

            float angleOfRelease = transform.rotation.z;
			float xForce = Mathf.Cos (angleOfRelease) * thrust * .1f * direction; 
			tower.addForce (xForce);

            thrownItem.GetComponent<Throwable>().Throw();
        }
	}
}

