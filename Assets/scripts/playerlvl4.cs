using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerlvl4 : MonoBehaviour
{
    public int playerSpeed = 8;
    private bool facingRight = false;
    public int playerJumpIntensity = 1200;
    private float moveXdir;
    private Animator animator;
    private bool isActive = false;
    public Text coinText;
    public int coinCount = 0;
    public bool onGround;
    public WinManager winscreen;
    private bool wasGrounded;
    private Vector3 originalScale; // To store the player's initial scale

    void Start()
    {
        playerSpeed = 8;
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        animator = GetComponent<Animator>();

        // Save the player's original scale to prevent unintended changes
        originalScale = transform.localScale;
    }

    void Update()
    {
        onGround = IsGrounded();
        movePlayer();

        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }

        if (!wasGrounded && onGround) // Player has landed
        {
            animator.SetTrigger("hasLanded");
            animator.SetBool("isJumping", false);
        }

        if(Input.GetKeyDown(KeyCode.W)){

            Debug.Log("pressed");
        }

        wasGrounded = onGround;
       // Debug.Log("h");
    }

    void LateUpdate()
    {
        // Ensure the player's scale remains consistent
        transform.localScale = originalScale;
    }

    private IEnumerator SpeedBoost()
    {
        if (!isActive)
        {
            isActive = true;
            playerSpeed += 4;

            Debug.Log("Speed boost activated!");

            yield return new WaitForSeconds(10);

            playerSpeed -= 3;
            isActive = false;

            Debug.Log("Speed boost ended!");
        }
    }

    private bool IsGrounded()
    {
        float rayLength = 2f;
        LayerMask ground = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, ground);

        return hit.collider != null;
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
        if (moveXdir < 0.0f && facingRight == false)
        {
            changeDirection();
        }
        else if (moveXdir > 0.0f && facingRight == true)
        {
            changeDirection();
        }

        // Apply player physics for movement
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveXdir * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void changeDirection()
    {
        facingRight = !facingRight;

        Vector2 local = gameObject.transform.localScale;
        local.x *= -1;

        transform.localScale = local;
    }

    void jumpPlayer()
    {
        if (IsGrounded() || IsOnObstacle())
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpIntensity);
            if (IsGrounded())
            {
                animator.SetBool("isJumping", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "speedboots"
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

            // Increase coin count
            coinCount += 10;

            // Optionally log the updated coin count
            Debug.Log("Total Coins: " + coinCount);

            // Destroy the coin object
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("door")){

            PlayerPrefs.SetInt("level", 4);
            winscreen.showwin();
        }
    }

    private bool IsOnObstacle()
    {
        // Check if the player is on an obstacle, even though they are not technically "grounded"
        LayerMask obstacleLayer = LayerMask.GetMask("obstacle");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, obstacleLayer);
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);

        if (hit.collider != null)
            Debug.Log("on obstacle");
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            Debug.Log("Player is on the moving platform.");
            // Parent the player to the platform without altering its scale
            transform.SetParent(collision.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            Debug.Log("Player left the moving platform.");
            // Unparent the player to detach from the platform
            transform.SetParent(null, true);
        }
    }
}
