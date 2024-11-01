using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyIdleState idle;
    public override EnemyState Run()
    {
        return this;
    }

    public override EnemyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded)
    {
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        transform.parent.parent.position += (this.enemyDir * (this.enemySpeed * Time.deltaTime));
        //Debug.Log("Currently Attacking!");
        //Debug.Log(enemySpeed);

        return this;
    }
}
