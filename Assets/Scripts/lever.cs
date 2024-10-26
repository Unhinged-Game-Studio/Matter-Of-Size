using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class lever : MonoBehaviour
{

    public Transform playerCamera; 
    public float interactionDistance = 7.0f;

    public bool isattached = false;
    public Transform levermesh;

    public Animator animator;

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera not assigned.");
        }
        if (levermesh == null)
        {
            Debug.LogError("Light switch not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("boolean = " + isattached);
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.position;
        Vector3 rayDirection = playerCamera.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        {
            Debug.DrawRay(rayOrigin, rayDirection * interactionDistance, Color.red, 0.1f);

            if (hit.transform == levermesh)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    if (isattached)
                    {
                        Debug.Log("should open door");
                        animator.enabled = true;
                    }
                }
            }
        }

    }

}
