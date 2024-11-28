using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnvyRetreatState : EnvyState
{ 
    // return to switch to the specified state
    public EnvyChaseState chase;
    public EnvyDespawnState despawn;

    public EnvySpawnState spawn;

    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact, bool isPlayerSight)
    {
        // assigning variables
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;

        if (!isPlayerContact)
        {
            transform.parent.parent.position =
            Vector3.MoveTowards(transform.position, spawn.getSpawnLocation(),
            enemySpeed * Time.deltaTime);
        }

        if (isPlayerContact)
        {
            if (isPlayerSight)
            {
                Debug.Log("Chase State");
                return chase;
            }
        }

        // Vector3.Distance(target, gameObject.transform.position);

        if ((Vector3.Distance(spawn.getSpawnLocation(), gameObject.transform.position) == 0) )
        {
            Debug.Log("Despawn State");
            return despawn;
        }
        
        return this;
    }
}
