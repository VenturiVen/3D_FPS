using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // these can be referenced with 
    // // PlayerStats.Instance.stat  
    // // e.g., PlayerStats.Instance.speedModifier

    // wanted to try keep stats in one place for ease of use and we can tweak them in inspector

    [Header("Health Stats")]
    public int maxHP;
    public int currentHP;

    [Header("Movement Stats")]
    public float speedModifier;
    public int jumpStrength = 1;
    public Vector3 currentPos;

    [Header("Score Stats")]
    public int score;

    [Header("Gun Stats")]
    public int currentCap;
    public int magSize;


    private void Awake()
    {
        // check if there already is an Instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(global::PlayerStats.Instance);
        }

        currentHP = maxHP;
    }

    public int getCurrentHP()
    {
        return currentHP;
    }

    public int getMaxHP()
    {
        return maxHP;
    }

    public int getScore()
    {
        return score;
    }

    // Bunch of methods to modify stats
    // cannot be referenced like: PlayerStats.Instance.TakeDamage()

    public void TakeDamage(int num)
    {

        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Dead");
        }
        else
        {
            currentHP -= num;
            Debug.Log("Live On");
        }
    }

    public void Heal(int num)
    {
        currentHP += num;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void IncreaseScore(int num)
    {
        score += num;
    }

    public void IncreaseSpeed(int num)
    {
        speedModifier += num;
    }

    public void DecreaseSpeed(int num)
    {  
        speedModifier -= num;
    }

    public void IncreaseMaxHP(int num)
    {
        maxHP += num;
    }

    public void DecreaseMaxHP(int num) 
    { 
        maxHP -= num; 
    }

}
