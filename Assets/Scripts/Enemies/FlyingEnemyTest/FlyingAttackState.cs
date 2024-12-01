using UnityEngine;

public class FlyingAttackState : FlyingState
{
    private float shootTimer;
    private float previousNoiseX;
    private float previousNoiseZ;
    public FlyingAttackState(FlyingEnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override void Execute()
    {
        AttackMode();
        FacePlayerContinuously();
        Debug.Log("Attack State active.");
        TryShoot();

    }

    private void AttackMode()
    {
        Transform player = enemyAI.GetPlayer();
    
        // Early return if no player is available
        if (player == null) return;

        float surroundDistance = enemyAI.GetSurroundDistance();
        float sideToSideSpeed = enemyAI.getSidetoSideSpeed();
        float sideToSideAmplitude = enemyAI.getSidetoSideAmplitude();

        // Calculate the direction to the player.
        Vector3 directionToPlayer = (player.position - enemyAI.transform.position).normalized;
    
        // Calculate a circular radius around the player.
        Vector3 playerRadius = player.position - directionToPlayer * surroundDistance;
        playerRadius.y = player.position.y + enemyAI.GetHoverHeight();

        // Generate a random offset using Perlin noise.
        float time = Time.time * sideToSideSpeed;
        float angle = Mathf.PerlinNoise(time, 0f) * Mathf.PI * 2f;
    
        // Create an offset to stay near the player.
        Vector3 perpendicular = Vector3.Cross(directionToPlayer, Vector3.up).normalized;
        Vector3 circularOffset = perpendicular * Mathf.Cos(angle) + Vector3.up * Mathf.Sin(angle);
        circularOffset *= sideToSideAmplitude;
        Vector3 targetPosition = playerRadius + circularOffset;

        // Move towards the target position
        MoveTowards(targetPosition);
    }

    private void TryShoot()
    {
        // Start a timer via deltaTime to detect when its ready to shoot past the cooldown.
        shootTimer += Time.deltaTime;

        // When the enemy is aiming correctly, and the timer has passed the cooldown, you can shoot.
        if (shootTimer >= enemyAI.GetProjectileCooldown() && RaycastHitsPlayer())
        {
            ShootProjectile();
            // Reset the timer.
            shootTimer = 0f;
        }
    }

    // Check if Raycast hits the player, then allow shooting.
    private bool RaycastHitsPlayer()
    {
        if (Physics.Raycast(enemyAI.GetRaycastOrigin().position, enemyAI.GetRaycastOrigin().forward, out RaycastHit hit, enemyAI.GetDetectionRange()))
        {
            return hit.transform.CompareTag("Player");
        }
        return false;
    }

    private void ShootProjectile()
    {
        // If there's no Projectile or RaycastObject available, return nothing.
        if (enemyAI.GetProjectilePrefab() == null || enemyAI.GetRaycastOrigin() == null) return;

        // Instantiate projectile, then set the velocity of projectile via rigidbody.
        GameObject projectile = Object.Instantiate(enemyAI.GetProjectilePrefab(), enemyAI.GetRaycastOrigin().position, enemyAI.GetRaycastOrigin().rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = enemyAI.GetRaycastOrigin().forward * enemyAI.GetProjectileSpeed();

        // Check if projectile has the Script attached. If it does, homing is set.
        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(enemyAI.GetPlayer());
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - enemyAI.transform.position).normalized;
        enemyAI.transform.position += direction * enemyAI.GetMoveSpeed() * Time.deltaTime;
    }

    private void FacePlayerContinuously()
    {
        // Return if no player is available
        Transform player = enemyAI.GetPlayer();
        if (player == null) return;
        
        // Get the norm of the distance between player and enemy to get the direction.

        Vector3 directionToPlayer = (player.position - enemyAI.transform.position).normalized;
        
        // With the direction, we interpolate the rotation between the enemy and the target using
        // Slerp if the direction to the player is offset. 
        if (directionToPlayer.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            enemyAI.transform.rotation = Quaternion.Slerp(enemyAI.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
