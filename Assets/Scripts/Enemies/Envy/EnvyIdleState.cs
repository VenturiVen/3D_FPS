using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;

public class EnvyIdleState : EnvyState
{

    // idle state can switch to a lot of states !!

    // return to switch to the specified state
    public EnvyAttackState attack;
    public EnvyChaseState chase;
    public EnvyRetreatState retreat;
    public EnvyJumpAttackState jumpAttack;

    Vector3 target = Vector3.zero;

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

        if (isPlayerContact)
        {
            Debug.Log("Chase State");
            return chase;
        }
        /*
        if (isEnemyContact)
        {
            // #TODO for future steering use
            return this;
        }
        */
        
        //Debug.Log("Idling...");
        return this;
    }

    
}
