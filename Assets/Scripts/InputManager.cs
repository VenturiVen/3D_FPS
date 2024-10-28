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

        // TODO: this does not work
        // jump
        // ctx = call back context
        // when grounded jump is performed, ctx is used to call jump in PlayerMotor (motor)
        grounded.Jump.performed += ctx => motor.Jump();
    }

    // change FixedUpdate to Update if stutters happen
    void Update()
    {
        motor.ProcessMove(grounded.Movement.ReadValue<Vector2>());
    }

    // called after all Update functions have been called
    private void LateUpdate()
    {
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
