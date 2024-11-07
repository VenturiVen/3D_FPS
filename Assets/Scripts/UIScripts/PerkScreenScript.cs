using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkScreen : MonoBehaviour
{

    [SerializeField] GameObject perkPanel;

    public void DisablePanel()
    {
        perkPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LustBtn()
    {
        PlayerStats.Instance.IncreaseMaxHP(0.30f);
        // DMG -5%
        DisablePanel();
    }

    public void GluttonyBtn()
    {
        PlayerStats.Instance.IncreaseMaxHP(0.1f);
        PlayerStats.Instance.DecreaseSpeed(0.02f);
        DisablePanel();
    }

    public void GreedBtn()
    {
        PlayerStats.Instance.IncreaseScoreAdded(0.20f);
        PlayerStats.Instance.DecreaseMaxHP(0.50f);
        DisablePanel();
    }

    public void SlothBtn()
    {
        PlayerStats.Instance.IncreaseMaxHP(0.25f);
        PlayerStats.Instance.DecreaseSpeed(0.05f);
        DisablePanel();
    }

    public void WrathBtn()
    {
        PlayerStats.Instance.IncreaseSpeed(0.1f);
        PlayerStats.Instance.DecreaseMaxHP(0.05f);  
        DisablePanel();
    }

    public void EnvyBtn()
    {
        PlayerStats.Instance.IncreaseSpeed(0.1f);
        PlayerStats.Instance.DecreaseJumpStrength(0.1f);
        DisablePanel();
    }

    public void PrideBtn()
    {
        // DMG +15%
        PlayerStats.Instance.DecreaseMaxHP(0.2f);
        DisablePanel();
    }
}
