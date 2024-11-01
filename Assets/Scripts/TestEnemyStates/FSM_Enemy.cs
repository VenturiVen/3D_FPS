using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Enemy : MonoBehaviour
{
    public EnemyState current;
    public Vector3 newDir = new Vector3(0f, 0f, 0f);
    public float newSpeed = 0f;
    public bool newIsGrounded = false;

    void FixedUpdate()
    {
        // The plan is that once a state is called, its public velocity and
        // speed variables are used as parameters for instantiating the next
        // state. E.g. An enemy transitions between a chase state to a jump
        // state we can bring the velocity from the chase state to the jump state.
        // This is a state transition
        EnemyState next = current?.Run(newDir, newSpeed, newIsGrounded);

        if (next != null)
        {
            newIsGrounded = current.isGrounded;
            newSpeed = current.enemySpeed;
            //Debug.Log(current.enemySpeed);
            newDir = current.enemyDir;
            current = next;
        }
    }

    // Below code is for detecting if enemy is grounded
    // Commented out for now so I don't forget it. - Evan

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            newIsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            newIsGrounded = false;
        }
    }
}
