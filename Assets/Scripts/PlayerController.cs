
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
    public Vector3 minThrowAngle = new Vector3(-1f, 1f, 0f);
    public Vector3 maxThrowAngle = new Vector3(-1f, 1f, 0f);
    public SpriteRenderer sprite;
    
	private ThrowableSpawner _spawner;
	private TowerControl _tower;

    private GameObject _held;

    void Awake()
    {
        if (holdPoint == null)
            holdPoint = GetComponent<Transform>();
    }
	void Start() {
		_spawner = GetComponent<ThrowableSpawner> ();
		_tower = GetComponentInParent<TowerControl> ();

        minThrowAngle.Normalize();
        maxThrowAngle.Normalize();

        // Init held throwable.
        LoadNextItem();
    }

    void Update() {

        bool left = Input.GetKeyDown(fireLeft);
        bool right = Input.GetKeyDown(fireRight);

        if (left || right) {
            float directionMod = left ? 1f : -1f;

            // Sprite change.
            sprite.flipX = right;

            // Get next throwable.
            var thrownItem = _held;
            thrownItem.transform.SetParent(null);
            thrownItem.transform.position = left ? throwPointLeft.transform.position : throwPointRight.transform.position;
            thrownItem.transform.rotation = gameObject.transform.rotation;
            thrownItem.GetComponent<Collider2D>().enabled = true;

            var thrownRigidBody = thrownItem.GetComponent <Rigidbody2D>();
            thrownRigidBody.isKinematic = false;

            // Physics.
            float objMass = thrownRigidBody.mass;
			float thrust = objMass * ACCELERATION;
            Vector2 angle = Vector2.Lerp(minThrowAngle, maxThrowAngle, Random.value);
			thrownRigidBody.AddForce(new Vector2(angle.x * thrust * directionMod, angle.y * thrust)); 
            thrownRigidBody.AddTorque(Random.Range(-1f * throwTorque * thrust, throwTorque * thrust));
            _tower.AddXForce(directionMod * thrust * .1f);

            // Process throw.
            thrownItem.GetComponent<Throwable>().Throw();

            // Queue next items.
            LoadNextItem();
        }
	}

    void LoadNextItem()
    {
        _held = _spawner.GetThrowable();
        _held.layer = isTower1 ? 8 : 13;
        _held.transform.SetParent(holdPoint);
        _held.transform.localPosition = Vector3.zero;
        _held.transform.localRotation = Quaternion.identity;
        _held.GetComponent<Rigidbody2D>().isKinematic = true;
        _held.GetComponent<Collider2D>().enabled = false;
    }
}

