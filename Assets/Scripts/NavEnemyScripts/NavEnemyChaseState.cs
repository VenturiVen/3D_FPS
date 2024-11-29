using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class NavEnemyChaseState : NavEnemyState
{
    public NavMeshAgent agent;
    public NavEnemyPatrolState patrol;
    public NavEnemyWaitState wait;
    public Transform player;
    public GameObject jello;

    private void Start()
    {
        player = GameObject.Find("Player_PSX").transform;
        jello = GameObject.Find("Jello");
    }

    public override NavEnemyState Run()
    {
        return this;
    }

    public override NavEnemyState Run(float enemySpeed, int contact)
    {
        this.enemySpeed = enemySpeed;
        this.contact = contact;

        agent.speed = this.enemySpeed;
        agent.destination = player.position;

        if (Vector3.Distance(transform.position, player.position) > 20f)
        {
            // Returns on the path to the node it was previously travelling towards
            Debug.Log("Agent: No longer chasing the player, back on patrol");
            this.enemySpeed = 2f;
            jello.transform.position -= new Vector3(0f, 1f, 0f);
            return patrol;
        }

        if (contact == 2)
        {
            PlayerStats.Instance.TakeDamage(50, transform.position);
            this.enemySpeed = 0f;
            jello.transform.position -= new Vector3(0f, 1f, 0f);
            return wait;
        }
        return this;
    }
}
