
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Security.Cryptography;

[RequireComponent(typeof(ThrowableSpawner))]

public class PlayerController : MonoBehaviour {

    private const float ACCELERATION = 210.172341f;

    public bool isTower1 = false;
    public KeyCode fireLeft;
    public KeyCode fireRight;
    public float throwTorque = 0.2f;
    public Transform throwPointLeft;
    public Transform throwPointRight;
    public Transform holdPoint;
    public Vector3 throwAngle = new Vector3(0, 0, 72f);
    public GameObject held;
    //[Tooltip("1f = left, -1f = right")]
    //public float direction = 1f;
    public SpriteRenderer sprite;

	private GameObject item;
	private ThrowableSpawner spawner;
	private TowerControl _tower;

    void Awake()
    {
        //if(throwPoint == null)
        //    throwPoint = GetComponent<Transform>();

        if (holdPoint == null)
            holdPoint = GetComponent<Transform>();
    }
	void Start() {
		spawner = GetComponent<ThrowableSpawner> ();
		_tower = GetComponentInParent<TowerControl> ();

        // Init held throwable.
        LoadNextItem();
    }

    void Update() {

		GameObject thrownItem;
		Rigidbody2D thrownRigidBody;

        bool left = Input.GetKeyDown(fireLeft);
        bool right = Input.GetKeyDown(fireRight);


        if (left || right) {
            float directionMod = left ? 1f : -1f;

            // Sprite change.
            sprite.flipX = right;

            // Get next throwable.
            thrownItem = held;
            thrownItem.transform.SetParent(null);
            thrownItem.transform.position = left ? throwPointLeft.transform.position : throwPointRight.transform.position;
            thrownItem.transform.rotation = gameObject.transform.rotation;

            thrownItem.GetComponent<Collider2D>().enabled = true;

            thrownRigidBody = thrownItem.GetComponent <Rigidbody2D>();
            thrownRigidBody.isKinematic = false;

            // Physics.
            float objMass = thrownRigidBody.mass;
			float thrust = objMass * ACCELERATION; 
			//thrownRigidBody.AddForce (throwAngle * thrust * directionMod); //good
            thrownRigidBody.AddForce(transform.up * thrust * directionMod);
            thrownRigidBody.AddTorque(Random.Range(-1f * throwTorque * thrust, throwTorque * thrust));

   //         float angleOfRelease = throwAngle.z * directionMod;
   //         //float angleOfRelease = Quaternion.Euler(throwAngle).z * directionMod;
   //         //float angleOfRelease = transform.rotation.z * directionMod;
   //         float xForce = Mathf.Cos (angleOfRelease) * thrust * .1f; 
			//tower.addForce (xForce);

            _tower.addForce(directionMod * thrust * .1f);

            thrownItem.GetComponent<Throwable>().Throw();

            LoadNextItem();
        }
	}

    void LoadNextItem()
    {
        held = spawner.GetThrowable();
        held.layer = isTower1 ? 8 : 13;
        held.transform.SetParent(holdPoint);
        held.transform.localPosition = Vector3.zero;
        held.transform.localRotation = Quaternion.identity;
        held.GetComponent<Rigidbody2D>().isKinematic = true;
        held.GetComponent<Collider2D>().enabled = false;
    }
}

