using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFloater : MonoBehaviour {

    public float minSpeed = 0.5f;
    public float maxSpeed = 2f;
    public bool isMovingLeft = false;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    private float _speed;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _speed = Random.Range(minSpeed, maxSpeed);
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length-1)];
    }

    private void Update()
    {
        transform.position = transform.position + (_speed * Time.deltaTime * (isMovingLeft ? Vector3.left : Vector3.right));

        // bleh... looping
        if (transform.position.x < -12)
            transform.position = new Vector3(12, transform.position.y, transform.position.z);
    }

}
