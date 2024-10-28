using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.GroundedActions grounded;

    private PlayerMotor motor;

    // Unity calls Awake when an enabled script instance is being loaded
    void Awake()
    {
        playerInput = new PlayerInput();
        grounded = playerInput.Grounded;
        motor = GetComponent<PlayerMotor>();
    }

    // change FixedUpdate to Update if stutters happen
    void FixedUpdate()
    {
        motor.ProcessMove(grounded.Movement.ReadValue<Vector2>());
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
