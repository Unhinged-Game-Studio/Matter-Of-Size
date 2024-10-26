using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public float pickupDistance = 2f;
    public Transform playerTransform;
    public Transform holdTransform;
    public string pickupTag = "Pickup";
    public float holdForce = 2f; // The force applied to the held item
    public float minHoldDistance = 1.7f; // Minimum distance between player and held item
    public float maxHoldDistance = 2.2f; // Maximum distance between player and held item
    public float scrollSensitivity = 3f; // Sensitivity for mouse scroll wheel
    public float launchForce = 10f; // The force applied to launch the item
    public KeyCode launchKey = KeyCode.G; // The key to launch the item

    private bool isHeld = false;
    private bool isTouchingOtherRigidbody = false;

    public FirstPersonController fps; // Reference to the player controller
    public Collider playerCollider;

    private Rigidbody heldItemRb;
    private float currentHoldDistance;

    private void Start()
    {
        currentHoldDistance = minHoldDistance; // Initialize distance
    }

    private void Update()
    {
        // Handle pickup and drop actions
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isHeld)
                DropItem();
            else
                TryPickup();
        }

        // Adjust hold distance with mouse scroll wheel
        if (isHeld)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                AdjustHoldDistance(scroll);
            }

            // Launch the item if the launch key is pressed
            if (Input.GetKeyDown(launchKey))
            {
                LaunchItem();
            }

            // Continue holding the item
            HoldItem();
        }
    }

    private void TryPickup()
    {
        RaycastHit hit;
        Vector3 rayOrigin = playerTransform.position;
        Vector3 rayDirection = playerTransform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, pickupDistance))
        {
            if (hit.collider.CompareTag(pickupTag))
                PickupItem(hit.collider.gameObject);
        }
    }

    private void PickupItem(GameObject item)
    {
        isHeld = true;
        heldItemRb = item.GetComponent<Rigidbody>();

        if (heldItemRb != null)
        {
            heldItemRb.useGravity = false;
            heldItemRb.drag = 5f;
            heldItemRb.angularDrag = 10f;
            heldItemRb.angularVelocity = Vector3.zero;

            item.transform.SetParent(holdTransform);

            Collider itemCollider = item.GetComponent<Collider>();
            if (itemCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(itemCollider, playerCollider, true);
            }

            itemCollider = item.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.gameObject.AddComponent<PickupItemCollisionDetector>().Initialize(this);
            }
        }
    }

    private void DropItem()
    {
        if (heldItemRb != null)
        {
            isHeld = false;
            Transform itemTransform = holdTransform.GetChild(0);
            itemTransform.SetParent(null);
            heldItemRb.useGravity = true;
            heldItemRb.drag = 0f;
            heldItemRb.angularDrag = 0f;

            Collider itemCollider = itemTransform.GetComponent<Collider>();
            if (itemCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(itemCollider, playerCollider, false);
            }

            var collisionDetector = itemTransform.GetComponent<PickupItemCollisionDetector>();
            if (collisionDetector != null)
            {
                Destroy(collisionDetector);
            }

        }
    }

    private void HoldItem()
    {
        if (heldItemRb != null && !isTouchingOtherRigidbody)
        {
            float playerScale = fps.transform.localScale.x;

            Vector3 targetPosition = playerTransform.position + playerTransform.forward * (currentHoldDistance * playerScale);
            Vector3 force = (targetPosition - heldItemRb.position) * holdForce;
            heldItemRb.AddForce(force);
        }
    }

    private void LaunchItem()
    {
        if (heldItemRb != null)
        {
            Vector3 launchDirection = playerTransform.forward;
            DropItem();

            Debug.Log($"Launching item with force: {launchDirection * launchForce}");
            heldItemRb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            heldItemRb = null;
        }
    }

    private void AdjustHoldDistance(float scroll)
    {
        currentHoldDistance -= scroll * scrollSensitivity;
        currentHoldDistance = Mathf.Clamp(currentHoldDistance, minHoldDistance, maxHoldDistance);
    }

    public void SetTouchingOtherRigidbody(bool touching)
    {
        isTouchingOtherRigidbody = touching;
    }
}

public class PickupItemCollisionDetector : MonoBehaviour
{
    private PickupSystem pickupSystem;

    public void Initialize(PickupSystem pickupSystem)
    {
        this.pickupSystem = pickupSystem;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            pickupSystem.SetTouchingOtherRigidbody(true);
            Debug.Log("started touching another body");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            pickupSystem.SetTouchingOtherRigidbody(false);
            Debug.Log("stopped touching another object");
        }
    }
}
