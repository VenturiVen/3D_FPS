using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// had trouble understanding leos player input script, decided to watch this video to aid me.
// using this as a placeholder to test guns, we can implement this into leos script hopefully
// https://www.youtube.com/watch?v=qQLvcS9FxnY

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour

{

    public Camera playerCamera;
    public float idleSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = 10f;
    //yada yada yada

    public float sensitivity = 2f; 
    public float lookXLimit = 45f; // this one is interesting, prevents the camera from exceeding looking over 45 degrees to ensure you cant flip the camera!!

    Vector3 move = Vector3.zero; // 3d vector that holds positions
    float rotationX = 0; // angle of viewing of camera

    public bool canMove = true; // 

    CharacterController characterController; // make character controller

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        #region Handles Movement

        Vector3 forward = transform.TransformDirection(Vector3.forward); // z axis +
        Vector3 right = transform.TransformDirection(Vector3.right); // x axis +

       
        bool isRunning = Input.GetKey(KeyCode.LeftShift); 

        float SpeedX = canMove ? (isRunning ? sprintSpeed : idleSpeed) * Input.GetAxis("Vertical") : 0;
        float SpeedY = canMove ? (isRunning ? sprintSpeed : idleSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = move.y; // retain y position
        move = (forward * SpeedX) + (right * SpeedY);

        #endregion

        #region Handles Jumping

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) // jump function
        {
            move.y = jumpHeight;
        }
        else
        {
            move.y = movementDirectionY; // retain y position
        }

        if (!characterController.isGrounded) 
        {
            move.y -= gravity * Time.deltaTime;  // activate gravity when not grounded
        }

        #endregion

        #region Handles Rotation
        characterController.Move(move * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }

        #endregion
    }
}
