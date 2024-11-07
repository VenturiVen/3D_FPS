using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class HUDText : MonoBehaviour
{
    [SerializeField] private GunData gunData;

    // UI Health Text objects
    public GameObject textmeshpro_currentHP;
    public GameObject textmeshpro_maxHP;
    // UI Ammo Text Objects
    public GameObject textmeshpro_currentAmmo;
    public GameObject textmeshpro_maxAmmo;
    // UI score Text Objects
    public GameObject textmeshpro_score;

    // Game variables
    public int currentHP;
    public int maxHP;
    public int currentAmmo;
    public int maxAmmo;
    public int score; 

    // Text components
    TextMeshProUGUI textmeshpro_currentHP_text;
    TextMeshProUGUI textmeshpro_maxHP_text;
    TextMeshProUGUI textmeshpro_currentAmmo_text;
    TextMeshProUGUI textmeshpro_maxAmmo_text;
    TextMeshProUGUI textmeshpro_score_text;

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro_currentHP_text = textmeshpro_currentHP.GetComponent<TextMeshProUGUI>();
        textmeshpro_maxHP_text = textmeshpro_maxHP.GetComponent<TextMeshProUGUI>();
        textmeshpro_currentAmmo_text = textmeshpro_currentAmmo.GetComponent<TextMeshProUGUI>();
        textmeshpro_maxAmmo_text = textmeshpro_maxAmmo.GetComponent<TextMeshProUGUI>();
        textmeshpro_score_text = textmeshpro_score.GetComponent<TextMeshProUGUI>();
    }

    // setting the values to the text on the HUD
    // doing this in FixedUpdate() is HORRIBLE for performance
    // so if we have time I would fix this later
    void FixedUpdate()
    {
        // getting the values
        currentHP = PlayerStats.Instance.getCurrentHP();
        maxHP = PlayerStats.Instance.getMaxHP();
        currentAmmo = PlayerStats.Instance.currentCap;
        maxAmmo = PlayerStats.Instance.magSize;
        score = PlayerStats.Instance.getCurrentScore();

        // converting the values to string to be used by TextMeshPro
        textmeshpro_currentHP_text.text = currentHP.ToString("000");
        textmeshpro_maxHP_text.text = "/" + maxHP.ToString("000");
        textmeshpro_currentAmmo_text.text = currentAmmo.ToString("000");
        textmeshpro_maxAmmo_text.text = "/" + maxAmmo.ToString("000");
        textmeshpro_score_text.text = score.ToString("00000"); // padding with zeroes
    }
}
