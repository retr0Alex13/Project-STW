using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    private BoxCollider safeZoneCollider;

    private void Start()
    {
        safeZoneCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out FirstPersonController player))
        {
            eventSystem.isInSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FirstPersonController player))
        {
            eventSystem.isInSafeZone = false;
        }
    }
}
