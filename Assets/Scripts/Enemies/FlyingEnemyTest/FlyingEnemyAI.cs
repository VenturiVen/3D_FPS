using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float sideToSideSpeed = 2.25f;
    [SerializeField] private float sideToSideAmplitude = 10f;
    [SerializeField] private float hoverHeight = 1f;
    [SerializeField] private float detectionRange = 13f;
    [SerializeField] private float surroundDistance = 4.5f;

    [Header("Raycast & Shooting")]
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileCooldown = 2f;
    [SerializeField] private float projectileSpeed = 10f;

    private Transform player;
    private float shootTimer;

    private Vector3 RandomTarget;
    private Vector3 spawnPoint;

    private EnemyState currentState;

    // these are values preserved for previous X-Z Axis Smoothing with Perlin noise.
    private float previousNoiseX;
    private float previousNoiseZ;
    
    private float noiseSmoothingFactor = 5f;

    private enum EnemyState { Patrolling, Pursuing, AttackMode }

    private void Start()
    {
        spawnPoint = transform.position; // When enemy spawns, remember the position of the spawn.
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentState = EnemyState.Patrolling; // Start off patrolling the area you spawned in
    }

    // Check, update and execute changes.
    private void Update()
    {
        if (player == null) return;

        UpdateState();
        ExecuteState();
    }

    
    private void UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            // At Starting State, check if distance is within detection radius.
            case EnemyState.Patrolling:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = EnemyState.Pursuing;
                }
                break;
            
            // When chasing player, if enemy close enough, start Attacking with AttackMode
            // else-if player has run away from detection radius, go back to patrolling.
            case EnemyState.Pursuing:
                if (distanceToPlayer <= surroundDistance)
                {
                    currentState = EnemyState.AttackMode;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = EnemyState.Patrolling;
                }
                break;

            // If player has run away from detection radius, go back to pursuing.
            case EnemyState.AttackMode:
                if (distanceToPlayer > surroundDistance)
                {
                    currentState = EnemyState.Pursuing;
                }
                break;
        }
    }
    
    private void ExecuteState()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;

            case EnemyState.Pursuing:
                PursuePlayer();
                break;

            case EnemyState.AttackMode:
                AttackMode();
                TryShoot();
                break;
        }
    }

    // In our assigned spawn point, create a radius to patrol an area.
    private void Patrol()
    {
        if (RandomTarget == Vector3.zero || Vector3.Distance(transform.position, RandomTarget) < 1f) // If no patrol targets have been set, generate new random targets. 
        {
            Vector2 randomCircle = Random.insideUnitCircle * patrolRadius; // Go to random points within the radius.
            RandomTarget = spawnPoint + new Vector3(randomCircle.x, 0, randomCircle.y);
            RandomTarget.y = spawnPoint.y + hoverHeight; // Maintain a constant hover height to stay flying.
        }

        MoveTowards(RandomTarget);
    }

    private void PursuePlayer()
    {
        if (player == null)
        {
            return; // Return nothing if Player is destroyed or not there.
        }

        MoveTowards(player.position);
    }

    private void AttackMode()
    {
        if (player == null)
        {
            return; // Return nothing if Player is destroyed or not there.
        }
        
        // Calculate direction to the player, then create a radius and try and keep the player in the radius via fixedPos.
        Vector3 directionToPlayer = (transform.position - player.position).normalized;
        Vector3 fixedPosition = player.position + directionToPlayer * surroundDistance;
        fixedPosition.y = player.position.y + hoverHeight;

        // Use time since game started and Perlin Noise to create random values for the X-Z axis.
        float time = Time.time * sideToSideSpeed;
        float noiseX = Mathf.PerlinNoise(time, 0f) * 2f - 1f;
        float noiseZ = Mathf.PerlinNoise(0f, time) * 2f - 1f;

        // Use Lerp to smooth transitions from previous noise values. ( did not help :c )
        noiseX = Mathf.Lerp(previousNoiseX, noiseX, Time.deltaTime * noiseSmoothingFactor);
        noiseZ = Mathf.Lerp(previousNoiseZ, noiseZ, Time.deltaTime * noiseSmoothingFactor);

        previousNoiseX = noiseX;
        previousNoiseZ = noiseZ;

        // Create an offset based off the Perlin Noise.
        Vector3 noiseOffset = new Vector3(noiseX, 0, noiseZ) * sideToSideAmplitude;
        Vector3 targetPosition = fixedPosition + noiseOffset;

        // If the distance of the noise and fixed position are too low, no need to move, this is to try and prevent stuttery movement. (did not work...)
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = fixedPosition;
        }

        MoveTowards(targetPosition);
    }

    private void MoveTowards(Vector3 target)
    {
        // Move the enemy to the player.
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // If the player exists, lock on to the player.
        if (player != null)
        {
            Vector3 lookDirection = (player.position - transform.position).normalized; 
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    private void TryShoot()
    {
        // Start a timer via deltaTime to detect when its ready to shoot past the cooldown.
        shootTimer += Time.deltaTime;
        
        // When the enemy is aiming correctly, and the timer has passed the cooldown, you can shoot.
        if (shootTimer >= projectileCooldown && RaycastHitsPlayer())
        {
            ShootProjectile();
            shootTimer = 0f; // Reset the timer.
        }
    }

    // Check if Raycast hits the player, then allow shooting.
    private bool RaycastHitsPlayer()
    {
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out RaycastHit hit, detectionRange))
        {
            return hit.transform.CompareTag("Player");
        }
        return false;
    }

    private void ShootProjectile()
    {
        if (projectilePrefab == null || raycastOrigin == null) {
            return; // If there's no Projectile or RaycastObject available, return nothing.
        }
        
        // Instantiate projectile, then set the velocity of projectile via rigidbody.
        GameObject projectile = Instantiate(projectilePrefab, raycastOrigin.position, raycastOrigin.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = raycastOrigin.forward * projectileSpeed;
        
        // Check if projectile has the Script attached. If it does, homing is set.
        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(player);
        }
    }

    // Used to visualise Patrol, Detection and Surround radii.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(spawnPoint, patrolRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, surroundDistance);
    }
}
