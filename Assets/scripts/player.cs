using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public int playerSpeed = 4;
    public bool facingRight = true;
    public int playerJumpIntensity = 1200;
    private float moveXdir;
    private Animator animator;
    private bool isActive = false;
    public Text coinText;
    public int coinCount = 0;

   
    public bool onGround;
    private bool wasGrounded;
    public AudioSource audioSource;
    public AudioSource desertMusic;
    private bool canFly = false;
    private bool isFlying = false; 
    private float flightSpeed = 0f;
    private float flightDuration = 3f; 
    public WinManager winscreen;
    private float flightTimeLeft = 0f;

    
    private Rigidbody2D rb; // Reference to the player's Rigidbody

    // Array to store audio clips

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSpeed = 6;
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
        animator = GetComponent<Animator>();
        desertMusic.Play();


    }
    void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (isFlying)
        {
           
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * flightSpeed); //velocity

           
            flightTimeLeft -= Time.deltaTime; //flight duration time decreases with time
            if (flightTimeLeft <= 0f) // after assigned time, flight ends/disabled
            {
                DisableFlight();
            }
        }
        else
        {
            
            if (IsGrounded()) // after flight dur, player will fall to ground
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); // vertical vel set to zero
            }
            
        }

        onGround = IsGrounded();


        movePlayer();
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }

        if (!wasGrounded && onGround) // Player has landed
        {
            animator.SetTrigger("hasLanded");
            animator.SetBool("isJumping", false);
        }
       

        wasGrounded = onGround;
    }
   

        private bool IsGrounded(){

        float rayLength=2f;
        LayerMask ground = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,rayLength,ground);
       
        return hit.collider!=null;
    }
    public void movePlayer()
    {
        // Get player input
        moveXdir = Input.GetAxis("Horizontal");
       
            // Update running animation based on movement and grounded state
            if (IsGrounded() && moveXdir != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
                
            }

            // Handle jumping
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", true);
                jumpPlayer();
            }

            // Flip player direction based on movement
            if (moveXdir < 0.0f && facingRight == true)
            {
                changeDirection();
                facingRight=false;
            }
            else if (moveXdir > 0.0f && facingRight == false)
            {
                changeDirection();
                facingRight=true;
            }

            // Apply player physics for movement
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveXdir * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        
    }


    void changeDirection(){


        
        facingRight=!facingRight;

        
        
    }

    void jumpPlayer(){

        if(IsGrounded() || IsOnObstacle()){

        
            GetComponent<Rigidbody2D>().AddForce ( Vector2.up * playerJumpIntensity);
            if(IsGrounded()){

                animator.SetBool("isJumping",true);
                
                
            }

            
        }
    }


    private IEnumerator SpeedBoost()
    {
        if (!isActive)
        {

            isActive = true;
            playerSpeed += 3;

            Debug.Log("Speed boost activated!");


            yield return new WaitForSeconds(10);


            playerSpeed -= 3;
            isActive = false;

            Debug.Log("Speed boost ended!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("door")){

            PlayerPrefs.SetInt("level",2);
                winscreen.showwin();
        }


        if (collision.CompareTag("speedboots"))
        {
            Debug.Log("Boots collected!");

            // Increase player speed
            StartCoroutine("SpeedBoost");

            // Destroy the boots object
            Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("coin"))
        {
            Debug.Log("Coin collected!");
            audioSource.Play();

            // Increase coin count
            coinCount += 10;

         
            // Destroy the coin object
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("quicksand"))
        {
            playerSpeed -= 2; // Reduce player speed by 2
            Debug.Log("Player entered quicksand! Speed reduced.");
        }



        if ( SceneManager.GetActiveScene().name=="Level5" && collision.CompareTag("PowerUp"))
        {
            FlightFeathers powerUp = collision.GetComponent<FlightFeathers>();
            if (powerUp != null)
            {
                StartCoroutine(ActivateFlight(powerUp.flightDuration, powerUp.flightSpeed));
            }
            Destroy(collision.gameObject);  // destroy after activation
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("quicksand"))
        {
            playerSpeed +=2; // Reset player speed to original
            Debug.Log("Player left quicksand! Speed restored.");
        }
    }

    private bool IsOnObstacle()
    {
        // Check if the player is on an obstacle, even though they are not technically "grounded"
        LayerMask obstacleLayer = LayerMask.GetMask("obstacle");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, obstacleLayer);
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);

        if (hit.collider !=null)
            Debug.Log("on obstacle");
        return hit.collider != null;
        
    }

    public void EnableFlight(float speed, float duration)
    {
        isFlying = true;
        flightSpeed = speed;
        flightTimeLeft = duration; 
    }

    public void DisableFlight()
    {
        isFlying = false;
        flightSpeed = 0f;  

        // after flight ends, gravity will affect player to fall 
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);  
        }
    }

    private IEnumerator ActivateFlight(float duration, float speed)
    {
        EnableFlight(speed, duration);
        yield return new WaitForSeconds(duration); 
        DisableFlight(); 
    }

  
}
