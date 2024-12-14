using System.Collections;
using UnityEngine;

public class PowerUpOrb : MonoBehaviour
{
    public AudioClip powerUpSound; // The sound effect to play
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the orb
        if (other.CompareTag("Player"))
        {
            PlayerHealth4 playerHealth = other.GetComponent<PlayerHealth4>();

            if (playerHealth != null)
            {
                playerHealth.ActivateInvincibility(); // Activate invincibility on the player

                // Play the power-up sound effect
                if (powerUpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(powerUpSound);
                }

                // Destroy the power-up orb after a short delay to allow the sound to play
                Destroy(gameObject, 0.1f); // Adjust delay if needed
            }
        }
    }
}
