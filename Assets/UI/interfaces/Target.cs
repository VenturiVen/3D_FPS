using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageble
{
    private float health = 100f;
    private PlayerStats playerStats;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(playerStats == null){
                playerStats = collision.gameObject.GetComponent<PlayerStats>();
            }

            playerStats.TakeDamage(100);
        }
    }
}
