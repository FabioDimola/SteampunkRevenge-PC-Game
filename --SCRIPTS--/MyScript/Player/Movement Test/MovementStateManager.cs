using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3;
    [HideInInspector] public Vector3 dir;
    float hInput, vInput;
    CharacterController controller;

    private Animator _animator;

    [Header("Ground check Settings")]
    [SerializeField] private LayerMask _groundCheckMask; //layer su cui agisce la sfera che determina collisione terreno serve per escludere il collider dal player
    [SerializeField] private float _groundCheckRadius; //raggio sfera
    [SerializeField] private Vector3 _groundCheckOffset;
    private bool _isGrounded;


    [Header("Jump Settings")]
    private float _timeAtNextJump;
    private float _currentVerticalSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    private Transform startPos;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        GetDirectionAndMove();
    }

    private void Update()
    {
        GetDirectionAndMove();
        CheckIsGrounded();
        ApplyGravity();
        Jump();
    }
    void GetDirectionAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");



        dir = transform.forward * vInput + transform.right * hInput;

        controller.Move(dir* moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos() //disegna sfera per determinare se il player è a terra o no
    {
        

        //SFERA GROUND CHECK //player is ignore Ray Cast layer
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckOffset, _groundCheckRadius);
    }

    //controlla pos sfera,raggio sfera e se ci sono layer mask
    private void CheckIsGrounded()
    {
        _isGrounded = Physics.CheckSphere(transform.TransformPoint(_groundCheckOffset), _groundCheckRadius, _groundCheckMask);
        _animator.SetBool("Grounded", _isGrounded); //da inserire 
    }


    private void ApplyGravity()
    {
        if (_isGrounded && _currentVerticalSpeed <= 0) _currentVerticalSpeed = 0;
        else _currentVerticalSpeed -= 9.81f * Time.deltaTime;
    }



    public void Jump()
    {

        if (_isGrounded && _timeAtNextJump <= Time.time)
        {
            _currentVerticalSpeed += _jumpForce;
            Vector3 move = new Vector3(0, _currentVerticalSpeed, 0);
            controller.Move(move * Time.deltaTime);
            _timeAtNextJump = Time.time + _jumpCooldown; //tempo che intercorre tra due salti
        }

    }


   
}
