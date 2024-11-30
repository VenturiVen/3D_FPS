using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Vector3 spawnPos = Vector3.zero;
    private bool playerIsInRange = false;
    public int starterTimer = 300;
    public int keep = 0;
    void Update()
    {
        if (playerIsInRange && starterTimer == 0)
        {
            float x = Random.Range(-5f, 5f) + transform.position.x;
            float z = Random.Range(-5f, 5f) + transform.position.z;
            float y = transform.position.y;
            Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
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

<<<<<<< Updated upstream
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
            keep = 0;
        }
    }
=======
    //private void OnDrawGizmos()
    //{
     //   Gizmos.color = Color.yellow;
     //   Gizmos.DrawSphere(transform.position, 15f);
    //}
>>>>>>> Stashed changes
}