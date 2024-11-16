using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float maxSpeed = 10.0f;
    public float drag = 0.1f;

    public Transform cameraTransform;
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isSteelActive = false;
    private bool isPewterActive = false;

    public float steelPushForce = 30f;
    public float pushRange = 10f;
    public LayerMask metalLayer;
    public LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.enabled = false; // Hide the line at the start
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplyDrag();
    }

    void Update()
    {
        HandleJump();
        HandleSteelPush();
        DrawSteelLines();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        float currentSpeed = isPewterActive ? moveSpeed * 1.5f : moveSpeed;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            rb.AddForce(moveDirection * currentSpeed, ForceMode.Acceleration);
        }

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVelocity.magnitude > maxSpeed)
        {
            rb.velocity = flatVelocity.normalized * maxSpeed + new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void ApplyDrag()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - drag), rb.velocity.y, rb.velocity.z * (1 - drag));
        }
    }

    private void HandleJump()
    {
        // Raycast for ground detection
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckRadius, groundMask);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckRadius, isGrounded ? Color.green : Color.red);

        // Debugging
        Debug.Log($"IsGrounded: {isGrounded} | Space Pressed: {Input.GetButtonDown("Jump")}");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            float currentJumpForce = isPewterActive ? jumpForce * 1.5f : jumpForce;
            rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);

            Debug.DrawRay(transform.position, Vector3.up * currentJumpForce, Color.blue, 1f);
            Debug.Log("Jump triggered!");
        }
    }

    private void HandleSteelPush()
    {
        if (isSteelActive && Input.GetKeyDown(KeyCode.R))
        {
            ActivateSteelPush();
        }
    }

    private void ActivateSteelPush()
    {
        Collider[] metals = Physics.OverlapSphere(transform.position, pushRange, metalLayer);

        if (metals.Length > 0)
        {
            Transform closestMetal = metals[0].transform;
            Vector3 pushDirection = (transform.position - closestMetal.position).normalized;

            rb.AddForce(pushDirection * steelPushForce, ForceMode.Impulse);
            Debug.Log("Steel Push activated!");
        }
        else
        {
            Debug.Log("No metal objects in range for Steel Push.");
        }
    }

    private void DrawSteelLines()
    {
        Collider[] metals = Physics.OverlapSphere(transform.position, pushRange, metalLayer);
        if (metals.Length > 0)
        {
            Transform closestMetal = metals[0].transform;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, closestMetal.position);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    // Placeholder methods to resolve missing definitions
    public void ResetAllomanticAbilities()
    {
        Debug.Log("Allomantic abilities reset.");
    }

    public void ActivatePewter()
    {
        Debug.Log("Pewter activated! Increased speed and jump height.");
    }

    public void ActivateSteel()
    {
        Debug.Log("Steel activated! You can now use Steel Push.");
    }

    public void ActivateIron()
    {
        Debug.Log("Iron activated! You can now use Iron Pull.");
    }

    public void StartClimb(Vector3 targetPosition)
    {
        Debug.Log("Climbing started towards: " + targetPosition);
    }
}