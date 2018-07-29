using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {

        if ( BallScript._TotalScoreRed == 10 || BallScript._TotalScoreBlue == 10)
        {
            StartCoroutine(GameOver(1f));
        }

        if (BallScript.onecescore == true)
        {

            BallScript.o = true;

        }

        if (BallScript.o == true)
        {
            StartCoroutine(reload(1.5f));

        }
    }
    IEnumerator reload(float waitbySecs)
    {
        yield return new WaitForSeconds(waitbySecs);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        BallScript.onecescore = false;
        BallScript.o = false;
    }
    IEnumerator GameOver(float waitbySecs)
    {
        yield return new WaitForSeconds(waitbySecs);
        SceneManager.LoadScene("GameOver");

    }
    
}
   
