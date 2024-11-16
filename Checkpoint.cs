using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Respawn respawnManager;  // Reference to the Respawn script

    void Start()
    {
        // Find the Respawn script in the scene
        respawnManager = FindObjectOfType<Respawn>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set this checkpoint as the new respawn point
            respawnManager.RespawnPoint = transform;
            Debug.Log("Checkpoint set!");
        }
    }
}