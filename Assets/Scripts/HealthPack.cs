using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{

    private PlayerStats playerStats;

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0); //rotates 50 degrees per second around y axis
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (playerStats == null)
            {
                playerStats = collision.gameObject.GetComponent<PlayerStats>();
            }

            playerStats.Heal(100);
            Destroy(gameObject);
        }
    }
}
