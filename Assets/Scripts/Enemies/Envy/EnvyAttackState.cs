using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvyAttackState : EnvyState
{
    // return to switch to the specified state
    public EnvyIdleState idle;
    public EnvyChaseState chase;

    // bool to track if player has been hit
    private bool playerHit = false;

    // variables for coroutine
    Coroutine coroutine;
    private bool countdownFinished = false;
    private bool countdownStarted = false;
    public int coolDownTime = 3; // time after attacking before Envy can attack again

    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact, bool isPlayerSight)
    {
        // assigning variables
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;
        
        if (!playerHit || !countdownStarted)
        {
            Debug.Log("Player Hit!");
            PlayerStats.Instance.TakeDamage(20, transform.position);
            playerHit = true;
            countdownStarted = true;
            coroutine = StartCoroutine(AttackCooldown(coolDownTime));
            return this;
        }
        
        if (countdownFinished)
        {
            countdownFinished = false;
            playerHit = false;
            countdownStarted = false;
            Debug.Log("Chase State");
            return chase;
        }
       
        return this;
        
    }

    IEnumerator AttackCooldown(int coolDownTime)
    {
        Debug.Log("Countdown started.");
        yield return new WaitForSecondsRealtime(coolDownTime);
        countdownFinished = true;
    }
}
