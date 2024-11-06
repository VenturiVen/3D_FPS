using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public Vector3 enemyDir = new Vector3(0f,0f,0f);
    public float enemySpeed = 0f;
    public bool isGrounded = false;
    public bool isContact = false;
    public abstract EnemyState Run();
    public abstract EnemyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isContact);
}
