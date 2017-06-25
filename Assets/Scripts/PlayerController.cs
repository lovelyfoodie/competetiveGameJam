
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
    public Transform holdPoint;
    public GameObject held;
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

        if (holdPoint == null)
            holdPoint = GetComponent<Transform>();
    }
	void Start() {
		spawner = GetComponent<ThrowableSpawner> ();
		tower = FindObjectOfType<Tower> ();

        // Init held throwable.
        LoadNextItem();
    }

    void Update() {

		GameObject thrownItem;
		Rigidbody2D thrownRigidBody;

		if (Input.GetKeyDown (fireKey)) {

            // Get next throwable.
            thrownItem = held;
            thrownItem.transform.SetParent(null);
            //thrownItem = spawner.GetThrowable();
            thrownItem.transform.position = throwPoint.transform.position;
            thrownItem.transform.rotation = gameObject.transform.rotation;

            thrownItem.GetComponent<Collider2D>().enabled = true;

            thrownRigidBody = thrownItem.GetComponent <Rigidbody2D>();
            thrownRigidBody.isKinematic = false;

            // Physics.
            float objMass = thrownRigidBody.mass;
			float thrust = objMass * ACCELERATION; 
			thrownRigidBody.AddForce (transform.up * thrust);
            thrownRigidBody.AddTorque(Random.Range(-1f * throwTorque * thrust, throwTorque * thrust));

            float angleOfRelease = transform.rotation.z;
			float xForce = Mathf.Cos (angleOfRelease) * thrust * .1f * direction; 
			tower.addForce (xForce);

            thrownItem.GetComponent<Throwable>().Throw();

            LoadNextItem();
        }
	}

    void LoadNextItem()
    {
        held = spawner.GetThrowable();
        held.transform.SetParent(holdPoint);
        held.transform.localPosition = Vector3.zero;
        held.transform.localRotation = Quaternion.identity;
        held.GetComponent<Rigidbody2D>().isKinematic = true;
        held.GetComponent<Collider2D>().enabled = false;
    }
}

