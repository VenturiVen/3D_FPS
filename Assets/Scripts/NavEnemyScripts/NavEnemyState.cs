using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavEnemyState : MonoBehaviour
{
    public float enemySpeed = 0f;
    public bool isPLayerContact = false;
    public int currentTarget = 0;
    public abstract NavEnemyState Run();
    public abstract NavEnemyState Run(float enemySpeed, bool isPlayerContact, int currentTarget);
}
