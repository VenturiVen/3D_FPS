using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;

public class EnvyChaseState : EnvyState
{
    // return to switch to the specified state
    public EnvyIdleState idle;
    public EnvyAttackState attack;
    public EnvyJumpAttackState jumpAttack;

    Vector3 target = Vector3.zero;

    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact)
    {
        //Debug.Log("Chasing...");
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;

        if (!isPlayerContact) 
        {
            return idle;
        }

        transform.parent.parent.position =
            Vector3.MoveTowards(transform.position, PlayerStats.Instance.currentPos,
            enemySpeed * Time.deltaTime);

        target = PlayerStats.Instance.currentPos;
        target.y = 0f;
        transform.parent.parent.LookAt(PlayerStats.Instance.currentPos);

        return this;
    }
}
