using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D _RB;    //RIGID BODY OF THE CHARACTER
    protected SpriteRenderer _SR;
    public GameObject _RBArrow;
  
    public float jumpMult = 1;
    public float offsetY;
    public float raycastOffset = 0.4f;
    public float raycastDistance = 1f;
    private int jumpCount;
    private float xInput;
    private float yInput;
    private float xInputRJ;
    private float yInputRJ;
    private float angle;
    Animator anim;
    private Vector2 _Direction;

    [Range(0, 4)]
    public int PlayerIndex;

    [Range(1, 2)]
    public int ControllerType;

    [SerializeField]
    private float _MoveSpeed = 5;

    [SerializeField]
    private float _JumpSpeed = 7;

    [SerializeField]
    private KeyCode _JumpKey;

    [SerializeField]
    private KeyCode _JumpKey2;

    [SerializeField]
    private KeyCode _SetKey;

    [SerializeField]
    private KeyCode PauseKey;

    [SerializeField]
    private KeyCode _SetKey2;

    [SerializeField]
    private KeyCode _BumpKey;

    [SerializeField]
    private KeyCode _SpikeKey;

    [SerializeField]
    private int _SetSpeed;

    [SerializeField]
    private int _BumpSpeed;

    [SerializeField]
    public static int _SpikeSpeed;

    public static bool Pause = true;
    bool canpause = true;

    public AudioClip Set;
    public AudioClip Bump;
    public AudioClip Spike;
    public AudioClip ballhit;
    public AudioClip Death;
    public AudioClip Jump;
    public AudioClip CrowdAw;
    public AudioClip walk;


    AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();     //GETTING THE COMPONENTS FROM THE GAME OBJECT 
        _SR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded(-raycastOffset) || IsGrounded(raycastOffset); //CHECK IF THE CHARACTER IS GROUNDED

        xInput = Input.GetAxis("Horizontal_" + (PlayerIndex));
        yInput = Input.GetAxis("Vertical_" + (PlayerIndex));
        //print(PlayerIndex);
        //SWITCH THESE ARROUND FOR THE ARROW AND THE SHOOTING TO SEWITCH BETWEEN RS LS
        //xInputRJ = Input.GetAxis("HorizontalRJ_" + (PlayerIndex));  //USING RIGHT HJOYSTIC
        //yInputRJ = Input.GetAxis("VerticalRJ_" + (PlayerIndex));

        if (xInput < 0) _SR.flipX = true;                                                                  
        else if (xInput > 0) _SR.flipX = false;
        _RB.velocity = new Vector2(_MoveSpeed * xInput, _RB.velocity.y);  //CHARACTER MOVING

        

        if (Input.GetKeyDown(PauseKey))
        {
            if (canpause)
            {
                Debug.Log("pause");
                Time.timeScale = 0;
                canpause = false;
                Pause = false;
            }
            else
            {
                Time.timeScale = 1;
                canpause = true;
                Pause = true;
            }
        
    }


        if (ControllerType == 1)
        {
            if (Input.GetKeyDown(_SetKey))
            {
                anim.SetBool("Set", true);
                //audioSource.PlayOneShot(Set, 2F);
            }
            else
            {
                anim.SetBool("Set", false);
            }

            //BUMP
            if (Input.GetKeyDown(_BumpKey))
            {
                anim.SetBool("Bump", true);
                //audioSource.PlayOneShot(Bump, 2F);

            }
            else
            {
                anim.SetBool("Bump", false);
            }


            //SPIKE
            if (Input.GetKeyDown(_SpikeKey) && !isGrounded)
            {
                anim.SetBool("Spike", true);
                //audioSource.PlayOneShot(Spike, 0.7F);

            }
            else
            {
                anim.SetBool("Spike", false);
            }

           
          if (isGrounded == true)
            {
                anim.SetFloat("Walk", Mathf.Abs(xInput));
                if(Input.GetAxis("Horizontal_" + (PlayerIndex)) != 0)
                    {
                    audioSource.playOnAwake = true;
                    /*if (audioSource.playOnAwake)
                    {
                       audioSource.Play(); // Play the clip if it wasn't set to play on awake
                    } */

                }
                 anim.SetBool("Jump", false);
            } else if (isGrounded == false)
            {
                anim.SetFloat("Walk", 0);
                anim.SetBool("Jump", true);
                audioSource.playOnAwake = false;
            }
   
        }



        if (ControllerType == 1)
        {
            Vector3 LookVec1 = new Vector3(yInput, xInput, 4096);
            _RBArrow.transform.rotation = Quaternion.LookRotation(LookVec1, Vector3.back); //MAKE ARROR ROTATE

            if ((isGrounded || jumpCount <= 1) && Input.GetKeyDown(_JumpKey))   //JUMPING 
            {
                /*if (BallSpawner.ball == false) {*/
                jumpCount++;
                _RB.velocity = new Vector2(_RB.velocity.x, _JumpSpeed * jumpMult);
                 audioSource.PlayOneShot(Jump, 1f);
            }
                /*}*/

        }

        else if (ControllerType == 2)
        {
            Vector3 LookVec = new Vector3(yInputRJ, xInputRJ, 4096);
            _RBArrow.transform.rotation = Quaternion.LookRotation(LookVec, Vector3.back); //MAKE ARROR ROTATE

            if ((isGrounded || jumpCount <= 1) && Input.GetKeyDown(_JumpKey2))   //JUMPING 
            {
                if (BallSpawner.ball == false)
                {
                    jumpCount++;
                    _RB.velocity = new Vector2(_RB.velocity.x, _JumpSpeed * jumpMult);
                }
            }

        }
    

    }


    protected bool IsGrounded(float offsetX)     //FUNCTION THAT CHECKS IF ITS GROUNDED 
    {
        Vector2 origin = transform.position; //the pivot point //vector 2 is 2 floats    
        origin.x += offsetX;
        origin.y += offsetY;
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.down, raycastDistance); //(from origin cast a raydown, a given distance)
        Debug.DrawRay(origin, Vector2.down * raycastDistance, Color.green);
        
        if (hitInfo == true && hitInfo.collider.gameObject.CompareTag("Ground"))        
        {
            jumpCount = 0;
            return true;
        }
        return false;
         //NOT RETURNING TRUE MAYBE

    }

    //This requires the ball to have a trigger slightly bigger than its collider
   // private void OnTriggerStay2D(Collider2D other)   ///////CHANGE BACK/////////////// 
        private void OnTriggerEnter2D(Collider2D other)
    {
        bool isGrounded = IsGrounded(raycastOffset) || IsGrounded(-raycastOffset); //CHECK IF THE CHARACTER IS GROUNDED

        //TO DO 
        //MOVE UP FOR BUM
        //REMOVE BOUNCE IF HITS PLAYER

        if (ControllerType == 1)
        {
            if (other.CompareTag("Ball") && Input.GetKey(_SetKey))
            {
                //Add force here based on this vector mult by ForceMagnitud
                Vector2 dir = new Vector2(xInput, -yInput).normalized;
                other.GetComponent<Rigidbody2D>().velocity = dir * _SetSpeed;
                print("set");
            }

            //BUMP
            if (other.CompareTag("Ball") && Input.GetKey(_BumpKey))
            {
                Vector2 dir = new Vector2(0, 5).normalized;
                other.GetComponent<Rigidbody2D>().velocity = dir * _BumpSpeed; //MOVE UP
            }

            //SPIKE
            if (other.CompareTag("Ball") && Input.GetKey(_SpikeKey))
                if(isGrounded == false){
            {
                Vector2 dir = new Vector2(xInput, -yInput).normalized;
                other.GetComponent<Rigidbody2D>().velocity = dir * _SpikeSpeed;
                audioSource.PlayOneShot(Spike);
            }
                }

        }
        /////////////////////////////////////////////// USING TRIGGERS

        else if (ControllerType == 2)
        {
            //SET
            if (other.CompareTag("Ball") && Input.GetKey(_SetKey2))
            {
                //Add force here based on this vector mult by ForceMagnitud
                Vector2 dir = new Vector2(xInput, -yInput).normalized;
                other.GetComponent<Rigidbody2D>().velocity = dir * _SetSpeed;
            }

            //BUMP
            if (other.CompareTag("Ball") && Input.GetAxis("LTrigger_" + (PlayerIndex)) != 0)
            {
                Vector2 dir = new Vector2(0, 5).normalized;
                other.GetComponent<Rigidbody2D>().velocity = dir * _BumpSpeed; //MOVE UP
            }

            //SPIKE
            if (other.CompareTag("Ball") && Input.GetAxis("RTrigger_" + (PlayerIndex)) != 0)
            {
                Vector2 dir = new Vector2(xInputRJ, -yInputRJ).normalized;
                other.GetComponent<Rigidbody2D>().velocity = dir * _SpikeSpeed;
            }
        }
        //////////
        if (other.CompareTag("Wall"))
        {
            _JumpSpeed = 13;
            //anim.SetBool("Wall", true);
            //anim.SetBool("Jump", false);
            
        }else
        {
            //anim.SetBool("Wall", false);
        }

        if (other.CompareTag("Hell"))
        {
            audioSource.PlayOneShot(Death, 2f);
            audioSource.PlayOneShot(CrowdAw, 3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _JumpSpeed = 8;
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hell"))
        {
            audioSource.PlayOneShot(Death,2f);
            audioSource.PlayOneShot(CrowdAw, 3f);
        }

    }*/    ////////////////////////////////////////////CHANGE BACK TO STAYONTRIGGER2D

    private void WalkingSound()
    {
        audioSource.Play(); // Play the clip if it wasn't set to play on awake
    }

    private void JumpSound()
    {
        //FIND OUT HOW TO ONLY MAKE IT HAPPEN ONCE 
        //audioSource.PlayOneShot(Death, 2f);
    }


}
