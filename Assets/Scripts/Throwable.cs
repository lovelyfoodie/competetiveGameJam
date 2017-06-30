using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Throwable : MonoBehaviour {

    public ThrowableData data;
    public UnityEvent OnThrown;

    private float _size = 1f;
    public float Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;

            if (data.thrownObjectSize != null)
            {
                data.thrownObjectSize.SetValue(GetSizeRatio(value), gameObject);
            }

            gameObject.transform.localScale = new Vector3(value, value, value);
        }
    }

    private float GetSizeRatio(float value)
    {
        return (value - data.minSize) / (data.maxSize - data.minSize);
    }

    public float Area
    {
        get
        {
            if (Collider is CapsuleCollider2D)
            {
                CapsuleCollider2D c = (Collider as CapsuleCollider2D);
                return c.bounds.size.x * c.bounds.size.y;
                //return c.size.x * c.size.y * transform.localScale.x * transform.localScale.y;
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

    private Animator _animator = null;
    public Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_animator == null)
                _animator = GetComponentInChildren<Animator>();

            return _animator;
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

        if (Animator != null)
            Animator.StartPlayback();
    }
    private void OnValidate()
    {
        Rigidbody.mass = Mass;
    }

    public void Throw()
    {
        if (data.onThrownSound != null)
            data.onThrownSound.Post(gameObject);

        if (Animator != null)
            Animator.StopPlayback();

        OnThrown.Invoke();
    }

}
