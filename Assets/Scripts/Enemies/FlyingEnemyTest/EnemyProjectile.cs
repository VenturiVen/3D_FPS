using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float lifespan = 5f;
    [SerializeField] private float projectileSpeed = 10f;

    private Transform target;
    private bool isHoming = false;

    private void Start()
    {
        // Destroy the projectile after lifespan runs out.
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        if (isHoming && target != null)
        {
            // Moves the projectile to target.
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * projectileSpeed * Time.deltaTime, Space.World);
            
            // Rotates projectile towards target
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * projectileSpeed);
        }
        else
        {
            // If not homing, shoot forward as per normal.
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Apply damage.
            PlayerStats playerStats = collision.collider.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damageAmount, Vector3.zero);
            }
            
            // Destroy the projectile after hitting the player.
            Destroy(gameObject);
        }
        else
        {
            // Destroy the projectile on collision.
            Destroy(gameObject);
        }
    }

    // Method to set the target to home on.
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isHoming = true;
    }
}   