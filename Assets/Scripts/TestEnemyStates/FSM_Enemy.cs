using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Enemy : MonoBehaviour
{
    public EnemyState current;

    void FixedUpdate()
    {
        // The plan is that once a state is called, its public velocity and
        // speed variables are used as parameters for instantiating the next
        // state. E.g. An enemy transitions between a chase state to a jump
        // state we can bring the velocity from the chase state to the jump state.
        // This is a state transition
        EnemyState next = current?.Run();

        if (next != null)
        {
            current = next;
        }
    }

    // Below code is for detecting if enemy is grounded
    // Commented out for now so I don't forget it. - Evan

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("Ground"))
    //    {
    //        Debug.Log("FSM_Enemy is grounded");
    //    }
    //}
}
