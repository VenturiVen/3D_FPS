using UnityEngine;
public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float hoverHeight = 1f;
    [SerializeField] private float detectionRange = 13f;
    [SerializeField] private float surroundDistance = 4.5f;
    [SerializeField] private float sideToSideSpeed = 2.25f;
    [SerializeField] private float sideToSideAmplitude = 10f;

    [Header("Raycast & Shooting")]
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileCooldown = 2f;
    [SerializeField] private float projectileSpeed = 10f;

    private Transform player;
    private EnemyState currentState;
    private FlyingPatrolState patrolState;
    private FlyingChaseState chaseState;
    private FlyingAttackState attackState;
    private Vector3 spawnPoint;

    private enum EnemyState { Patrolling, Pursuing, AttackMode }

    private void Start()
    {
        // When enemy spawns, remember the position of the spawn.
        spawnPoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        // Start off patrolling the area you spawned in
        currentState = EnemyState.Patrolling;
        patrolState = new FlyingPatrolState(this);
        chaseState = new FlyingChaseState(this);
        attackState = new FlyingAttackState(this);
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
                patrolState.Execute();
                break;
            case EnemyState.Pursuing:
                chaseState.Execute();
                break;
            case EnemyState.AttackMode:
                attackState.Execute();
                break;
        }
    }

    // These getter methods are used to call the private variables for the other States.
    public float GetMoveSpeed() => moveSpeed;
    public float GetPatrolRadius() => patrolRadius;
    public float GetHoverHeight() => hoverHeight;
    public float GetDetectionRange() => detectionRange;
    public float GetSurroundDistance() => surroundDistance;
    public Transform GetRaycastOrigin() => raycastOrigin;
    public GameObject GetProjectilePrefab() => projectilePrefab;
    public float GetProjectileCooldown() => projectileCooldown;
    public float GetProjectileSpeed() => projectileSpeed;
    public Transform GetPlayer() => player;
    public float getSidetoSideSpeed() => sideToSideSpeed;
    public float getSidetoSideAmplitude() => sideToSideAmplitude;

    
    // Draw Radius and Range of Patrol, detection for the enemy and surrounding the enemy
    private void OnDrawGizmosSelected()
    {
        // If spawn point hasn't been set in Play mode, use current position in Edit mode
        Vector3 currentSpawnPoint = spawnPoint != Vector3.zero ? spawnPoint : transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(currentSpawnPoint, patrolRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, surroundDistance);
    }
}