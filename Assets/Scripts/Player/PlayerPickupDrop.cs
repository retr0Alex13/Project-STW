using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class PlayerPickupDrop : MonoBehaviour
    {
        [SerializeField] private float pickUpDistance = 2f;
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private Transform objectGrabPointTransform;
        [SerializeField] private LayerMask pickUpLayerMask;

        private StarterAssetsInputs playerInputs;
        private ObjectGrabbable objectGrabbable;

        void Awake()
        {
            playerInputs = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            HandlePickup();
        }

        private void HandlePickup()
        {
            if (playerInputs.IsPickingup())
            {
                if (objectGrabbable == null)
                {
                    if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
                    {
                        if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                        {
                            objectGrabbable.Grab(objectGrabPointTransform);
                        }
                    }
                }
                else
                {
                    objectGrabbable.Drop();
                    objectGrabbable = null;
                }
            }
        }
    }
}
