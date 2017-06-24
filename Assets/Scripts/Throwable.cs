using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Throwable : MonoBehaviour {

    public ThrowableData data;
    public UnityEvent OnThrown;

    public float Area
    {
        get
        {
            if (Collider is CapsuleCollider2D)
            {
                CapsuleCollider2D c = (Collider as CapsuleCollider2D);
                return c.size.x * c.size.y;
            } 
            else
            {
                throw new System.Exception("Collider type not defined!");
            }
        }
    }
    public float Mass
    {
        get
        {
            return data.density * Area;
        }
    }

    private Collider2D _collider = null;
    public Collider2D Collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();

            return _collider;
        }
    }
    private Rigidbody2D _rb = null;
    public Rigidbody2D Rigidbody
    {
        get
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody2D>();

            return _rb;
        }
    }

    private void Awake()
    {
        Rigidbody.mass = Mass;
    }
    private void OnValidate()
    {
        Rigidbody.mass = Mass;
    }

    public void Throw()
    {
        data.onThownSound.Post(gameObject);
        OnThrown.Invoke();
    }

}
