using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitch : MonoBehaviour
{
    public Light lightSource1;
    public Light lightSource2;
    public Light lightSource3;
    public Transform playerCamera; 
    public float interactionDistance = 7.0f;

    private bool check_switch = false;
    public Transform light_switch;

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera not assigned.");
        }
        if (light_switch == null)
        {
            Debug.LogError("Light switch not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("boolean = " + check_switch);
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.position;
        Vector3 rayDirection = playerCamera.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        {
            Debug.DrawRay(rayOrigin, rayDirection * interactionDistance, Color.red, 0.1f);

            if (hit.transform == light_switch)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    if (check_switch)
                    {
                        switch_on();
                        check_switch = !check_switch;
                    }
                    else {
                        switch_off();
                        check_switch = !check_switch;
                    }
                }
            }
        }

    }


    void switch_off()
    {
        lightSource1.enabled = false;
        lightSource2.enabled = false;
        lightSource3.enabled = false;
        light_switch.Rotate(new Vector3(0, 0, 180));
    }
    void switch_on()
    {
        lightSource1.enabled = true;
        lightSource2.enabled = true;
        lightSource3.enabled = true;
        light_switch.Rotate(new Vector3(0, 0, 180));
    }
}
