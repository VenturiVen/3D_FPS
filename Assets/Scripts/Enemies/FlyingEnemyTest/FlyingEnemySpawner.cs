using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is a modified version of Evan Buggy's code just to spawn the enemies less frequently and add a limit to how
// many enemies there can be at once.

public class FlyingEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
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
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 20f && currentEnemyInstance == null)
        {
            float x = Random.Range(-5f, 5f) + transform.position.x;
            float z = Random.Range(-5f, 5f) + transform.position.z;
            float y = transform.position.y;

            currentEnemyInstance = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
            spawnTimer = 0f;

        }
    }
}