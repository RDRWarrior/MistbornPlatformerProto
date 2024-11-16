using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Pewter, Steel, Iron
}

public class VialPickup : MonoBehaviour
{
    public AbilityType abilityType;  // Ability type that the vial unlocks
    public GameObject pickupEffect;  // Visual effect for pickup
    private bool isPickedUp = false; // Prevent multiple activations

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return; // Prevent reactivation

        if (other.CompareTag("Player"))
        {
            isPickedUp = true; // Mark as picked up

            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                ActivateAbility(player);
            }

            // Visual feedback
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            Debug.Log($"Vial {gameObject.name} picked up. Ability: {abilityType}");

            gameObject.SetActive(false);
        }
    }

    private void ActivateAbility(PlayerMovement player)
    {
        switch (abilityType)
        {
            case AbilityType.Pewter:
                player.ActivatePewter();
                break;
            case AbilityType.Steel:
                player.ActivateSteel();
                break;
            case AbilityType.Iron:
                player.ActivateIron();
                break;
        }
    }

    // Reset the vial for reuse (e.g., on respawn)
    public void ResetVial()
    {
        isPickedUp = false;
        gameObject.SetActive(true);
        Debug.Log("Vial reset: " + gameObject.name);
    }
}
