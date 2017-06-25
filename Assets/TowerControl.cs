using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerControl : MonoBehaviour
{
    public Transform currentCenter;
    
    public WwisePostEvent towerCreekSound;
    public WwisePostEvent towerCreekSoundStop;
    public WwiseSetRTPC towerVelocityChange;
    public WwiseSetRTPC towerDisplacementChange;

    private Transform _originalCenter;
    private float _prevDisplacement = 0f;
    private int _maxMoveSamples = 5;
    private float[] _moveSamples;

    //TODO IsFalling?

    public float Velocity
    {
        get
        {
            return MoveSampleAverage();
        }
    }
    public float Displacement
    {
        get
        {
            return (_originalCenter.position - currentCenter.position).magnitude;
        }
    }

    //-----------------------------

    private void Awake()
    {
        towerCreekSound.Post(gameObject);
         _originalCenter = currentCenter;
        _moveSamples = new float[_maxMoveSamples];
    }

    private void FixedUpdate()
    {
        if (_prevDisplacement != Displacement)
        {
            float change = _prevDisplacement - Displacement;
            AddMoveSample(change);

            towerVelocityChange.SetValue(Velocity, gameObject);
            towerDisplacementChange.SetValue(Displacement, gameObject);

            _prevDisplacement = Displacement;
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
    }
}

