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
        DisablePanel();
    }

    public void GluttonyBtn()
    {
        DisablePanel();
    }

    public void GreedBtn()
    {
        DisablePanel();
    }

    public void SlothBtn()
    {
        DisablePanel();
    }

    public void WrathBtn()
    {
        DisablePanel();
    }

    public void EnvyBtn()
    {
        DisablePanel();
    }

    public void PrideBtn()
    {
        DisablePanel();
    }
}
