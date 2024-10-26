using UnityEngine;

public class headchecker : MonoBehaviour
{
    public FirstPersonController fps; // Reference to the FirstPersonController
    public Transform headTransform;   // Reference to the Transform of the head (or a point above the head)
    public float raycastDistance = 3f; // Distance of the raycast

    private bool isScalingEnabled = true; // Keep track of current scaling state

    void Start()
    {
        if (fps == null)
        {
            Debug.LogError("FPS reference is not assigned in the Inspector.");
        }
        if (headTransform == null)
        {
            Debug.LogError("Head Transform reference is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Perform the raycast and check for collisions
        RaycastHit hit;
        Vector3 rayDirection = Vector3.up; // Raycast direction is downward

        // Check if the raycast hits something
        if (Physics.Raycast(headTransform.position, rayDirection, out hit, raycastDistance))
        {
            // If ray hits something and scaling is enabled, disable scaling
            if (isScalingEnabled)
            {
                fps.enableScale = false;
                isScalingEnabled = false;
                Debug.Log("Scaling disabled: Hit " + hit.collider.name);
            }
        }
        else
        {
            // If ray does not hit anything and scaling is disabled, enable scaling
            if (!isScalingEnabled)
            {
                fps.enableScale = true;
                isScalingEnabled = true;
                Debug.Log("Scaling enabled: No hit");
            }
        }

        // Optionally, draw the ray in the editor for debugging
        Debug.DrawRay(headTransform.position, rayDirection * raycastDistance, Color.green);
    }
}
