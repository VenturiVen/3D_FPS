using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private MeshRenderer cube;
    private PlayerStats playerStats;
    private int timer = 0;
    private void Start()
    {
        cube = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        Debug.Log(timer);
        if (timer > 0)
        {
            timer--;
        }
        else 
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0); //rotates 50 degrees per second around y axis
            cube.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player") && (timer == 0))
        {
            PlayerStats.Instance.Heal(100);
            cube.enabled = false;
            PlayerStats.Instance.healthPackRespawnTime += 100;
            timer = PlayerStats.Instance.healthPackRespawnTime;
        }
    }
}
