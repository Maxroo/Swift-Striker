using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Controls");
        BallScript._TotalScoreBlue = BallScript._TotalScoreRed = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
