using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject agent;
    private Vector3 spawnPos = Vector3.zero;
    private bool agentSpawned = false;
    public int starterTimer = 300;
    public int keep = 0;
    public Transform player;

    void Update()
    {
        if ((Vector3.Distance(transform.position, player.position)) < 15f && starterTimer == 0)
        {
            float x = Random.Range(-5f, 5f) + transform.position.x;
            float z = Random.Range(-5f, 5f) + transform.position.z;
            float y = transform.position.y;

            if (PlayerStats.Instance.currentScore > 2000 && agentSpawned == false)
            {
                if (Random.Range(0, 1f) > 0.5f)
                {
                    Instantiate(agent, new Vector3(x, y, z), Quaternion.identity);
                    agentSpawned = true;
                }
                else
                {
                    Instantiate(zombie, new Vector3(x, y, z), Quaternion.identity);
                }
            }
            else
            {
                Instantiate(zombie, new Vector3(x, y, z), Quaternion.identity);
            }
            starterTimer = PlayerStats.Instance.timer - keep;

            if (keep < 250)
            {
                keep += 50;
            }
        }

        if (starterTimer > 0)
        {
            starterTimer--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 15f);
    }
}