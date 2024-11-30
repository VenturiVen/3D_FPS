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

    //// target is the player's current location
    //Vector3 target = Vector3.zero;
    //// distance is the difference between the player and Envy's current location
    //float distance = 0f;

    //NavMesh
    public GameObject Envy;
    public GameObject player;
    private NavMeshAgent navMeshAgent;

    // variables for coroutine
    Coroutine coroutine;
    private bool countdownStarted = false;
    private bool countdownFinished = false;
    [SerializeField] private int idleDelay = 3;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

        // NO LONGER NEEDED!
        // target equals player's current position
        // target = PlayerStats.Instance.currentPos;
        // distance equals player's current position minus Envy's current position
        // distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (isPlayerContact)
        {
            Debug.Log("Attack State");
            return attack;
        }

        if (!isPlayerSight)
        {
            if (!countdownStarted)
            {
                countdownStarted = true;
                coroutine = StartCoroutine(IdleDelay(idleDelay));
            } else if (countdownFinished)
            {
                countdownStarted = false;
                countdownFinished = false;
                Debug.Log("Idle State");
                return idle;
            }
        }

        navMeshAgent.destination = player.transform.position;

        transform.parent.parent.LookAt(player.transform.position);

        return this;
    }

    IEnumerator IdleDelay(int idleDelay)
    {
        //Debug.Log("Countdown started.");
        yield return new WaitForSecondsRealtime(idleDelay);
        countdownFinished = true;
    }
}
