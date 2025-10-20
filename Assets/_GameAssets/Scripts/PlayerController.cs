using System.Runtime.CompilerServices;

using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]

    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movemementSpeed;

    [Header("Jump Settings")]

    [SerializeField] private KeyCode _Jumpkey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _canJump;

    [Header("Sliding Settings")]    
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    private Rigidbody _playerRigidbody;

    private float _horizantalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizantalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }

        else if (Input.GetKey(_Jumpkey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizantalInput;

        if (_isSliding)
        {
            _playerRigidbody.AddForce(_movementDirection.normalized * _movemementSpeed * _slideMultiplier, ForceMode.Force);
        }
        else
        {
            _playerRigidbody.AddForce(_movementDirection.normalized * _movemementSpeed, ForceMode.Force);
        }
    }

    private void SetPlayerDrag()
    {
        if (_isSliding)
        {
            _playerRigidbody.linearDamping = _groundDrag;
        }
        else
        {
            _playerRigidbody.linearDamping = _groundDrag;
        }

    }
    

    private void LimitPlayerSpeed()
    {
        Vector3 FlatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        if(FlatVelocity.magnitude > _movemementSpeed)
        {
            Vector3 LimitedVelocity = FlatVelocity.normalized * _movemementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(LimitedVelocity.x, _playerRigidbody.linearVelocity.y, LimitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}

