using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
public class PlayerJoystickMovement : MonoBehaviour
{
    [Header("Joystick Settings")]
    public FloatingJoystick joystick;
    public Transform cameraTransform;
    public CharacterController cC;
    public Animator animator;

    [Header("Surface Check")]
    public Transform surfaceCheck;
    public float surfaceDistance = 0.6f;
    public LayerMask surfaceMask;
    private bool onSurface;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpForce = 1f;

    [Header("Stats")]
    public PlayerStats playerStats;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private bool jumpRequested = false;
    private bool isJumping = false;

    void Update()
    {
        GroundCheck();
        ApplyGravity();
        MovePlayer();
        HandleJump();
    }

    void GroundCheck()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;

            // If player has landed, reset jumping state
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
    }

    void MovePlayer()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float moveSpeed = playerStats.isSprinting && playerStats.GetStamina() > 0f
                ? playerStats.sprintSpeed
                : playerStats.walkSpeed;

            cC.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            animator.SetBool("walk", !playerStats.isSprinting);
            animator.SetBool("running", playerStats.isSprinting);
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("running", false);
        }
    }

    public void JumpRequest()
    {
        jumpRequested = true;
    }

    public void HandleJump()
    {
        if (onSurface && jumpRequested && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        jumpRequested = false;
    }
}
