using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class TowerControl : MonoBehaviour
{
    [Tooltip("Discplacement from vertical (in degrees) at which the tower will break.")]
    public float displacementLimit = 60f; // degrees
    public float killForce = 1f;
    public float killTorque = 0.5f;
    public Vector2 killVector;

    public WwisePostEvent towerCreekSound;
    public WwisePostEvent towerCreekSoundStop;
    public WwiseSetRTPC towerVelocityChange;
    public WwiseSetRTPC towerDisplacementChange;

    private Rigidbody2D _rb;
    private HingeJoint2D _hinge;
    private EndGameControl _end;
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
        
        _end = FindObjectOfType<EndGameControl>();
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
        float directionMod = (_hinge.jointAngle > 0f) ? 1f : -1f;
        killVector.Normalize();

        towerCreekSoundStop.Post(gameObject);

        _hinge.enabled = false;

        AddForce(new Vector2(killVector.x * killForce * directionMod, killVector.y * killForce));
        AddTorque(killTorque * -directionMod);

        //TODO: detach players for hilarity

        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        _end.EndGame();
    }

    public void AddXForce(float xForce)
    {
        AddForce(transform.right * xForce);
    }
    public void AddForce(Vector2 force)
    {
        _rb.AddForce(force);
    }
    public void AddTorque(float amount)
    {
        _rb.AddTorque(amount);
    }
}

