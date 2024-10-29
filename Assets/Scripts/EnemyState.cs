using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public Vector3 enemyVel = Vector3.zero;
    public Vector3 enemyGravityVector = Vector3.zero;
    public float enemySpeed = 0f;
    public abstract EnemyState Run();
}
