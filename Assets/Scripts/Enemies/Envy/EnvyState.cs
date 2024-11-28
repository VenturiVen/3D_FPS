using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every class of an Envy state inherits this class
public abstract class EnvyState : MonoBehaviour
{
    public Vector3 enemyDir = new Vector3(0f, 0f, 0f);
    public float enemySpeed = 0f;
    public bool isGrounded = false;
    public bool isPlayerContact = false; // tracking if enemy is in contact with player
    public bool isEnemyContact = false; // tracking if enemy is in contact with other enemies (for steering purposes)
    public bool isPlayerSight = false; // tracking if enemy can see player

    /* state transition conditons
     * deleted these for now, don't think they are necessary
     * 
    public bool isPlayerInChaseRange = false;
    public bool isPlayerInAttackRange = false;
    public bool isPlayerInJumpAttackRange = false;
    */

    public abstract EnvyState Run();
    public abstract EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact, bool isPlayerSight);
}
