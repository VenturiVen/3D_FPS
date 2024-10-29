using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Enemy : MonoBehaviour
{
    EnemyState current;

    // Update is called once per frame
    void Update()
    {
        EnemyState next = current?.Run();

        if (next != null)
        {
            current = next;
        }
    }
}
