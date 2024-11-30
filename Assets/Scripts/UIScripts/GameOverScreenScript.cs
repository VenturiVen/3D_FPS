using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void RestartBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("EnvyAI_Testing");
    }
    
    public void ToMenuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }
}
