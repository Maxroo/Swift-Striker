using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{

    /*private bool _ContactWithPlayer=false;*/
    protected SpriteRenderer _SR;
    public GameObject BallGlow;
    public GameObject BallGlowRed;
    protected Rigidbody2D _RB;
    private int _PlayerCounter;
    public static int _TotalScoreRed;
    public static int _TotalScoreBlue;
    public static bool LastScored;
    public static bool LastTouched;
    public AudioClip green;
    public Sprite BallGreen;
    public Sprite BallYellow;
    public Sprite BallRed;
    public Sprite BallGrey;
    public GameObject praticeball;
    public GameObject praticeballRed;
    public GameObject Trigger;

    public AudioClip Green;
    public AudioClip Yellow;
    public AudioClip Red;
    public AudioClip Grey;
    public AudioClip WallHit;
    private AudioSource ANN_PointScored;
    private AudioSource LightBeam;
    private AudioSource ExplosionSound;
    bool God=false;
    bool preGod = false;
    

     private AudioSource Cheers;
      private AudioSource Boos;

    public ParticleSystem ImpactParticleS;
    public ParticleSystem ExplosionParticle;

    private bool AllowTriggerWithPLayers=true;

    private int CounterSound1 = 0;
    private int CounterSound2 = 0;
    private int CounterSound3 = 0;


    private Quaternion RotationAdjustment; 


    AudioSource audioSource;
    public AudioClip Score;
    public static bool onecescore = false;
    public static bool o;

    private void Awake()
    {
        _SR = GetComponent<SpriteRenderer>();
        _RB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        RotationAdjustment = Quaternion.Euler(-90,0,0);
    }

    private void Start()
    {
        ANN_PointScored = GameObject.FindGameObjectWithTag("PointScoredVO").GetComponent<AudioSource>();
        LightBeam = GameObject.FindGameObjectWithTag("Beam").GetComponent<AudioSource>();
        ExplosionSound = GameObject.FindGameObjectWithTag("EXP").GetComponent<AudioSource>();
        Cheers= GameObject.FindGameObjectWithTag("Cheer").GetComponent<AudioSource>();
        Boos= GameObject.FindGameObjectWithTag("Boo").GetComponent<AudioSource>();
    }

    void Update ()
    {
        if(Input.GetKeyDown("q"))
        {
            preGod = true;
        }
        
        if(Input.GetKeyDown("g") && preGod)
        {
            God = true;
        }
        if(Input.GetKeyDown("p") && God)
        {
            _TotalScoreBlue ++;
        }
        if(Input.GetKeyDown("o") && God)
        {
            _TotalScoreRed ++;
        }
        

            switch (_PlayerCounter)
        {
            case 0:
                _SR.sprite = BallGreen;
                CounterSound1 = 0;
                CounterSound2 = 0;
                CounterSound3 = 0;
                BallGlowRed.SetActive(false);

                //_SR.color = Color.green;
                break;
            case 1:
                _SR.sprite = BallYellow;
                if (CounterSound1==0)
                {
                    audioSource.PlayOneShot(Green, 2F);
                    CounterSound1++;
                }

                //_SR.color = Color.yellow;
                break;
            case 2:
                _SR.sprite = BallRed;

                if (CounterSound2 == 0)
                {
                    audioSource.PlayOneShot(Yellow, 2F);
                    CounterSound2++;
                }

                //_SR.color = Color.red;
                break;
            case 3:
                _SR.sprite = BallGrey;
                BallGlowRed.SetActive(true);
                if (CounterSound3 == 0)
                {
                    audioSource.PlayOneShot(Red, 2F);
                    CounterSound3++;
                }
                //_SR.color = Color.white;
                break;
            default:
                _SR.sprite = BallGrey;
                //_SR.color = Color.white;
                break;
                

        }

      
        
        /* if (_ContactWithPlayer == true)
        {
            _SR.color = Color.red;
        }
        else
        {
            _SR.color = Color.white;
        }*/
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zone"))
        {
            PlayerController._SpikeSpeed = 27;
            BallGlow.SetActive(true);
        } else
        {
            PlayerController._SpikeSpeed = 20;
        }
        if (other.CompareTag("Wall") || other.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(WallHit, 0.7F);
        }

        if (other.CompareTag("PTeamRed"))
        {
            //_RB.velocity = new Vector2(0, 0); //TURN ON AND OFF BOUNCE OF PLAYES
            ++_PlayerCounter;
            LastTouched = true;
            if (_PlayerCounter >= 4)
            {
                ExplosionParticle.Play(); //DO SAME AS TILES AND TURN SPEED OFF //TURN OFF TRIGGER
                ExplosionSound.Play();
                Boos.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                BallGlowRed.SetActive(false); 
                Trigger.SetActive(false);
                _RB.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX; //FREZZE ROTATION
                onecescore = true;

            }

        }

        if (other.CompareTag("PTeamBlue"))
        {
            //_RB.velocity = new Vector2(0, 0);
            ++_PlayerCounter;
            LastTouched = false;
            if (_PlayerCounter >= 4)
            {
                ExplosionParticle.Play(); //JUST NEED TO DISABLE TRIGGER!!!!!!!!!
                ExplosionSound.Play();
                Boos.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                BallGlowRed.SetActive(false);
                Trigger.SetActive(false);
                 BallSpawner.ball = true;
                
                _RB.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                onecescore = true;

            }

        }

        if (_PlayerCounter >= 4)
        {
            if (LastTouched == false)
            {
                BallSpawner.ball = true;                
                LastScored = true;
                _TotalScoreRed = _TotalScoreRed + 1;
            }
            else if (LastTouched == true)
            {
                 BallSpawner.ball = true;
                LastScored = false;
                _TotalScoreBlue = _TotalScoreBlue + 1;
               
                
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reloading with reload script now 
            /*ExplosionParticle.Play(); //DO SAME AS TILES AND TURN SPEED OFF
            ExplosionSound.Play();*/
            
            //Destroy(gameObject);
        }

        if (other.CompareTag("ResetWall"))
        {
            _PlayerCounter = 0;
        }

        
        if (other.CompareTag("ScoreRed"))
        {
            //audioSource.PlayOneShot(Score, 0.7f);
            ANN_PointScored.Play();
            LightBeam.Play();
            Cheers.Play();
            _TotalScoreRed = _TotalScoreRed + 1;
            Instantiate(praticeballRed,transform.position, RotationAdjustment);         
            LastScored = true;
            onecescore = true;
            BallSpawner.ball = true;
            
            Destroy(gameObject);

            /*_RB.transform.position = SpawnPointRed.transform.position;
            _RB.velocity = new Vector2(0, 0);*/
        }

        if (other.CompareTag("ScoreBlue"))
        {
            //audioSource.PlayOneShot(Score, 0.7f);
            ANN_PointScored.Play();
            LightBeam.Play();
            Cheers.Play();
            _TotalScoreBlue = _TotalScoreBlue + 1;
            Instantiate(praticeball, transform.position, RotationAdjustment);
            BallSpawner.ball = true;
            LastScored = false;
            onecescore = true;
            Destroy(gameObject);

        }



    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ImpactParticleS.Play();  //MAYBE CHANGE TO TRIGGER
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Zone"))
        {
            BallGlow.SetActive(false);
            PlayerController._SpikeSpeed = 20;
        }

    }

   


}
