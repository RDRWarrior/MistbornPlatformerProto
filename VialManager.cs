using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialManager : MonoBehaviour
{
    public List<GameObject> vials = new List<GameObject>();  // List to store vials
    private Dictionary<GameObject, (Vector3 position, Quaternion rotation)> originalTransforms
        = new Dictionary<GameObject, (Vector3, Quaternion)>();

    void Start()
    {
        foreach (GameObject vial in vials)
        {
            if (vial != null)
            {
                originalTransforms[vial] = (vial.transform.position, vial.transform.rotation);
            }
        }
    }

    // Function to reset all vials back to their original positions and rotations
    public void ResetVials()
    {
        foreach (GameObject vial in vials)
        {
            if (vial != null)
            {
                vial.SetActive(true);
                var (position, rotation) = originalTransforms[vial];
                vial.transform.position = position;
                vial.transform.rotation = rotation;
                Debug.Log($"Vial {vial.name} reset to original position and rotation.");
            }
        }
    }

    // Function to reset only inactive vials
    public void ResetInactiveVials()
    {
        foreach (GameObject vial in vials)
        {
            if (vial != null && !vial.activeSelf)
            {
                var (position, rotation) = originalTransforms[vial];
                vial.transform.position = position;
                vial.transform.rotation = rotation;
                vial.SetActive(true);
                Debug.Log($"Vial {vial.name} reactivated and reset.");
            }
        }
    }

    // Add a vial to the manager
    public void AddVial(GameObject vial)
    {
        if (!vials.Contains(vial))
        {
            vials.Add(vial);
            originalTransforms[vial] = (vial.transform.position, vial.transform.rotation);
        }
    }

    // Remove a vial from the manager
    public void RemoveVial(GameObject vial)
    {
        if (vials.Contains(vial))
        {
            vials.Remove(vial);
            originalTransforms.Remove(vial);
        }
    }

    // Debugging: Log the states of all vials
    public void LogVialStates()
    {
        foreach (GameObject vial in vials)
        {
            Debug.Log($"Vial: {vial.name}, Active: {vial.activeSelf}, Position: {vial.transform.position}");
        }
    }
}