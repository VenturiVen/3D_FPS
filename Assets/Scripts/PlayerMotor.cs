using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController charController;
    public float speed = 0f;
    private Vector3 vel = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    // gravity
    private bool isGrounded;
    public float gravity = -9.8f;

    // jump
    public float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = charController.isGrounded;
    }

    // receives inputs from InputManager.cs and applies them to charController
    public void ProcessMove(Vector2 input)
    {
        // If the player is pressing any movement keys (WASD)
        if (input != Vector2.zero)
        {
            // Acceleration: Speed increases as long as the player is moving and vice versa
            speed = Mathf.Clamp(speed += 0.5f, 0f, 6f);
            moveDir.x = tweenMoveDir(moveDir.x, input.x);
            moveDir.z = tweenMoveDir(moveDir.z, input.y);
        }
        else
        {
            speed = Mathf.Clamp(speed -= 0.5f, 0f, 6f);
            // Tween the moveDir to 0 when the player slows down enough.
            // This prevents "snapback" moments if the player were to start
            // moving in the opposite direction. moveDir can't be reset to
            // 0 upon the player stopping as it is needed for deceleration.
            if (speed <= 1f)
            {
                moveDir.x = tweenMoveDir(moveDir.x, 0f);
                moveDir.z = tweenMoveDir(moveDir.z, 0f);
            }
        }

        vel = transform.TransformDirection(moveDir) * speed * Time.deltaTime;

        // gravity
        vel.y += gravity * Time.deltaTime;

        // prevents downward force on player from continuously increasing while grounded
        if (isGrounded && vel.y < 0)
            vel.y = -0.3f;
        charController.Move(vel);

    }

    // This function is to smooth digital (e.g. WASD movement).
    // Rather than just making the player snap to the input vector, this function
    // tweens the moveDir vector to the input vector to make it smoother to move around.
    public float tweenMoveDir(float move, float input)
    {
        if (move != input)
        {
            if (move < input)
            {
                move = Mathf.Clamp(move + 0.2f, -1f, input);
            }
            else
            {
                move = Mathf.Clamp(move - 0.2f, input, 1f);
            }
        }
        return move;
    }

    public void Jump()
    {
        if (isGrounded) 
        {
            // TODO: I assume Evan wants to do this
            // imo the jump doesn't need to be too crazy bc im thinking of COD: WaW
            // but knowing Evan he'll do some other shit !!
            // something similar to Half Life's jump would be cool I think..
            // - Leo

            // this is placheolder code (that does not work)
            vel.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
