using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavEnemy : MonoBehaviour
{
    public Transform goTo1;
    public Transform goTo2;
    public Transform goTo3;
    public Transform goTo4;
    public Transform player;
    private NavMeshAgent agent;
    private List<Transform> goToList = new List<Transform>();
    public int index = 0;
    RaycastHit hit;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goTo1 = GameObject.Find("goTo1").transform;
        goTo2 = GameObject.Find("goTo2").transform;
        goTo3 = GameObject.Find("goTo3").transform;
        goTo4 = GameObject.Find("goTo4").transform;
        player = GameObject.Find("Player_PSX").transform;

        goToList.Add(goTo1);
        goToList.Add(goTo2);
        goToList.Add(goTo3);
        goToList.Add(goTo4);
        goToList.Add(player);
    }

    private void FixedUpdate()
    {
        agent.destination = goToList[index].position;
        if (Vector3.Distance(transform.position, player.position) < 20f)
        {
            if (Physics.Linecast(transform.position, player.position, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    index = 4;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);
        Gizmos.DrawSphere(transform.position, 20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goTo"))
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
        }
        // This doesnt work atm: Going to fix tmw
        // The enemy just keeps going after the player since it is always gonna be in range
        else
        {
            if (other.CompareTag("Player"))
            {
                float distance = 0f;
                for (int i = 0; i < goToList.Count - 1; i++)
                {
                    float d = -Vector3.Distance(transform.position, goToList[i].position);
                    if (d < distance)
                    {
                        distance = d;
                        index = i;
                    }
                }
            }
        }
    }
}
