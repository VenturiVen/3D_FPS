using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // PlayerStats.Instance.speedModifier
    // PlayerStats.Instance.jumpStrength

    private Rigidbody character;
    public float speed = 0f;
    private Vector3 vel = Vector3.zero;
    private Vector3 gravityVec = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;
    //private float lastYrot = 0f;

    // gravity
    private bool isGrounded;
    RaycastHit hit;
    public float gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if ((transform.eulerAngles.y > lastYrot - 2 && transform.eulerAngles.y < lastYrot)
        //    || (transform.eulerAngles.y < lastYrot + 2 && transform.eulerAngles.y > lastYrot))
        //{
        //    Debug.Log("Strafe Angle met");
        //}
        //lastYrot = transform.eulerAngles.y;
        if (Physics.SphereCast(transform.position, 0.2f, -transform.up, out hit, 1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }
        PlayerStats.Instance.currentPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position - transform.up * 1, 0.2f);
    }

    // receives inputs from InputManager.cs and applies them to charController
    public void ProcessMove(Vector2 input)
    {
        // If the player is pressing any movement keys (WASD)
        if (input != Vector2.zero) {
            // Acceleration: Speed increases as long as the player is moving and vice versa
            speed = (Mathf.Clamp(speed += 0.5f, 0f, PlayerStats.Instance.speedModifier));
            moveDir.x = tweenMoveDir(moveDir.x, input.x);
            moveDir.z = tweenMoveDir(moveDir.z, input.y);
        }
        else {
            speed = (Mathf.Clamp(speed -= 0.5f, 0f, PlayerStats.Instance.speedModifier));

            // Tween the moveDir to 0 when the player slows down enough.
            // This prevents "snapback" moments if the player were to start
            // moving in the opposite direction. moveDir can't be reset to
            // 0 upon the player stopping as it is needed for deceleration.
            if (speed <= 1f) {
                moveDir.x = tweenMoveDir(moveDir.x, 0f);
                moveDir.z = tweenMoveDir(moveDir.z, 0f);
            }
        }

        vel = transform.TransformDirection(moveDir) * speed;
        character.MovePosition(transform.position + vel * Time.deltaTime);
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
            character.AddForce(transform.up * PlayerStats.Instance.jumpStrength, ForceMode.Impulse);
        }
    }
}
