using UnityEngine;

public abstract class FlyingState
{
    protected FlyingEnemyAI enemyAI;

    public FlyingState(FlyingEnemyAI enemyAI)
    {
        this.enemyAI = enemyAI;
    }

    public abstract void Execute();
}