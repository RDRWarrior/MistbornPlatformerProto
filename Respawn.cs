using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public Transform RespawnPoint;        // The respawn position
    public GameObject respawnEffect;     // Visual effect for respawn
    private VialManager vialManager;      // Reference to the VialManager
    private bool isRespawning = false;   // Prevent re-triggering during respawn

    void Start()
    {
        // Find the VialManager in the scene
        vialManager = FindObjectOfType<VialManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isRespawning) return; // Prevent immediate re-triggering

        if (other.CompareTag("Player"))
        {
            StartCoroutine(RespawnPlayer(other));
        }
    }

    IEnumerator RespawnPlayer(Collider player)
    {
        isRespawning = true;

        // Optional: Add visual feedback
        if (respawnEffect != null)
            Instantiate(respawnEffect, RespawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(1f); // Delay before respawning

        // Move player to the respawn point
        player.transform.position = RespawnPoint.position;
        Debug.Log("Player respawned at: " + RespawnPoint.position);

        // Reset player abilities
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.ResetAllomanticAbilities();
        }

        // Reset vials
        if (vialManager != null)
        {
            vialManager.ResetVials();
        }

        // Reset velocity
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        yield return new WaitForSeconds(0.5f); // Optional: Delay before allowing re-trigger
        isRespawning = false;
    }

    // Update the respawn point dynamically
    public void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        RespawnPoint = newRespawnPoint;
        Debug.Log("Respawn point updated to: " + newRespawnPoint.position);
    }
}