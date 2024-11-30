using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnvySpawner : MonoBehaviour
{
    [SerializeField] private GameObject envy;
    [SerializeField] private GameObject spawnPt;

    // to limit number of Envys that are spawned
    [SerializeField] private int spawnLimit = 0;
    private int envyNum;

    private void Start()
    {
        envyNum = spawnLimit;
    }

    private void SpawnEnvy()
    {
            envyNum--;
            Instantiate(envy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (envyNum > 0)
            {
                SpawnEnvy();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (envyNum > 0)
            {
                SpawnEnvy();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        envyNum++;
    }
}
