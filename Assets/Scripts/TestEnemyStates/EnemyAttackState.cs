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
}
