using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.GroundedActions grounded;

    private PlayerMotor motor;

    private PlayerLook look;

    // Unity calls Awake when an enabled script instance is being loaded
    void Awake()
    {
        playerInput = new PlayerInput();
        grounded = playerInput.Grounded;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // jump
        // ctx = call back context
        // when grounded jump is performed, ctx is used to call jump in PlayerMotor (motor)
        grounded.Jump.performed += ctx => motor.Jump();
    }

    // do not change this to Update(), this did not fix stutters
    void FixedUpdate()
    {
        motor.ProcessMove(grounded.Movement.ReadValue<Vector2>());
        look.ProcessLook(grounded.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        grounded.Enable();
    }

    private void OnDisable()
    {
        grounded.Disable();
    }
}
