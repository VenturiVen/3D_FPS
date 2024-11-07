using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private GunData gunData;

    private float damageAmount;
    private Vector3 direction;
    public float velocity;
    private Vector3 initialPosition;
    public float maxDistance;
    public float DamageAmount { get; }

    // init
    public void Initialize(Vector3 dir, float dmg, float maxDist)
    {
        if (gunData == null)
        {
            Debug.LogError("gunData is not assigned in Projectiles.");
            return;
        }
        
        direction = dir.normalized;
        damageAmount = dmg;
        maxDistance = gunData.maxDistance;
        initialPosition = transform.position; // starting position

        // calculate velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * velocity;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) >=
            maxDistance) // ensure projectiles only have range of max distance
        {
            Destroy(gameObject);
        }
    }
    
    // need this for target.cs | that doesnt really like functioning well without this
    public float GetDamage()
    {
        return damageAmount;
    }

    
    // this function will genuinely kill me
    private void OnCollisionEnter(Collision collision) // delete projectile and damage on collision
    {
        IDamageble damageable = collision.transform.GetComponent<IDamageble>();
        if (damageable != null)
        {
            // Apply damage to the object
            damageable.Damage(damageAmount);
            Debug.Log($"{gameObject.name} collided with {collision.gameObject.name} for damage: {damageAmount}");
        }
        else
        {
            Debug.Log($"{gameObject.name} collided with {collision.gameObject.name} for no damage: {damageAmount}");
        }

        // Destroy the projectile after collision
        Destroy(gameObject);
    }


}