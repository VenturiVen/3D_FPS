using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;
using UnityEngine.AI;

public class EnvyChaseState : EnvyState
{
    // return to switch to the specified state
    public EnvyIdleState idle;
    public EnvyAttackState attack;
    public EnvyJumpAttackState jumpAttack;

    // desired differences between Envy and player in order to switch to either attack or jumpattack states
    public float distToAttack = 5f;
    public float distoJumpAttack = 5f;
    // target is the player's current location
    Vector3 target = Vector3.zero;
    // distance is the difference between the player and Envy's current location
    float distance = 0f;

    //NavMesh
    public GameObject Envy;
    public Transform player;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = Envy.GetComponent<NavMeshAgent>();
    }

    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact, bool isPlayerSight)
    {
        //Debug.Log("Chasing...");
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;

        // target equals player's current position
        // target = PlayerStats.Instance.currentPos;
        // distance equals player's current position minus Envy's current position
        distance = Vector3.Distance(player.position, gameObject.transform.position);

        if (!isPlayerContact) 
        {
            Debug.Log("Idle State");
            return idle;
        }

        if (distance <= distToAttack)
        {
            Debug.Log("Attack State");
            return attack;
        }
        /*
        if (distance <= distoJumpAttack) 
        {
            Debug.Log("Jump Attack State");
            return jumpAttack;
        }
        */
        
        navMeshAgent.destination = player.position;


        // target.y = 0f;
        transform.parent.parent.LookAt(player.position);

        return this;
    }
}
