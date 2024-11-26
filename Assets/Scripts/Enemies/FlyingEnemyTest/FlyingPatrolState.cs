using UnityEngine;

public class FlyingPatrolState : FlyingState
{
    private Vector3 spawnPoint;
    private Vector3 RandomTarget;

    public FlyingPatrolState(FlyingEnemyAI enemyAI) : base(enemyAI)
    {
        this.spawnPoint = enemyAI.transform.position;
    }

    public override void Execute()
    {
        Patrol();
    }

    // In our assigned spawn point, create a radius to patrol an area.
    private void Patrol()
    {
        // If no patrol targets have been set, generate new random targets. 
        if (RandomTarget == Vector3.zero || Vector3.Distance(enemyAI.transform.position, RandomTarget) < 1f)
        {
            // Go to random points within the radius.
            Vector2 randomCircle = Random.insideUnitCircle * enemyAI.GetPatrolRadius();
            RandomTarget = spawnPoint + new Vector3(randomCircle.x, 0, randomCircle.y);
            // Maintain a constant hover height to stay flying.
            RandomTarget.y = spawnPoint.y + enemyAI.GetHoverHeight();
        }

        MoveTowards(RandomTarget);
    }

    private void MoveTowards(Vector3 target)
    {
        // Move the enemy to the player.
        Vector3 direction = (target - enemyAI.transform.position).normalized;
        enemyAI.transform.position += direction * enemyAI.GetMoveSpeed() * Time.deltaTime;

        // If the player exists, lock on to the player.
        if (enemyAI.GetPlayer() != null)
        {
            Vector3 lookDirection = (enemyAI.GetPlayer().position - enemyAI.transform.position).normalized;
            enemyAI.transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}