using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureStateClass : MonoBehaviour, IVultureState
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] protected float speed = 0;

    private GameObject _vulture;
    protected Rigidbody _rb;
    protected Vector2 _movementDirection;
    protected bool _jump, _duck, _attack;

    public GameObject Vulture
    {
        get { return _vulture; }
        set { _vulture = value; }
    }

    public bool jump
    {
        get { return _jump; }
        set { _jump = value; }
    }

    public bool duck
    {
        get { return _duck; }
        set { _duck = value; }
    }

    public bool attack
    {
        get { return _attack; }
        set { _attack = value; }
    }

    protected virtual void Start()
    {
        // acquire the rigidbody of either the vulture model or the parent object
        if (Vulture != null && Vulture.transform.parent && Vulture.transform.parent.GetComponent<Rigidbody>()) 
        { 
            _rb = Vulture.transform.parent.GetComponent<Rigidbody>();
        }
        else if (Vulture != null && Vulture.GetComponent<Rigidbody>())
        {
            _rb = Vulture.GetComponent<Rigidbody>();
        }
    }

    protected virtual void Update()
    {
        if (movement != null) { _movementDirection = movement.action.ReadValue<Vector2>(); }
    }

    protected virtual void FixedUpdate()
    {
        RaycastHit hit;
        
        if (_rb != null)
        {
            jump = Physics.Raycast(_rb.position, Vector3.down, out hit);
            Debug.DrawRay(_rb.position, Vector3.down * hit.distance, Color.green, 60f);
            _rb.velocity = new Vector3(_movementDirection.x * speed, _rb.velocity.y, _movementDirection.y * speed);
        }
    }

    public virtual void Ducking() { return; }

    public virtual void DisableDucking() { return; }

    public virtual void Jumping() { return; }

    public virtual void DisableJumping() { return; }

    public virtual void Landing() { return; }
}