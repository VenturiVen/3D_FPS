using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyHitState hit;
    Vector3 target = Vector3.zero;
    public override EnemyState Run()
    {
        return this;
    }

    public override EnemyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isContact)
    {
        this.isContact = isContact;
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;

        transform.parent.parent.position =
            Vector3.MoveTowards(transform.position, PlayerStats.Instance.currentPos,
            enemySpeed * Time.deltaTime);

        target = PlayerStats.Instance.currentPos;
        target.y = 0f;
        transform.parent.parent.LookAt(PlayerStats.Instance.currentPos);
        //Debug.Log("Currently Attacking!");
        //Debug.Log(enemySpeed);

        if (isContact)
        {
            //Debug.Log("going to hit state");
            //Debug.Log(isGrounded);
            return hit;
        }

        return this;
    }
}
