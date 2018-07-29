using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

    private int _NumberOfHits=0;
    public Sprite AfterHits;

    private int Sound1Counter=0;
    private int Sound2Counter = 0;

    public ParticleSystem GroundBreakPS;
    public AudioClip GroundHit;
    public AudioClip GroundBreak;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            ++_NumberOfHits;
        }

        if (_NumberOfHits == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = AfterHits;
            if (Sound1Counter == 0)
            {
                //GroundBreakPS.Play();
                audioSource.PlayOneShot(GroundHit, 0.7F);
                Sound1Counter++;
            }

        }

        if (_NumberOfHits == 2)
        {
            if (Sound2Counter == 0)
            {
                audioSource.PlayOneShot(GroundBreak, 0.7F);
                Sound2Counter++;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GroundBreakPS.Play();



                //Destroy(gameObject); //DOSENT LET THE SOUND OR PARTICLES PLAY///COMMENT THIS FOR PREVIEW

            }

        }

    }
}
