using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyAttackState : EnvyState
{
    // return to switch to the specified state
    public EnvyIdleState idle;
    public EnvyChaseState chase;

    // bool to track if player has been hit
    private bool playerHit = false;

    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact)
    {
        // assigning variables
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;

        // #TODO: right now this is bugged and kills you instantly !!!!
        if (!playerHit) 
        {
            Debug.Log("Player Hit");
            PlayerStats.Instance.TakeDamage(20, transform.position);
            playerHit = true;
        }
        else
        {
            playerHit = false;
            Debug.Log("Chase State");
            return chase;
        }
        
        return this;
    }
}
