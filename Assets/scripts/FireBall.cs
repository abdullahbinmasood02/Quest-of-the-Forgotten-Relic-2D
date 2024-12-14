using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f; // Speed of the fireball
    public int damage = 10;  // Damage dealt by the fireball
    public Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        damage=10;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Fireball moves to the right by default
    }

    private void OnTriggerEnter2D(Collider2D collision)
{

    Debug.Log("Fireball collided with: " + collision.gameObject.name);

    // Check if the fireball hits the player
    if (collision.CompareTag("Player"))
    {
        PlayerHealth4 playerHealth = collision.GetComponent<PlayerHealth4>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Reduce player's health
            Debug.Log("Player took damage. Current Health: " + playerHealth.currentHealth);
            animator.SetTrigger("isHurt");
        }

        Destroy(gameObject); // Destroy the fireball after hitting the player
    }

    // Destroy the fireball if it hits any other object
    if (collision.CompareTag("Ground") || collision.CompareTag("obstacle"))
    {
        Destroy(gameObject);
    }
}

}
