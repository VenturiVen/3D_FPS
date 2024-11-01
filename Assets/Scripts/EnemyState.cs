using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public Vector3 enemyVel = new Vector3(0f,0f,0f);
    public Vector3 enemyGravityVector = Vector3.zero;
    public float enemySpeed = 2f;
    public abstract EnemyState Run();
}
