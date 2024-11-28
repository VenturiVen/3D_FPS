using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class NavEnemyPatrolState : NavEnemyState
{
    public NavMeshAgent agent;
    public NavEnemyChaseState chase;
    public Transform goTo1;
    public Transform goTo2;
    public Transform goTo3;
    public Transform goTo4;
    public Transform player;
    private List<Transform> goToList = new List<Transform>();
    private int index = 0;
    public int lastContact = 0;
    RaycastHit hit;

    private void Start()
    {
        goTo1 = GameObject.Find("goTo1").transform;
        goTo2 = GameObject.Find("goTo2").transform;
        goTo3 = GameObject.Find("goTo3").transform;
        goTo4 = GameObject.Find("goTo4").transform;
        player = GameObject.Find("Player_PSX").transform;

        goToList.Add(goTo1);
        goToList.Add(goTo2);
        goToList.Add(goTo3);
        goToList.Add(goTo4);
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
        agent.destination = goToList[index].position;

        if (contact == 1 && lastContact == 0)
        {
            if (index == 3)
            {
                index = 0;
            }
            else
            {
                if (index < 3)
                {
                    index++;
                }
            }
            Debug.Log("Agent: Now going to goTo" + (index + 1));
        }

        if (Vector3.Distance(transform.position, player.position) < 20f)
        {
            if (Physics.Linecast(transform.position, player.position, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Now chasing the player");
                    this.enemySpeed = 4f;
                    return chase;
                }
            }
        }

        lastContact = contact;

        return this;
    }
}
