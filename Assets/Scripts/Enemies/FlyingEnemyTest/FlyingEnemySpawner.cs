using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is a modified version of Evan Buggy's code just to spawn the enemies less frequently and add a limit to how
// many enemies there can be at once.

public class FlyingEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Vector3 spawnPos = Vector3.zero;
    private bool playerIsInRange = false;
    private float spawnTimer = 0f;
    private GameObject currentEnemyInstance = null;


    void Start()
    {
        float x = Random.Range(-5f, 5f) + transform.position.x;
        float z = Random.Range(-5f, 5f) + transform.position.z;
        float y = transform.position.y;
        currentEnemyInstance = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }

    void Update()
    {
        if (!playerIsInRange) return;
        spawnTimer += Time.deltaTime;
        if (currentEnemyInstance == null)
        {
            {
                if (spawnTimer >= 20f)
                {
                    float x = Random.Range(-5f, 5f) + transform.position.x;
                    float z = Random.Range(-5f, 5f) + transform.position.z;
                    float y = transform.position.y;

                    currentEnemyInstance = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);

                    spawnTimer = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a projectile
        if (other.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = false;
            spawnTimer = 0;
        }
    }
}