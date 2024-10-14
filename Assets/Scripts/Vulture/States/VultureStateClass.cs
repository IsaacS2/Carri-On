using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VultureStateClass : MonoBehaviour, IVultureState
{
    [SerializeField] protected InputActionReference movement;
    [SerializeField] protected float speed = 0, baseJumpPower;
    [SerializeField] protected bool newMovement = false;

    private GameObject _vulture;
    private bool _jump, _duck, _attack;

    protected VultureObject vultObj;
    protected Rigidbody _rb;
    protected Vector3 _velocity;
    protected Vector2 _movementDirection;
    protected int stepsSinceLastGrounded;
    protected bool isJumping;

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
        UpdateState();
        AdjustVelocity();
        //RaycastHit hit;

        if (_rb != null)
        {
            isGrounded = vultObj.PlatformContact();
            /*isGrounded = Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f) && vultObj.PlatformContact();
            Debug.Log("Raycast collision: " + Physics.Raycast(_rb.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f)
                + " Platform collision: " + vultObj.PlatformContact());
            Debug.DrawRay(_rb.position, Vector3.down * hit.distance, Color.green, 1f);*/
            if (_movementDirection != Vector2.zero)
            {
                _rb.transform.forward = (new Vector3(_movementDirection.x, 0, _movementDirection.y)).normalized;
            }

            if (!newMovement || isJumping) {
                _rb.velocity = new Vector3(_movementDirection.x * speed * Time.fixedDeltaTime, _rb.velocity.y, _movementDirection.y * speed * Time.fixedDeltaTime);
            }
            else
            {
                _rb.velocity = _velocity;
            }
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


    Vector3 ProjectOnContactPlane(Vector3 vector)
    {
        if (vultObj) {
            Vector3 contactNorm = vultObj.GetContactNormal();
            return vector - vultObj.GetContactNormal() * Vector3.Dot(vector, contactNorm);
        }
        return Vector3.up;
    }

    protected virtual void UpdateState()
    {
        stepsSinceLastGrounded += 1;

        if (_rb)
        {
            _velocity = _rb.velocity;
        }

        if (vultObj && (isGrounded || vultObj.SnapToGround(ref _velocity, stepsSinceLastGrounded)))
        {
            stepsSinceLastGrounded = 0;
        }
    }

    protected virtual void AdjustVelocity()
    {
        if (_rb)
        {
            Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            float currentX = Vector3.Dot(_velocity, xAxis);
            float currentZ = Vector3.Dot(_velocity, zAxis);

            _velocity += (xAxis * ((_movementDirection.x * speed * Time.fixedDeltaTime) - currentX)) 
                + (zAxis * ((_movementDirection.y * speed * Time.fixedDeltaTime) - currentZ));
        }
    }
}