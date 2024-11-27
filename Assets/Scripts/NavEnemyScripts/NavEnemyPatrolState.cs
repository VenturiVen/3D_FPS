using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavEnemyPatrolState : NavEnemyState
{
    NavEnemyChaseState chase;
    public override NavEnemyState Run()
    {
        return this;
    }

    public override NavEnemyState Run(float enemySpeed, bool isPlayerContact, int currentTarget)
    {
        // Code for finding the Player goes here
        return this;
    }
}
