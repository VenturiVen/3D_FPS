using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class FSM_NavEnemy : MonoBehaviour
{
    public NavEnemyState current;
    public float newEnemySpeed = 0f;
    public int newContact = 0;
    public int newCurrentTarget = 0;

    void FixedUpdate()
    {
        NavEnemyState next = current?.Run(newEnemySpeed, newContact);

        if (next != null)
        {
            newEnemySpeed = current.enemySpeed;
            newContact = current.contact;
            current = next;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goTo"))
        {
            newContact = 1;
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                newContact = 2;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("goTo"))
        {
            newContact = 0;
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                newContact = 0;
            }
        }
    }
}
