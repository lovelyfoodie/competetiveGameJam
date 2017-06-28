﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class TowerControl : MonoBehaviour
{
    [Tooltip("Discplacement from vertical (in degrees) at which the tower will break.")]
    public float displacementLimit = 60f; // degrees
    
    public WwisePostEvent towerCreekSound;
    public WwisePostEvent towerCreekSoundStop;
    public WwiseSetRTPC towerVelocityChange;
    public WwiseSetRTPC towerDisplacementChange;

    private Rigidbody2D _rb;
    private HingeJoint2D _hinge;
    private float _prevDisplacement = 0f;
    private int _maxMoveSamples = 5;
    private float[] _moveSamples;

    public float Velocity
    {
        get
        {
            return MoveSampleAverage() * 100;
        }
    }
    public float Displacement
    {
        get
        {
            return Mathf.Abs(_hinge.jointAngle) / displacementLimit * 6.83f;
            //return (_originalCenterPosition - currentCenter.position).magnitude;
        }
    }

    //-----------------------------

    private void Awake()
    {
        towerVelocityChange.ResetRangeTracker();
        towerDisplacementChange.ResetRangeTracker();
        _rb = GetComponent<Rigidbody2D>();
        _hinge = GetComponent<HingeJoint2D>();
        _moveSamples = new float[_maxMoveSamples];
    }

    private void Start()
    {
        towerCreekSound.Post(gameObject);
    }

    private void FixedUpdate()
    {
        if (_prevDisplacement != Displacement)
        {
            // Calculate and update velocity and displacement.
            float change = _prevDisplacement - Displacement;
            AddMoveSample(change);

            towerVelocityChange.SetValue(Velocity, gameObject);
            towerDisplacementChange.SetValue(Displacement, gameObject);

            _prevDisplacement = Displacement;

            // Check angle and break tower if over limit.
            if (Mathf.Abs(_hinge.jointAngle) > displacementLimit)
                Kill();
        }
    }

    //-----------------------------

    private void AddMoveSample(float amount)
    {
        for (int i = _maxMoveSamples - 1; i > 0; i--)
        {
            _moveSamples[i] = _moveSamples[i - 1];
        }
        _moveSamples[0] = amount;
    }
    private float MoveSampleAverage()
    {
        float sum = 0f;
        foreach (var item in _moveSamples)
        {
            sum += item;
        }
        return sum / _maxMoveSamples;
    }
    public void Kill()
    {
        towerCreekSoundStop.Post(gameObject);

        _hinge.enabled = false;

        //TODO: add upward force for hilarity
        //TODO: detach players for hilarity
        //TODO: inform game to end
    }

    public void addForce(float xForce)
    {
        _rb.AddForce(transform.right * xForce);
    }
}

