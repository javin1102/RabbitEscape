using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce, gravityAccel;
    
    private Vector3 playerVelocity;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    private InGameManager instance;
    private CharacterController controller;
    private Animator anim;
    private ScoreManager scoreManager;



    private bool swipeRight = false, swipeLeft = false, canJump = true;
    private bool turnLeft = false, turnRight = false;
    private bool isRage,hasTurn = false;


    void Start()
    {
        dragDistance = Screen.height * 7 / 100; //dragDistance is 7% height of the screen
        instance = InGameManager.instance;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        scoreManager = ScoreManager.instance;
    }

 

    void Update()
    {
 

        
        //dead
        if(transform.position.y < -10f)
        {
            
            instance.gameState = InGameManager.GameState.GAMEOVER;
            Destroy(gameObject);
        }


        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                     if(Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y))
                     {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            if (canJump) 
                            {
                                print("Jump");
                                anim.SetBool("Jump", true); 
                                playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityAccel);
                                canJump = false;
                            } 
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            
                        }
                     }

                     else
                    {
                        if ((lp.x > fp.x) && instance.nextLaneDir == Direction.RIGHT && !swipeRight)
                        {   //Right swipe
                            Debug.Log("Turn Right");
                            swipeRight = true;


                        }
                        else if ((lp.x < fp.x) && instance.nextLaneDir == Direction.LEFT && !swipeLeft)
                        {   //Left swipe
                            Debug.Log("Turn Left");
                            swipeLeft = true;
                        }
                    }
                }

            }
        }

        //Jump
        if (controller.isGrounded && playerVelocity.y <= 0)
        {
            playerVelocity.y = 0f;
            anim.SetBool("Jump", false);
            canJump = true;
        }

        playerVelocity.y += gravityAccel * Time.deltaTime;

        controller.Move(transform.forward * speed * Time.deltaTime);
        controller.Move(transform.right * Input.acceleration.x * Time.deltaTime * 25f);
        controller.Move(playerVelocity * Time.deltaTime);
       

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PowerRage"))
        {
            PowerRage pow = other.GetComponent<PowerRage>();
            
            float initSpeed = speed;
            speed += pow.addSpeed;
            isRage = true;

            AudioManager.instance.PlayS("power_up");
            StartCoroutine(resetRage(pow.dur, initSpeed));
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PowerMultiplier"))
        {
            PowerScoreMultiplier pow = other.GetComponent<PowerScoreMultiplier>();

            int initRate = scoreManager.incrementRate;
            scoreManager.incrementRate += pow.multiplier;
            AudioManager.instance.PlayS("power_up");
            StartCoroutine(resetMultiplier(pow.dur, initRate));
            Destroy(other.gameObject);

        }

        if (other.CompareTag("Obstacle") && !isRage)
        {
            instance.gameState = InGameManager.GameState.GAMEOVER;
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(swipeRight && other.CompareTag("Turning") && !turnRight)
        {
            turnRight = true;
        }
        else if(swipeLeft && other.CompareTag("Turning") && !turnLeft)
        {
            turnLeft = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Turning"))
        {
            if (turnRight && !hasTurn)
            {
                swipeRight = false;
                transform.Rotate(new Vector3(0, 90, 0));
                turnRight = false;
                hasTurn = true;
                StartCoroutine(resetSwipe(1.5f));
            }

            else if (turnLeft && !hasTurn)
            {
                swipeLeft = false;
                transform.Rotate(new Vector3(0, -90, 0));
                turnLeft = false;
                hasTurn = true;
                StartCoroutine(resetSwipe(1.5f));
            }

        }
    }

    IEnumerator resetRage(float dur, float speed)
    {
        yield return new WaitForSeconds(dur);
        this.speed = speed;
        isRage = false;
    }

    IEnumerator resetMultiplier (float dur, int rate)
    {
        yield return new WaitForSeconds(dur);
        scoreManager.incrementRate = rate;
    }

    IEnumerator resetSwipe(float dur)
    {
        yield return new WaitForSeconds(dur);
        swipeLeft = false;
        swipeRight = false;
        hasTurn = false;
    }

}

