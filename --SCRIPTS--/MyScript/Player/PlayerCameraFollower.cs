using System;
using System.Collections;

using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Cameras
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        
        [SerializeField] private Transform _target;
        [SerializeField][HideInInspector] private Transform _cameraSocket;
        
        [Header("Socket Settings")]
        [SerializeField] private Vector3 _zeroOffset;
        [SerializeField] private float _currentArmLenght;
        
        [Header("Rotation Settings")]
        [SerializeField] private float _lookInputHardness;
        [SerializeField][Tooltip("Horizontal rotation speed in degrees*second")] private float _horizontalRotationSpeed;
        [SerializeField][Tooltip("Vertical rotation speed in degrees*second")] private float _verticalRotationSpeed;
        
        private Vector2 _lookInput; 
        private Vector2 _smoothLookInput;
        
        private void OnValidate()
        {
            if (_target != null)
            {
                UpdatePosition();
            }
            if (_cameraSocket == null)
            {
                _cameraSocket = new GameObject().transform;
                _cameraSocket.parent = transform;
                _cameraSocket.gameObject.name = "Camera Socket";
                _cameraSocket.transform.position = Vector3.zero;
                _cameraSocket.transform.rotation = Quaternion.identity;
            }
            else
            {
                Vector3 socketPos = new Vector3(0, 0, -_currentArmLenght);
                _cameraSocket.transform.localPosition = socketPos;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _cameraSocket.position);
            Gizmos.DrawWireSphere(_cameraSocket.position, 0.05f);
        }

        

       

        private void SetArmLenght(float lenght)
        {
            _cameraSocket.transform.Translate(0, 0, (_currentArmLenght - lenght), Space.Self);
            _currentArmLenght = lenght;
        }

        private void UpdatePosition()
        {
            transform.position = _target.transform.position + _zeroOffset;
        }

        private void UpdateRotation()
        {
            transform.Rotate(0, _smoothLookInput.x * Time.deltaTime * _horizontalRotationSpeed, 0, Space.Self);
            _cameraSocket.RotateAround(transform.position, transform.right, _smoothLookInput.y * Time.deltaTime * _verticalRotationSpeed);
        }

        private void SmoothLookInputVector()
        {
            _lookInput = Vector2.ClampMagnitude(_lookInput, 1);
            _smoothLookInput = Vector2.Lerp(_smoothLookInput, _lookInput, Mathf.Clamp01(_lookInputHardness * Time.deltaTime));
        }
        
        private void Update()
        {
            _lookInput.x = Input.GetAxis("Mouse X");
            _lookInput.y = -Input.GetAxis("Mouse Y");
            SmoothLookInputVector();
          
        }

        private void LateUpdate()
        {
            UpdatePosition();
            UpdateRotation();
        }
    }
}