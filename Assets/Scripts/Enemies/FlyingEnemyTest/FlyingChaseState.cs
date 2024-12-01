using UnityEngine;
using UnityEngine.AI;

public class FlyingChaseState : FlyingState
{
    private Vector3 lastPlayerPosition; 
    private const float distanceToPass = 1f;

    public FlyingChaseState(FlyingEnemyAI enemyAI) : base(enemyAI)
    {
        lastPlayerPosition = Vector3.zero;
    }

    public override void Execute()
    {
        PursuePlayer();
        Debug.Log("Chase State active.");

    }

    private void PursuePlayer()
    {
        // Early return if no player is available
        if (enemyAI.GetPlayer() == null) return;

        NavMeshAgent agent = enemyAI.GetNavMeshAgent();
        Transform player = enemyAI.GetPlayer();

        // Check if the player has moved since the last update.
        if (Vector3.Distance(lastPlayerPosition, player.position) > distanceToPass)
        {
            // Set the destination as the players position.
            Vector3 targetPosition = player.position;
            targetPosition.y = player.position.y + enemyAI.GetHoverHeight();
            agent.SetDestination(targetPosition);

            // Update the last known player position.
            lastPlayerPosition = player.position;
        }

        FacePlayerContinuously();
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
