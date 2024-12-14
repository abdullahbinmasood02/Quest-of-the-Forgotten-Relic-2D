using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this for UI reference
using UnityEngine.SceneManagement; // Required for scene management

public class Player : MonoBehaviour
{
    public int playerSpeed = 6;
    public bool facingRight = true;
    public int playerJumpIntensity = 700;
    private float moveXdir;
    public bool onGround;
    public int coinCount = 0;  
    public Text coinText;
    private Rigidbody2D rb;
    public float constantSpeed = 2f;  //player ko slide karwane k liye - for the icy effect
    public AudioSource audioSource; 
    public AudioClip coinSound;
    private Animator animator;
    public WinManager winscreen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        if (audioSource == null)
        audioSource = GetComponent<AudioSource>();

        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
        animator = GetComponent<Animator>();
        onGround = checkIfGrounded();
        animator.SetBool("isJumping", false); //start state is not jumping
        animator.SetBool("isRunning", false); //start state is idle - more like sliding
       
      

    }

    void Update()
    {
        movePlayer();
        applyConstantMotion();  
    }
     bool checkIfGrounded()//check agar ground par hai ya nahi
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("ground"));
        return hit.collider != null;
    }

    public void movePlayer()
    {
        moveXdir = Input.GetAxis("Horizontal"); //left right

        //flip player direction right left
        if (moveXdir < 0.0f && facingRight == true)
        {
            changeDirection();
            animator.SetBool("isRunning", true);
            facingRight = false;
        }
        else if (moveXdir > 0.0f && facingRight == false)
        {
            changeDirection();
            animator.SetBool("isRunning", true);
            facingRight = true;
        }

        //physics for movement - chalne k liye
        rb.velocity = new Vector2(moveXdir * playerSpeed, rb.velocity.y);

        //jumping aur uske animations
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", true);
            jumpPlayer();
        }
    }

    //flipping direction
    void changeDirection()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void jumpPlayer()
    {
        rb.AddForce(Vector2.up * playerJumpIntensity);
        onGround = false; //jab jump karega tou hawa me hoga
        animator.SetBool("isJumping", true); //jumping state
    }

    void OnCollisionEnter2D(Collision2D collision) //sirf collide kare
    {
        Debug.Log("Collided with " + collision.collider.name);

        //collision w/ ground or platform
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("platform"))
        {
            onGround = true;
            animator.SetBool("isJumping", false); //reset jumping state neeche ane k baad
            updateAnimationState();
        }

    }
    void OnCollisionStay2D(Collision2D collision) // ussi position me rahe agar
    {
        //collision w/ ground or platform
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("platform"))
        {
            onGround = true;
            animator.SetBool("isJumping", false); //reset jumping state neeche ane k baad
            updateAnimationState(); // Update the animation state
        }
    }

    void OnCollisionExit2D(Collision2D collision) //khatam
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("platform"))
        {
            onGround = false;
        }
    }

    //voin collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision with coin
        if (collision.CompareTag("coin"))
        {
            Debug.Log("Coin collected!");
            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);  // Play the sound effect once
            }

            //increase coin count
            coinCount += 10;

            //screen psr likhna
            if (coinText != null)
            {
                coinText.text = coinCount.ToString();
            }

            //destroy coin
            Destroy(collision.gameObject);
        }

        //end of game trigger
        if (collision.CompareTag("end"))
        {
            //show "You Won!" panel
           PlayerPrefs.SetInt("level", 3);
                winscreen.showwin();
        }
    }


    // Apply constant motion to the player
    void applyConstantMotion()
    {
        //if you dont move left ya right tou jis bhi direction me ho, it moves in that
        if (moveXdir == 0f)
        {
            rb.velocity = new Vector2(constantSpeed * (facingRight ? 1 : -1), rb.velocity.y);
        }
    }

    void updateAnimationState()
    {
        if (moveXdir != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    void RestartLevel()
    {
        //restart current level after showing win panel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
