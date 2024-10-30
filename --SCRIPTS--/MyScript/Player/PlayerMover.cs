using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.TextCore.Text;

namespace PlayerController
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private float _inputHardness;

      
        
        [Header("Movement Settings")]
        [SerializeField] private Transform _directionTransform;
        [SerializeField] private float _turnHardness;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _speedChangeHardness;

        [Header("Ground check Settings")]
        [SerializeField] private LayerMask _groundCheckMask; //layer su cui agisce la sfera che determina collisione terreno serve per escludere il collider dal player
        [SerializeField] private float _groundCheckRadius; //raggio sfera
        [SerializeField] private Vector3 _groundCheckOffset;
         private bool _isGrounded;


        [Header("Hanging")]
       
        bool hanging = false;
        bool canMove = true;
        

        [Header("Jump Settings")]
        private float _timeAtNextJump;
        private float _currentVerticalSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCooldown;


        private Vector2 _moveInput;
        private Vector2 _smoothMoveInput;

        private float _maxSpeed;
        private float _currentSpeed;

        private PlayerState _currentState;
        
        private Transform _lockedTransform;
        private bool _lockedOn;
      
        private CharacterController _characterController;
        private Animator _animator;

        private void OnDrawGizmos() //disegna sfera per determinare se il player è a terra o no
        {
        /*  Gizmos.matrix = _directionTransform.localToWorldMatrix;
          Gizmos.color = Color.blue;
          Vector3 pos = new Vector3(_smoothMoveInput.x, 0, _smoothMoveInput.y);
          Gizmos.DrawLine(Vector3.zero, pos);
          Gizmos.DrawWireSphere(pos, 0.05f);

            */

        //SFERA GROUND CHECK //player is ignore Ray Cast layer
          Gizmos.matrix = transform.localToWorldMatrix;
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(_groundCheckOffset, _groundCheckRadius);  
        }

        private void OnEnable()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();

         
        }

        private void OnDisable()
        {
           
        }




        private void Start()
        {
            _maxSpeed = _walkSpeed;
           
        }

        private void Update()
        {

            
                ReceiveInput();
                NormalMove();
            SmoothMaxSpeed();
            SmoothMoveInputVector();
            

            CheckIsGrounded();
            ApplyGravity();
           
            Punch();
            BackFlip();
            LedgeGrab();

            Debug.Log(canMove);

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

        IEnumerator EnableCanMove()
        {
            yield return new WaitForSeconds(3f);
            canMove = true;
        }

       public void Jump()
        {
            if (_isGrounded && _timeAtNextJump <= Time.time)
            {
                _currentVerticalSpeed += _jumpForce;
                _timeAtNextJump = Time.time + _jumpCooldown; //tempo che intercorre tra due salti
            }
           
        }

        private void ReceiveInput()
        {
            // Movement Input
            _moveInput.x = Input.GetAxis("Horizontal");
            _moveInput.y = Input.GetAxis("Vertical");
            
            // Jump Input
            if (Input.GetButtonDown("Jump"))
            {
               

                if (hanging)
                {
                   // ApplyGravity();
                    hanging = false;
                    Jump();
                    _animator.SetBool("Hang", false);
                   
                }
                else
                {
                    Jump();
                }   
            }
            
            // Sprint Toggle
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                OnSprintBegin();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                OnSprintEnd();
            }

          
        }


        void LedgeGrab()
        {

            bool isGrounded = Physics.CheckSphere(transform.TransformPoint(_groundCheckOffset), _groundCheckRadius, _groundCheckMask); 
            if( !hanging && !isGrounded)
            {
                RaycastHit downHit;
                Vector3 lineDownStar = (transform.position + Vector3.up*3f) + transform.forward;
                Vector3 lineDownEnd = (transform.position + Vector3.up * 0.7f) + transform.forward;

                Physics.Linecast(lineDownStar,lineDownEnd, out downHit, LayerMask.GetMask("Ground"));

                Debug.DrawLine(lineDownStar, lineDownEnd, Color.red);
                
                if(downHit.collider !=null)
                {
                    RaycastHit fwdHit;
                    Vector3 lineFwdStar = new Vector3(transform.position.x , downHit.point.y-0.1f,transform.position.z);
                    Vector3 lineFwdEnd = new Vector3(transform.position.x, downHit.point.y - 0.1f, transform.position.z) + transform.forward ;

                    Physics.Linecast(lineFwdStar, lineFwdEnd, out fwdHit, LayerMask.GetMask("Ground"));

                    Debug.DrawLine(lineFwdStar, lineFwdEnd, Color.red);


                if(fwdHit.collider != null)
                    {
                        _currentVerticalSpeed = 0;
                        hanging = true;
                        _animator.SetBool("Hang",true);

                        Vector3 hangPos = new Vector3(fwdHit.point.x,downHit.point.y, fwdHit.point.z);
                        Vector3 offset = transform.forward * - 0.1f + transform.up * 3f;
                        hangPos += offset;
                        transform.position = hangPos;
                        transform.forward -= fwdHit.normal; // normal is the face of the object that collides
                        canMove = false;
                    }
                }
            }
        }




        public void Punch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Punch");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetTrigger("Flying Punch");
            }
        }


        public void BackFlip()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                _animator.SetTrigger("Flip");
            }
        }

        private void OnSprintBegin()
        {
            _maxSpeed = _runSpeed;
        }
        
        private void OnSprintEnd()
        {
            _maxSpeed = _walkSpeed;
        }


        private void SmoothMoveInputVector()
        {
            _smoothMoveInput = Vector2.Lerp(_smoothMoveInput, _moveInput, Mathf.Clamp01(_inputHardness * Time.deltaTime));
        }

        private void SmoothMaxSpeed()
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, Mathf.Clamp01(_speedChangeHardness * Time.deltaTime));
        }

        public void ToggleLockOn(bool lockedOn)
        {
            _lockedOn = lockedOn;
        }

        public void SetLockedTransform(Transform lockedTransform)
        {
            _lockedTransform = lockedTransform;
        }

      

        private void NormalMove()
        {
            if (_smoothMoveInput.magnitude > 0.1f)
            {
                Vector3 inputPos = new Vector3(_smoothMoveInput.x, 0, _smoothMoveInput.y);
                
                Quaternion lookRot = Quaternion.LookRotation(_directionTransform.TransformPoint(inputPos) - _directionTransform.position);
                
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Mathf.Clamp01(_turnHardness * Time.deltaTime));
            }
            
            Vector3 move = transform.TransformDirection(new Vector3(0, _currentVerticalSpeed, _smoothMoveInput.magnitude * _currentSpeed));
                
            _animator.SetFloat("Speed_Z", move.magnitude);
                
            _characterController.Move(move * Time.deltaTime);
        }

       
    }
}