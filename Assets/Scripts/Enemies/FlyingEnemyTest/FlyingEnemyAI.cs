using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Model Settings")]
    [SerializeField] private GameObject enemyModel;
    
    [SerializeField] private MeshRenderer meshRenderer;

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

    private NavMeshAgent navMeshAgent;
    
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
        
            // Add a NavMeshAgent component to the enemy.
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.agentTypeID = NavMesh.GetSettingsByIndex(2).agentTypeID;
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.acceleration = (moveSpeed) / 2 + 5f;
            navMeshAgent.angularSpeed = 720f;
            navMeshAgent.height = 2f;
            navMeshAgent.radius = 0.5f;
            navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    
            navMeshAgent.baseOffset = hoverHeight;
            navMeshAgent.updateRotation = false;
        
        // Makes sure both Parent and EnemyModel are rotating the same.
        if (enemyModel != null)
        {
            enemyModel.transform.position = transform.position;
            enemyModel.transform.rotation = transform.rotation;
        }

        // Start off patrolling the area of the NavMesh.
        currentState = EnemyState.Patrolling;
        patrolState = new FlyingPatrolState(this);
        chaseState = new FlyingChaseState(this);
        attackState = new FlyingAttackState(this);

        UpdateMaterialColor(); // Set the initial material color
    }

    // Check, update and execute changes.
    private void Update()
    {
        if (player == null) return;
        UpdateState();
        ExecuteState();
        
        // Sync the model with the Parent FlyingEnemy.
        if (enemyModel != null)
        {
            enemyModel.transform.position = transform.position;
            enemyModel.transform.rotation = transform.rotation;
        }
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
                    UpdateMaterialColor();
                }
                break;
            
            // When chasing player, if enemy close enough, start Attacking with AttackMode
            // else-if player has run away from detection radius, go back to patrolling.
            case EnemyState.Pursuing:
                if (distanceToPlayer <= surroundDistance)
                {
                    currentState = EnemyState.AttackMode;
                    UpdateMaterialColor();
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = EnemyState.Patrolling;
                    UpdateMaterialColor();
                }
                break;

            // If player has run away from detection radius, go back to pursuing.
            case EnemyState.AttackMode:
                if (distanceToPlayer > surroundDistance)
                {
                    currentState = EnemyState.Pursuing;
                    UpdateMaterialColor();
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
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;
    
    private void SmoothRotationTransition()
    {
        Vector3 directionToTarget = (spawnPoint - transform.position).normalized;
    
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    // Added method to update the material color based on the current state.
    private void UpdateMaterialColor()
    {
        if (meshRenderer == null) return;

        switch (currentState)
        {
            case EnemyState.Patrolling:
                meshRenderer.material.color = Color.green;
                break;
            case EnemyState.Pursuing:
                meshRenderer.material.color = Color.yellow;
                break;
            case EnemyState.AttackMode:
                meshRenderer.material.color = Color.red;
                break;
        }
    }

    public void SmoothRotateTowards(Vector3 targetPosition)
    {
        // Get the norm of the distance between target and enemy to get the direction.
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // With the direction, we interpolate the rotation between the enemy and the target using
        // Slerp if the direction to the target is offset. 
        if (directionToTarget.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    
    // Draw Radii and Range of detection for the enemy and surrounding the enemy including the raycast of the enemy.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, surroundDistance);

        if (raycastOrigin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(raycastOrigin.position, raycastOrigin.forward * detectionRange);
        }
    }
}
