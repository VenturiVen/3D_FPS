using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;

public class EnvyIdleState : EnvyState
{
    // return to switch to the specified state
    public EnvyChaseState chase;
    public EnvyRetreatState retreat;

    // variables for coroutine
    Coroutine coroutine;
    private bool countdownFinished = false;
    private bool countdownStarted = false;
    public int timeBeforeRetreat = 5; // how much time the player can be out of range before Envy retreats

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

        // if countdown is not finished
        if (!countdownFinished)
        {
            // check if player is in sight
            if (isPlayerSight)
            {
                Debug.Log("Chase State");
                return chase;
            }
            // if player is not in sight, start countdown
            else if (!countdownStarted)
            {
                countdownStarted = true;
                coroutine = StartCoroutine(TimeBeforeRetreat(timeBeforeRetreat));
                return this;
            }

        }
        // check if player is still not in chase range after countdown finished
        else if (!isPlayerSight) 
        {
            if (countdownFinished)
            {
                countdownFinished = false;
                countdownStarted = false;
                Debug.Log("Retreat State");
                return retreat;
            }
        }
        // check if player is in sight
        else if (isPlayerSight) 
        {
            countdownStarted = false;
            countdownFinished = false;
            return chase;
        }

        return this;
    }

    IEnumerator TimeBeforeRetreat(int timeBeforeRetreat)
    {
        Debug.Log("Countdown started.");
        yield return new WaitForSecondsRealtime(timeBeforeRetreat);
        countdownFinished = true;
    }

}
