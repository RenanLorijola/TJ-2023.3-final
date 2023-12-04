using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float dashSpeed = 50f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1;
    public float jumpPower = 7f;
    public float gravity = 10f;
 
 
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
 
 
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
 
    public bool enableMove = true;
    public bool enableCameraRotation = true;

    // dashing data
    private bool dashing = false;
    private float dashingTime = 0;
    private float dashingCooldownTime = 0f;

    public bool CanMove => enableMove && !GameManager.Singleton.PlayingEvent;
    public bool CanRotateCamera => CanMove && enableCameraRotation;
    public bool DashInCooldown => dashingCooldownTime > 0;
 
    
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void Update()
    {
 
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 mDirection = ((forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"))).normalized;

        float movementAngle = Vector3.SignedAngle(forward, mDirection, Vector3.up);
        bool canRun = Mathf.Abs(movementAngle) <= 45 && CanMove;
        bool canDash = Mathf.Abs(movementAngle) >= 90 && characterController.isGrounded && CanMove && !DashInCooldown;
        bool canJump = CanMove && characterController.isGrounded && !dashing;
        bool pressedDash = Input.GetKeyDown(KeyCode.LeftShift);

        if (!dashing && canDash && pressedDash)
        {
            dashing = true;
            dashingTime = 0f;
            moveDirection = mDirection * dashSpeed;
            dashingCooldownTime = dashCooldown;
        }

        float movementDirectionY = 0;
        if (!dashing)
        {
            if (dashingCooldownTime > 0)
            {
                dashingCooldownTime = Mathf.Max(0f,dashingCooldownTime - Time.deltaTime);
            }
            
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;
            float movementSpeed = CanMove ? (isRunning ? runSpeed : walkSpeed) : 0;
            movementDirectionY = moveDirection.y;
            moveDirection = mDirection * movementSpeed;
        }
        else
        {
            dashingTime += Time.deltaTime;
            if (dashingTime >= dashDuration)
            {
                dashing = false;
            }
        }

        #endregion
 
        #region Handles Jumping
        if (Input.GetButton("Jump") && canJump)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
 
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
 
        #endregion
 
        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);
 
        if (CanRotateCamera)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
 
        #endregion
    }
}
 