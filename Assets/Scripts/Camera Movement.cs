using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed of rotation
    public float minAngle = -30.0f; // Minimum angle
    public float maxAngle = 30.0f; // Maximum angle

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Calculate the midpoint and range of the angles
            float midpoint = (minAngle + maxAngle) / 2.0f;
            float range = (maxAngle - minAngle) / 2.0f;

            // Calculate the new rotation angle using a sine wave
            float currentRotationAngle = midpoint + Mathf.Sin(Time.time * rotationSpeed) * range;
            
            // Apply the rotation to the camera's transform
            transform.rotation = Quaternion.Euler(16, currentRotationAngle, 0);
        }
    }
}
