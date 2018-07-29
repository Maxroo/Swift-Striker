using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public Text ScoreBlue1;
    public Text ScoreRed1;
    void Start ()
    {
        ScoreBlue1.text = BallScript._TotalScoreBlue.ToString();
        ScoreRed1.text = BallScript._TotalScoreRed.ToString();
    }
	
	void Update ()
    {
        ScoreBlue1.text = BallScript._TotalScoreBlue.ToString();
        ScoreRed1.text = BallScript._TotalScoreRed.ToString();
    }
}
