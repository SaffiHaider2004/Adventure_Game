using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float jumpForce = 2f;

    [Header("Stats")]
    public PlayerStats playerStats;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private bool jumpRequested = false;
    private bool isJumping = false;

    private bool punchRequested = false;

    void Update()
    {
        GroundCheck();
        ApplyGravity();
        MovePlayer();
        HandleJump();
        HandlePunch();
    }

    void GroundCheck()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;

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

            // Move player only if not mid-punch (optional: you can disable this if needed)
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

    public void PunchRequest()
    {
        
        punchRequested = true;
    }

    public float punchRange = 2f;
    public LayerMask zombieLayer;
    public void HandlePunch()
    {
        if (punchRequested)
        {
            animator.SetTrigger("punch");

            Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, punchRange, zombieLayer);

            foreach (var hit in hits)
            {
                ZombieHealth zombie = hit.GetComponent<ZombieHealth>();
                if (zombie != null)
                {
                    zombie.TakeDamage(1);
                    zombie.ShowHealthBar(true);
                }
            }
        }

        punchRequested = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, punchRange);
    }
}
