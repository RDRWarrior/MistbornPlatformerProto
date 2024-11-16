using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;             // Reference to the player
    public Vector3 offset = new Vector3(0, 5, -8); // Default offset from the player
    public float smoothSpeed = 0.125f;    // Smoothing speed for camera motion

    void LateUpdate()
    {
        // Adjust camera position with smoothing
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Optional: Make the camera always look at the player
        transform.LookAt(player.transform);
    }
}