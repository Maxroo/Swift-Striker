using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverRed : MonoBehaviour {


AudioSource audioSource;
public AudioClip Redwins;
public AudioClip crowd;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(loadSceneAfterDelay(30));
        audioSource = GetComponent<AudioSource>();
                Renderer rend = gameObject.GetComponent<Renderer>();

        if (BallScript._TotalScoreRed != 10)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
           audioSource.PlayOneShot(Redwins);

        }

    }

    IEnumerator loadSceneAfterDelay(float waitbySecs)
    {

        yield return new WaitForSeconds(waitbySecs);

        SceneManager.LoadScene("MainMenu");

        BallScript._TotalScoreRed = BallScript._TotalScoreBlue = 0;
    }

    // Update is called once per frame
    /*void Update () {
        Renderer rend = gameObject.GetComponent<Renderer>();

        if (BallScript._TotalScoreRed != 10)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
           audioSource.PlayOneShot(Redwins);

        }

    }*/
}
