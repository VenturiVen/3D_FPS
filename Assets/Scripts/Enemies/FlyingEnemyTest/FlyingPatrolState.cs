using UnityEngine;
using UnityEngine.AI;

public class FlyingPatrolState : FlyingState
{
    private Vector3 spawnPoint;
    private Vector3 randomTarget;
    private float waypointTimer;
    private const float waypointInterval = 2.5f;

    public FlyingPatrolState(FlyingEnemyAI enemyAI) : base(enemyAI)
    {
        spawnPoint = enemyAI.transform.position;
    }

    public override void Execute()
    {
        Patrol();
    }

    // Patrol around the NavMeshSurface at random points.
    private void Patrol()
    {
        NavMeshAgent agent = enemyAI.GetNavMeshAgent();

        // Update the waypoint if there is no random target, if the enemy has reached the way point or if the timer is completed.
        waypointTimer += Time.deltaTime;
        if (randomTarget == Vector3.zero || agent.remainingDistance <= agent.stoppingDistance || waypointTimer >= waypointInterval)
        {
            // Select a random point on the NavMesh.
            Vector3 randomPoint;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(RandomNavmeshLocation(), out hit, 1000f, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
                randomTarget = randomPoint;
                randomTarget.y = spawnPoint.y + enemyAI.GetHoverHeight();

                // Go to the random target that's set by the NavMeshAgent and reset the timer.
                agent.SetDestination(randomTarget);
                waypointTimer = 0f;
            }
        }
        
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            enemyAI.SmoothRotateTowards(agent.steeringTarget);
        }
    }
    
    private Vector3 RandomNavmeshLocation()
    {
        // Go to a random direction, obtained from a wide range to make it modular for multiple use cases.
        Vector3 randomDirection = Random.insideUnitSphere * 500f;
        randomDirection += enemyAI.transform.position;
        return randomDirection;
    }
}
