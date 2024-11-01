using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyAttackState attack;
    public override EnemyState Run()
    {
        transform.parent.parent.position += enemyDir * enemySpeed * Time.deltaTime;
        return this;
    }
}
