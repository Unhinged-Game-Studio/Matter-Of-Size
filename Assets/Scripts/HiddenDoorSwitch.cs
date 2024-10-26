using UnityEngine;

public class LeverCollision : MonoBehaviour
{

    // void Update()
    // {
    //     Debug.Log(tag);
    // }
    // Define the layer for the base
    public LayerMask baseLayer;
    public lever lever_s;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the layer of the collided object matches the baseLayer
        if (IsInLayerMask(collision.gameObject, baseLayer))
        {
            AttachToBase(collision.transform);
            lever_s.isattached = true;
            this.tag = ("Untagged");
        }
    }

    private void AttachToBase(Transform baseTransform)
    {
        // Set the position and rotation of the lever to match the base
        transform.position = baseTransform.position;
        transform.rotation = baseTransform.rotation;

        // transform.localScale = Vector3.one;


        // Optionally, make the lever a child of the base so it moves with it
        transform.SetParent(baseTransform);
        
        // Disable physics interaction if needed
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    // Helper function to check if the object is in the specified layer mask
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask & (1 << obj.layer)) != 0;
    }
}
