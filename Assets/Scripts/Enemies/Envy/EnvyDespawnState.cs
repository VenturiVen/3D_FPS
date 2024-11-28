using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyDespawnState : EnvyState
{

    // despawn state should only return "this" or destroy object (not to be confused with the enemy being killed).

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

        // #TODO: play despawning animation

        Debug.Log("Envy Despawned");
        DespawnEnvy();

        return this;
    }

    public void DespawnEnvy()
    {
        Destroy(transform.parent.parent.gameObject);
    }
}
