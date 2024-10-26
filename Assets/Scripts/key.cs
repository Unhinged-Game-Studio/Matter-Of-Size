using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    public Transform playerCamera; 
    public float interactionDistance = 7.0f;

    public bool is_in_keyhole = false;
    public Transform door_handle;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera not assigned.");
        }
        if (door_handle == null)
        {
            Debug.LogError("Light switch not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.position;
        Vector3 rayDirection = playerCamera.forward;
        
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        {
            // Debug.Log(hit.transform);
            if (hit.transform == door_handle)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    if (is_in_keyhole)
                    {
                        animator.enabled = true;
                    }
                }
            }
        }
    }
}
