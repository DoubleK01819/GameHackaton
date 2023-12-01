using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float _jumpForce = 1000f;
    [SerializeField] Animator _animator;
    public float GroundDistance = 0.5f;
    private float _gravity = -9.8f;
    Vector3 _velocity;

    private void Start()
    {
        _animator.SetTrigger("Idle");
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _moveVector = transform.right * x + transform.forward * z;

        _characterController.Move(_moveVector * _speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(_groundCheck.position, GroundDistance, _groundMask);
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpForce * -2.0f * _gravity);
        }

        Animation(x, z, _velocity);
    }

    private void Animation(float forward, float z, Vector3 velocity)
    {
        if (z > 0 && velocity.y == 0)
        {
            _animator.SetTrigger("WalkForward");
        }

        else if (forward < 0)
        {

        }

        if (velocity.y > 0)
        {
            _animator.SetTrigger("Jump");
        }

        if (forward == 0 && z == 0 && velocity.y == 0)
        {
            _animator.SetTrigger("Idle");
        }
    }    
}
