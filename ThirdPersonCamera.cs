using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;       // Reference to the player
    public Vector3 offset;         // Offset of the camera from the player
    public float sensitivity = 2f; // Mouse sensitivity for rotation
    public float distance = 5f;    // Distance from the player

    private float pitch = 0f;      // Vertical rotation
    private float yaw = 0f;        // Horizontal rotation

    void Start()
    {
        // Initialize the offset and camera position
        offset = new Vector3(0, 2, -distance);
    }

    void LateUpdate()
    {
        // Get mouse input for rotation
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -20, 45); // Limit vertical rotation

        // Calculate new camera position based on rotation and offset
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // Move and rotate the camera
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * 1.5f); // Slightly above player's head
    }
}