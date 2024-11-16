using UnityEngine;

public class LedgeTrigger : MonoBehaviour
{
    public Transform ledgePosition; // Reference to the target position for the player to climb to

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Access the PlayerMovement script attached to the player
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // Trigger the climbing process by passing the ledge position
                player.StartClimb(ledgePosition.position);
                Debug.Log("Climb triggered to position: " + ledgePosition.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Optional: Visualize the ledge position in the Scene view for debugging
        if (ledgePosition != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(ledgePosition.position, 0.2f);
            Gizmos.DrawLine(transform.position, ledgePosition.position);
        }
    }
}