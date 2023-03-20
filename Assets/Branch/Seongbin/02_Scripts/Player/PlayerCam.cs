using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    private float _lookSensitivity;
    [SerializeField]
    private float _walkSpeed;

    private Rigidbody _rb;
    
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX = 0;

    [SerializeField]
    private Camera _mainCam;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraRotation();
        Move();
    }

    private void CameraRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0, _yRotation, 0) * _lookSensitivity;
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_characterRotationY));

        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * _lookSensitivity;
        _currentCameraRotationX -= _cameraRotationX;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

        _mainCam.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
    }
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * _walkSpeed;

        _rb.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
}
