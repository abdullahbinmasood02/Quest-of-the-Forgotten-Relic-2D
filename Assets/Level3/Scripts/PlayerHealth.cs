using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool died;
    public float healthAmount = 100f;   
    public float currentHealth;   
    public Slider healthBar;   
    private bool isShieldActive = false;   
    public AudioSource audioSource;   
    public AudioClip deathSound;  
    public AudioClip batHitSound;     
    public AudioClip icicleHitSound;   
    


    void Start()
    {
        died = false;
        currentHealth = healthAmount;
    }

    void Update()
    {
        if (gameObject.transform.position.y < -6)
        {
            Die();
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    //minus heatlht when colliding with bat
    public void TakeDamage(float damageAmount)
    {
        if (isShieldActive)
        {
            Debug.Log("Shield is active! No damage be taken.");
            return;  
        }

        currentHealth -= damageAmount;
        Debug.Log("Player got damage, Current health: " + currentHealth);
        audioSource.PlayOneShot(batHitSound);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //collisions with bat
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bat"))
        {
            Debug.Log("Bat hit player!");
            TakeDamage(10f);  
        }
        //collisions with icicle
        if (collision.gameObject.CompareTag("icicle"))
        {
            Debug.Log("Player hit by icicle!");
            TakeDamage(5f);  
        }
    }

    //activate shield
    public void ActivateShieldImmunity(float duration)
    {
        isShieldActive = true;
        Debug.Log("Shield activated! Immunity granted for " + duration + " seconds.");
        StartCoroutine(DeactivateShieldImmunityAfterTime(duration));
    }

    private IEnumerator DeactivateShieldImmunityAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isShieldActive = false;
        Debug.Log("Shield immunity expired. Player can take damage now.");
    }

    //death methods
    private IEnumerator DieAnimDelay()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        audioSource.PlayOneShot(deathSound);   
        StartCoroutine(DieAnimDelay());

    }
}
