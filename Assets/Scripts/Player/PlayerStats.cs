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
    public int healthPackRespawnTime = 300;

    [Header("Movement Stats")]
    public float speedModifier;
    public int jumpStrength = 1;
    public Vector3 currentPos;

    [Header("Score Stats")]
    public int currentScore;
    public int scoreAdded = 100;
    public int scoreGoal = 1000;
    public int timer = 300;

    [Header("Gun Stats")]
    public int currentCap;
    public int magSize;

    [Header("States")]
    public bool isAlive = true;
    public bool knockback = false;
    public Vector3 knockbackPos = Vector3.zero;

    [Header("Panels")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject perkPanel;

    // Added by Hersh: add a cooldown to the amount of times you can get hit by an enemy.
    [SerializeField] private float damageCooldown = 0.5f;
    private float lastDamageTime = 0f;
    
    public void Start()
    {
        gameOverPanel.SetActive(false);
        perkPanel.SetActive(false);
    }

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

    public int getCurrentScore()
    {
        return currentScore;
    }

    // Bunch of methods to modify stats
    // cannot be referenced like: PlayerStats.Instance.TakeDamage()

    public void TakeDamage(int num, Vector3 pos)
    {
        // Check if cooldown period finished.
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            knockbackPos = pos;
            knockback = true;
            currentHP -= num;

            if (currentHP <= 0)
            {
                currentHP = 0;
                PlayerDeath();
            }
            
            lastDamageTime = Time.time;
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

    public void IncreaseScore()
    {
        currentScore += scoreAdded;

        if (currentScore >= scoreGoal)
        {
            scoreGoal += 1000;
            PlayerGainPerk();
        }

    }

    public void IncreaseScoreAdded(float num)
    {
        scoreAdded += (int)(scoreAdded * num);
    }
    public void DecreaseScoreAdded(float num)
    {
        scoreAdded -= (int)(scoreAdded * num);
    }

    public void PlayerDeath()
    {
        // pause game
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        // GameObject.Destroy(gameObject);
        // ^ cause too many problems when restarting the game
        gameOverPanel.SetActive(true);
        isAlive = false;
    }

    public void PlayerGainPerk()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        perkPanel.SetActive(true);
    }
    
    public void IncreaseSpeed(float num)
    {
        speedModifier += (int)(speedModifier * num);
    }

    public void DecreaseSpeed(float num)
    {
        speedModifier -= (int)(speedModifier * num);
    }

    public void IncreaseMaxHP(float num)
    {
        maxHP += (int)(maxHP * num);
    }

    public void DecreaseMaxHP(float num) 
    {
        maxHP -= (int)(maxHP * num);
    }

    public void IncreaseJumpStrength(float num)
    {
        jumpStrength += (int)(jumpStrength * num);
    }

    public void DecreaseJumpStrength(float num)
    {
        jumpStrength -= (int)(jumpStrength * num);
    }

}
