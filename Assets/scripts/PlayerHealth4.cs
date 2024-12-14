using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth4 : MonoBehaviour
{
    public int maxHealth = 100;    // Maximum health
    public int currentHealth;      // Current health
    public Slider healthBar;       // Health bar UI slider
    private Animator animator;     // Animator for handling animations
    private bool died = false;     // To ensure Die() runs only once
    private bool isInvincible = false; // Tracks invincibility status
    public float invincibilityDuration = 5f; // Duration of invincibility
    public GameOverManager gameover;

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize health bar if assigned
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        // Get the animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update the health bar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Handle player falling below a certain level (optional)
        if (transform.position.y < -6 && !died)
        {
            Die();
        }
    }
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Lava") && !died) // Check if it's lava and player is not already dead
    {
        if (!isInvincible) // Only die if not invincible
        {
            Die(); // Call the Die method to handle death
        }
        else
        {
            Debug.Log("Player touched lava but is invincible!");
        }
    }
    else if (collision.CompareTag("Finish")) // Check if it's the finish object
    {
        Debug.Log("Player reached the finish line! Loading level5...");
        SceneManager.LoadScene("level5"); // Switch to the next scene
    }
}



    public void TakeDamage(int damage)
    {
        if (died || isInvincible) return; // Prevent damage when dead or invincible

        currentHealth -= damage;

        // Update health bar after taking damage
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Trigger hurt animation
        if (animator != null)
        {
            animator.SetTrigger("isHurt");
        }

        // Check if health is 0 or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (died) return; // Prevent multiple calls
        died = true;

        Debug.Log("Player Died!");

        // Trigger death animation
        if (animator != null)
        {
            animator.SetBool("hasDied", true);
        }

        // Start coroutine to handle death animation delay
        StartCoroutine(DieAnimationDelay());
    }

    private IEnumerator DieAnimationDelay()
    {
        yield return new WaitForSeconds(2); // Wait for death animation to complete

        // Reload the current scene
        gameover.ShowGameOver();    }

public void ActivateInvincibility()
{
    if (!isInvincible)
    {
        StartCoroutine(InvincibilityCoroutine());
    }
}

private IEnumerator InvincibilityCoroutine()
{
    isInvincible = true; // Set invincibility to true

    // Change player color to indicate invincibility
    SpriteRenderer sprite = GetComponent<SpriteRenderer>();
    if (sprite != null) sprite.color = Color.yellow;

    yield return new WaitForSeconds(invincibilityDuration); // Wait for the duration

    // Revert player color back to normal
    if (sprite != null) sprite.color = Color.white;
    isInvincible = false; // Revert invincibility
}

}
