using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
public class LevelOnePlayer : MonoBehaviour
{
    public int playerSpeed = 4;
    private bool facingRight = false;
    public int playerJumpIntensity = 370;
    private float moveXdir;
    private Animator animator;
    public Animator doorAnimator;
    //private bool isActive = false;
    public TextMeshProUGUI keyText;
    public bool onGround;
    private bool wasGrounded;
    public int keysCollected = 0;
    public int keysToCollect = 3;
    public WinManager winscreen;
    //private bool doorOpened = false;

    public GameUI gameUI;

    void Start()
    {
        if (keyText != null)
        {
            keyText.text = keysCollected.ToString();
        }
        animator = GetComponent<Animator>();
        if (gameUI == null)
        {
            gameUI = FindObjectOfType<GameUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        onGround = IsGrounded();


        movePlayer();
        if (keyText != null)
        {
            keyText.text = keysCollected.ToString();
        }

        if (!wasGrounded && onGround) // Player has landed
        {
            animator.SetTrigger("hasLanded");
            animator.SetBool("isJumping", false);
        }

        wasGrounded = onGround;
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
        //Debug.Log("Collision Detected");
        // Check if the collided object has the tag "key"
        if (collision.CompareTag("key"))
        {
            Debug.Log("Key collected! " + collision.name);

            // Increase player speed
            keysCollected++;
            gameUI.playSFX("Collect");

            // Destroy the key object
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("door"))
        {
            Debug.Log("Reached Door! " + collision.name);

            if (keysCollected >= keysToCollect)
            {
                // Trigger door animation
                //doorOpened = true;
                PlayerPrefs.SetInt("level", 1);
                StartCoroutine("doorAnimDelay");
            }
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
    private IEnumerator doorAnimDelay()
    {
        doorAnimator.SetTrigger("doorOpen");
        gameUI.playSFX("Door");
        yield return new WaitForSeconds(2);
        winscreen.showwin();
    }

}
