using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    // for movement
    [SerializeField]
    private float speed = 5f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    //for jump
    public Transform foot;
    bool isGrounded;
    public float jumpHeight;
    bool isJump;


    //for Rotate Player
    [SerializeField]
    private float lookSensitivity = 3f;
    private Vector3 rotation = Vector3.zero;

    //for Rotate Camera
    [SerializeField]
    GameObject fpsCamera;
    private float CameraUpAndDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Jump();
        RotatePlayer();
        RotateCamera();
        LockAndUnlockCursor();
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (isJump)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (fpsCamera != null)
        {
            CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation;
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85, 85);
            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation, 0, 0);
        }
    }

    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");



        Vector3 movementHorizontal = transform.right * xMovement;
        Vector3 movementVertical = transform.forward * zMovement;

        Vector3 movementVelocity =
            (movementHorizontal + movementVertical).normalized
            * speed;

        velocity = movementVelocity;
    }

    void Jump()
    {
        isGrounded = Physics.Raycast(foot.position, -transform.up, 0.2f);
        isJump = Input.GetKey(KeyCode.Space) && isGrounded;
    }

    void RotatePlayer()
    {
        float yRotation = Input.GetAxis("Mouse X");
        rotation = new Vector3(0, yRotation, 0) * lookSensitivity;
    }

    void RotateCamera()
    {
        CameraUpAndDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;
    }
}
