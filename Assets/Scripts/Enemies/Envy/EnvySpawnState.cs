using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvySpawnState : EnvyState
{
    // return to switch to the specified state
    public EnvyIdleState idle;

    // time it takes for Envy to finish spawning
    // this has to be an int not a float
    // as a float it did not work as intended
    // be sure to check inpsector value of this
    public int timeToSpawn = 3;

    // variables for coroutine
    Coroutine coroutine;
    private bool countdownStarted = false;
    private bool countdownFinished = false;

    public override EnvyState Run()
    {
        return this;
    }

    public override EnvyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isPlayerContact, bool isEnemyContact)
    {
        // assigning variables
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        this.isPlayerContact = isPlayerContact;
        this.isEnemyContact = isEnemyContact;

        // #TODO: play spawning animation
         
        // wait a number of seconds before running next line of code 
        // spawn animation should be playing during this time,
        // so however long that animation plays for should be the same as timeToSpawn
        if (!countdownStarted) // if countdown has not started yet, start coroutine
        {
            countdownStarted = true;
            coroutine = StartCoroutine(SpawnTime(timeToSpawn));
        }
        else if (countdownFinished) // if countdown has finished, switch to idle state
        {
            // change to idle state
            Debug.Log("Idle State");
            return idle;
        }

        return this;
        
    }

    // coroutines are good for delaying actions/code
    // good explanation of coroutines: https://youtu.be/kUP6OK36nrM?si=gKOAfMASZtJWyJCn
    IEnumerator SpawnTime (int timeToSpawn)
    {
        //Debug.Log("Countdown started.");
        yield return new WaitForSecondsRealtime(timeToSpawn);
        countdownFinished = true;   
    }
    
}
