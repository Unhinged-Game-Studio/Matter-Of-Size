using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoor : MonoBehaviour
{
    public LayerMask baseLayer;
    public key real_key;
    // Start is called before the first frame update
    void Start()
    {
        if (baseLayer == 0)
            Debug.Log("Layer not assined");       
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    private void OnCollisionEnter(Collision other) {

        if (IsInLayerMask(other.gameObject, baseLayer))
        {
            AttachToKeyHole(other.transform);
            real_key.is_in_keyhole = true;
            this.tag = "Untagged";
            
        }
    }

    private void AttachToKeyHole(Transform baseTransform)
    {
        transform.position = baseTransform.position;
        transform.rotation = baseTransform.rotation;
        transform.SetParent(baseTransform);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask & (1 << obj.layer)) != 0;
    }
}
