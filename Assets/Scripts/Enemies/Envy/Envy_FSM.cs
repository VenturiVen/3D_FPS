using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class Envy_FSM : MonoBehaviour
{
    // assign current state to Envy in inpsector
    // Envy starts off in this state
    public EnvyState current;

    public Vector3 newDir = new Vector3(0f, 0f, 0f);
    public float newSpeed = 0f;
    public bool newIsGrounded = false; 
    public bool newPlayerContact = false; // tracking if Envy is in contact with player
    public bool newEnemyContact = false; // tracking if Envy is in contact with other enemies (for steering purposes)
    public bool newPlayerSight = false;
    RaycastHit ray;

    public Transform player;

    void FixedUpdate()
    {
        // state transition
        // once a state is called, its public variables are passed as params
        // for instantiating the next state
        EnvyState next = current?.Run(newDir, newSpeed, newIsGrounded, newPlayerContact, newEnemyContact, newPlayerSight);

        if (next != null)
        {
            // changing values to that of the new state
            newDir = current.enemyDir;
            newSpeed = current.enemySpeed;
            newIsGrounded = current.isGrounded;
            newPlayerContact = current.isPlayerContact;
            newEnemyContact = current.isEnemyContact;
            current = next;
        }

        if (Physics.SphereCast(transform.position, 0.2f, -transform.up, out ray, 1f))
        {
            newIsGrounded = true;
        }
        else
        {
            newIsGrounded = false;
        }

    }

    // not seen in the game
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position - transform.up * 1, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player Contact");
            newPlayerContact = true;
            if (Physics.Linecast(transform.position, player.position, out ray))
            {
                if (ray.collider.CompareTag("Player"))
                {
                    newPlayerSight = true;
                }
            }

        } 
        if (other.CompareTag("Enemy"))
        {
            newEnemyContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NOT Contact");
            newPlayerContact = false;
        }
        if (other.CompareTag("Enemy"))
        {
            newEnemyContact = false;
        }
    }
}
