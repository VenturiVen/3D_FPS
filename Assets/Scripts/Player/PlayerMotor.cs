using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // PlayerStats.Instance.speedModifier
    // PlayerStats.Instance.jumpStrength

    private CharacterController charController;
    public float speed = 0f;
    private Vector3 vel = Vector3.zero;
    private Vector3 gravityVec = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;
    //private float lastYrot = 0f;
    public float jumpStrength = 3f;

    // gravity
    private bool isGrounded;
    public float gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //isGrounded = charController.isGrounded;
        //if ((transform.eulerAngles.y > lastYrot - 2 && transform.eulerAngles.y < lastYrot)
        //    || (transform.eulerAngles.y < lastYrot + 2 && transform.eulerAngles.y > lastYrot))
        //{
        //    Debug.Log("Strafe Angle met");
        //}
        //lastYrot = transform.eulerAngles.y;
    }

    // receives inputs from InputManager.cs and applies them to charController
    public void ProcessMove(Vector2 input)
    {
        // If the player is pressing any movement keys (WASD)
        if (input != Vector2.zero) {
            // Acceleration: Speed increases as long as the player is moving and vice versa
            speed = (Mathf.Clamp(speed += 0.5f, 0f, 6f));
            moveDir.x = tweenMoveDir(moveDir.x, input.x);
            moveDir.z = tweenMoveDir(moveDir.z, input.y);
        }
        else {
            speed = (Mathf.Clamp(speed -= 0.5f, 0f, 6f));

            // Tween the moveDir to 0 when the player slows down enough.
            // This prevents "snapback" moments if the player were to start
            // moving in the opposite direction. moveDir can't be reset to
            // 0 upon the player stopping as it is needed for deceleration.
            if (speed <= 1f) {
                moveDir.x = tweenMoveDir(moveDir.x, 0f);
                moveDir.z = tweenMoveDir(moveDir.z, 0f);
            }
        }

        vel = transform.TransformDirection(moveDir) * speed * Time.deltaTime;
        charController.Move(vel);

        // Using a separate Vec3 for gravity and jumping and another charController.Move
        gravityVec.y += gravity * Time.deltaTime;
        if (isGrounded && gravityVec.y < 0) {
            gravityVec.y = -2f;
        }
        charController.Move(gravityVec * Time.deltaTime);
    }

    // This function is to smooth digital (e.g. WASD movement).
    // Rather than just making the player snap to the input vector, this function
    // tweens the moveDir vector to the input vector to make it smoother to move around.
    public float tweenMoveDir(float move, float input)
    {
        if (move != input) {
            if (move < input) {
                move = Mathf.Clamp(move + 0.2f, -1f, input);
            }
            else {
                move = Mathf.Clamp(move - 0.2f, input, 1f);
            }
        }
        return move;
    }

    public void Jump()
    {
        if (isGrounded)  {
            gravityVec.y = PlayerStats.Instance.jumpStrength;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
