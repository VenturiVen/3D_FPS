using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageble
{
    private float health = 100f;
    private PlayerStats playerStats;

    [SerializeField] private ParticleSystem killParticles;
    private ParticleSystem killParticlesInstance;

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");

        if (health <= 0)
        {
            SpawnDamageParticles();
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} has been destroyed.");
        }
    }

    private void SpawnDamageParticles()
    {
            // spawn kill particles at current transformation
            killParticlesInstance = Instantiate(killParticles, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            if (playerStats == null)
            {
                playerStats = other.GetComponent<PlayerStats>();
            }

            playerStats?.TakeDamage(50);
            Debug.Log("Player took damage from Target.");
        }
        
        // Check if the colliding object is a projectile
        if (other.CompareTag("Projectile"))
        {
            // Get the projectile's damage amount and apply it to the target
            Projectiles projectile = other.GetComponent<Projectiles>();
            if (projectile != null)
            {
                Damage(projectile.GetDamage());
                Destroy(other.gameObject); // Destroy the projectile on collision
            }
        }
    }
}