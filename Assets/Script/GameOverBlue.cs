using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBlue : MonoBehaviour {


AudioSource audioSource;
public AudioClip Bluewins;
public AudioClip crowd;



	// Use this for initialization
	void Start () {
        StartCoroutine(loadSceneAfterDelay(30));
        audioSource = GetComponent<AudioSource>();

        
        Renderer rend = gameObject.GetComponent<Renderer>();

        if (BallScript._TotalScoreBlue != 10)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
            audioSource.PlayOneShot(Bluewins);
        }

    }

    IEnumerator loadSceneAfterDelay(float waitbySecs)
    {

        yield return new WaitForSeconds(waitbySecs);

        SceneManager.LoadScene("MainMenu");

        BallScript._TotalScoreRed = BallScript._TotalScoreBlue = 0;

    }

    /*void Update()
    {


    }*/
}
