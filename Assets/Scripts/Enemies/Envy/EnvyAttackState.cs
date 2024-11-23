using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyAttackState : EnvyState
{
    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact)
    {

        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;

        transform.parent.parent.position += (this.enemyDir * (this.enemySpeed * Time.deltaTime));

        if (this.isGrounded)
        {
            this.enemySpeed *= 3;
            
        }
        return this;
    }
}
