using UnityEngine;
public class FlyingChaseState : FlyingState
{
    public FlyingChaseState(FlyingEnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override void Execute()
    {
        PursuePlayer();
    }

    private void PursuePlayer()
    {
        // Return nothing if Player is destroyed or not there.
        if (enemyAI.GetPlayer() == null) return;

        Vector3 direction = (enemyAI.GetPlayer().position - enemyAI.transform.position).normalized;
        enemyAI.transform.position += direction * enemyAI.GetMoveSpeed() * Time.deltaTime;
        enemyAI.transform.rotation = Quaternion.LookRotation(direction);
    }
}