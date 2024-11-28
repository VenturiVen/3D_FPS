using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavEnemyWaitState : NavEnemyState
{
    private int timer = 100;
    public NavMeshAgent agent;
    public NavEnemyChaseState chase;
    public override NavEnemyState Run()
    {
        return this;
    }

    public override NavEnemyState Run(float enemySpeed, int contact)
    {
        this.enemySpeed = enemySpeed;
        this.contact = contact;

        agent.speed = this.enemySpeed;

        if (timer == 0)
        {
            Debug.Log("Agent: Going back to chasing the Player");
            this.enemySpeed = 4f;
            timer = 100;
            return chase;
        }

        timer--;

        return this;
    }
}