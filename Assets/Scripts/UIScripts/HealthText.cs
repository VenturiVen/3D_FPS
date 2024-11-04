using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    PlayerStats playerStats;

    // UI Health Text objects
    public GameObject textmeshpro_currentHP;
    public GameObject textmeshpro_maxHP;

    // Game variables
    public int currentHP;
    public int maxHP;

    // Text components
    TextMeshProUGUI textmeshpro_currentHP_text;
    TextMeshProUGUI textmeshpro_maxHP_text;

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro_currentHP_text = textmeshpro_currentHP.GetComponent<TextMeshProUGUI>();
        textmeshpro_maxHP_text = textmeshpro_maxHP.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentHP = PlayerStats.Instance.getCurrentHP();
        maxHP = PlayerStats.Instance.getMaxHP();
        textmeshpro_currentHP_text.text = currentHP.ToString();
        textmeshpro_maxHP_text.text = "/" + maxHP.ToString();
    }
}
