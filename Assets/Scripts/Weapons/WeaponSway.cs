using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was made with the help of this YouTube video:
// https://www.youtube.com/watch?v=QIVN-T-1QBE

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")] 
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    void FixedUpdate()
    {
        // get rawinput
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        // calculate rotation

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        
        Quaternion rotation = rotationX * rotationY;
        
        // rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, smooth * Time.deltaTime);
        
    }
}
