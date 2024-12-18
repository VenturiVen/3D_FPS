using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Enemy : MonoBehaviour
{
    public EnemyState current;
    public Vector3 newDir = new Vector3(0f, 0f, 0f);
    public float newSpeed = 0f;
    public bool newIsGrounded = false;
    public bool newContact = false;
    RaycastHit hit;

    void FixedUpdate()
    {
        // The plan is that once a state is called, its public velocity and
        // speed variables are used as parameters for instantiating the next
        // state. E.g. An enemy transitions between a chase state to a jump
        // state we can bring the velocity from the chase state to the jump state.
        // This is a state transition
        EnemyState next = current?.Run(newDir, newSpeed, newIsGrounded, newContact);

        if (next != null)
        {
            newIsGrounded = current.isGrounded;
            newSpeed = current.enemySpeed;
            newDir = current.enemyDir;
            newContact = current.isContact;
            current = next;
        }

        if (Physics.SphereCast(transform.position, 0.2f, -transform.up, out hit, 1f))
        {
            newIsGrounded = true;
        }
        else
        {
            newIsGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position - transform.up * 1, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            newContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            newContact = false;
        }
    }
}
