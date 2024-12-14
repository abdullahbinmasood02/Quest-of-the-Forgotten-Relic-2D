using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandWorms : MonoBehaviour
{
    public int wormSpeed = 2;
    public int moveX = 1;
    public float rayLength = 10f;
    public float detectionRange = 4f;
    public float burrowDelay = 1f; // Time before the worm emerges
    private bool isActive = false;
    private bool isBurrowing = false;
    private Animator animator;
    
    private Rigidbody2D rb;
    private GameObject player;

    private bool facingRight = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DisableWorm();
    }

    void Update()
    {
        InvokeRepeating("CheckPlayerProximity", 0f, 0.1f);
      

        if (isActive && !isBurrowing)
        {
            moveWorm();
        }
    }

    void CheckPlayerProximity()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!isActive && !isBurrowing)
            {
                StartCoroutine(BurrowAndEmerge());
            }
        }
        else
        {
            if (isActive)
            {
                DisableWorm();
            }
        }
    }

    IEnumerator BurrowAndEmerge()
    {
        isBurrowing = true;

        // Trigger burrowing animation
        if (animator != null)
        {
            animator.SetBool("burrow", true); // Play the burrow animation
            Debug.Log("Burrow animation triggered!");
        }

        // Wait for the burrowing animation duration
        yield return new WaitForSeconds(burrowDelay);

        // Stop burrowing animation
        if (animator != null)
        {
            animator.SetBool("burrow", false); // Reset the animation
        }

        EnableWorm(); // Make the worm active

        isBurrowing = false;
    }

    void EnableWorm()
    {
        isActive = true;
        GetComponent<SpriteRenderer>().enabled = true; // Make it visible
        rb.simulated = true; // Enable physics
    }

    void DisableWorm()
    {
        isActive = false;
        GetComponent<SpriteRenderer>().enabled = false; // Make it invisible
        rb.simulated = false; // Disable physics
        rb.velocity = Vector2.zero; // Stop movement
    }

    void moveWorm()
    {
        // Cast ray to the right
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, rayLength, LayerMask.GetMask("player"));

        // Cast ray to the left
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("player"));

      

        // Determine which ray hits the player
        if (hitRight.collider != null)
        {
            moveX = 1; // Move right
        }
        else if (hitLeft.collider != null)
        {
            moveX = -1; // Move left
        }

        // Move the worm in the direction of the player
        rb.velocity = new Vector2(moveX, 0) * wormSpeed;

        // Ensure the worm is facing the direction of movement
        if (moveX > 0 && !facingRight)
        {
            changeDirection();
        }
        else if (moveX < 0 && facingRight)
        {
            changeDirection();
        }
    }

    void changeDirection()
    {
        facingRight = !facingRight;

        // Flip the worm's sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
