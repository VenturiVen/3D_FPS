using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageble
{
    private float health = 100f;
    private PlayerStats playerStats;
    private AudioSource sound;

    [SerializeField] private ParticleSystem killParticles;
    private ParticleSystem killParticlesInstance;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        sound.Play();
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");

        if (health <= 0)
        {
            PlayerStats.Instance.IncreaseScore();
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

        if (other.CompareTag("DeathPlane"))
        {
            Debug.Log("Dying!");
            Destroy(gameObject);
        }
    }
}