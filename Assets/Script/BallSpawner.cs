using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public GameObject SpawnScoreRed;
    public GameObject SpawnScoreBlue;
    public GameObject SpawnBallRed;
    public GameObject SpawnBallBlue;    
    public GameObject Ball;
    public GameObject StartScreenRed;
    public GameObject StartScreenBlue;
    public static bool ball = true;
    private Vector2 _Position;
    [SerializeField]
    private KeyCode _JumpKey;
    int BallCounter = 0;

    private void Awake()
    {

    }


    void Start ()
    {
        if (BallScript.LastScored == true)
        {
            StartScreenRed.SetActive(true);
            SpawnBallRed.SetActive(true);
            StartScreenBlue.SetActive(false);
            SpawnBallBlue.SetActive(false);
            _Position = SpawnScoreRed.transform.position;
        }
        else
        {
            StartScreenBlue.SetActive(true);
            StartScreenRed.SetActive(false);
            SpawnBallRed.SetActive(false);
            SpawnBallBlue.SetActive(true);
            _Position = SpawnScoreBlue.transform.position;
        }
    }

    private void Update()
    {
        if (Input.GetKey(_JumpKey))
        {
            ball = false;
            if (BallCounter==0)
            Instantiate(Ball, _Position, Quaternion.identity);
            StartScreenBlue.SetActive(false);
            SpawnBallRed.SetActive(false);
            SpawnBallBlue.SetActive(false);                       
            StartScreenRed.SetActive(false);
            BallCounter++;
        }
    }

}
