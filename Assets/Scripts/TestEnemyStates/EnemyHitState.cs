using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyAttackState attack;
    public Animator anim;
    private int timer = 100;
    private bool playerHasBeenHit = false;
    public override EnemyState Run()
    {
        return this;
    }

    public override EnemyState Run(Vector3 enemyDir, float enemySpeed, bool isGrounded, bool isContact)
    {
        anim.SetBool("isContact", true);

        this.isContact = isContact;
        this.enemyDir = enemyDir;
        this.enemySpeed = enemySpeed;
        this.isGrounded = isGrounded;
        timer -= 1;

        if (timer <= 70 && timer >= 50 && !playerHasBeenHit && isContact)
        {
            PlayerStats.Instance.TakeDamage(30);
            playerHasBeenHit = true;
        }

        if (timer <= 0)
        {
            anim.SetBool("isContact", false);
            timer = 100;
            playerHasBeenHit= false;
            return attack;
        }
        return this;
    }
}
