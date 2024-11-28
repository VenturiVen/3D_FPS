using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavEnemyState : MonoBehaviour
{
    public float enemySpeed = 0f;
    public int contact = 0;
    public abstract NavEnemyState Run();
    public abstract NavEnemyState Run(float enemySpeed, int contact);
}
