using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SchemeScript : MonoBehaviour
{

    public void Advance()
    {
        SceneManager.LoadScene("ArenSelect");
        BallScript._TotalScoreRed = BallScript._TotalScoreBlue = 0;
        BallScript._TotalScoreBlue = BallScript._TotalScoreRed = 0;
    }
}
