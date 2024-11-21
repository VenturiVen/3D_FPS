using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyJumpAttackState : EnemyState
{
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

        transform.parent.parent.position += (this.enemyDir * (this.enemySpeed * Time.deltaTime));

        if (this.isGrounded)
        {
            this.enemySpeed *= 3;
            
        }
        return this;
    }
}
