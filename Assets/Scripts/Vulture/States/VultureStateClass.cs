using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureStateClass : MonoBehaviour, IVultureState
{
    [SerializeField] protected InputActionReference movement;
    [SerializeField] protected float speed = 0, baseJumpPower;

    private GameObject _vulture;
    private bool _jump, _duck, _attack;

    protected VultureObject vultObj;
    protected Rigidbody _rb;
    protected Vector2 _movementDirection;

    public event Action<int> OnStateSwitch = (_newState) => { };

    public GameObject Vulture
    {
        get { return _vulture; }
        set { _vulture = value; }
    }

    public bool isGrounded
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

        vultObj = Vulture.GetComponent<VultureObject>();
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
            isGrounded = Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f) && vultObj.PlatformContact();
            /*Debug.Log("Raycast collision: " + Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f)
                + " Platform collision: " + vultObj.PlatformContact());
            Debug.DrawRay(_rb.position, Vector3.down * hit.distance, Color.green, 1f);*/
            if (_movementDirection != Vector2.zero)
            {
                _rb.transform.forward = (new Vector3(_movementDirection.x, 0, _movementDirection.y)).normalized;
            }

            _rb.velocity = new Vector3(_movementDirection.x * speed * Time.fixedDeltaTime, _rb.velocity.y, _movementDirection.y * speed * Time.fixedDeltaTime);
        }
    }

    public virtual void Ducking() { return; }

    public virtual void DisableDucking() { return; }

    public virtual void Jumping() { return; }

    public virtual void DisableJumping() { return; }

    public virtual void Landing() { return; }

    protected virtual void ChildSwitchState(int _newState)  // used by child scripts to call parent event action
    {
        OnStateSwitch(_newState);
    }
}