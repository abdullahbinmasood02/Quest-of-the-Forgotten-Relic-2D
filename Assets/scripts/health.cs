using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class health : MonoBehaviour
{

    public bool died;
    public float healthAmount=100f;
    public float currentHealth;
    public float damagePerSecond=20f;
    public AudioClip[] hurtSounds;
    public GameOverManager gameover;
    public Slider healthBar;
    private Animator animator;
    private AudioSource audioSource1;
    private GameOverManager gameOverManager;
    // Start is called before the first frame update
    void Start()
    {
       
        died =false;
        audioSource1 = gameObject.AddComponent<AudioSource>();

        if (audioSource1 == null)
        {
            Debug.LogError("AudioSource1 was not added properly!");
        }
        audioSource1.playOnAwake = false;

        damagePerSecond =20f;
        healthAmount=100f;
        currentHealth=healthAmount;

        if (healthBar != null)
        {
            healthBar.maxValue = healthAmount;
            healthBar.value = currentHealth;
        }
        animator = GetComponent<Animator>();
        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y<-6)
            Die();

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

       
            

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            
            SkyKnightBehavior knight = collision.gameObject.GetComponent<SkyKnightBehavior>();

            if (knight != null)
            {
                currentHealth -= knight.damageAmount * Time.deltaTime; // Using knight's damage value
            }
            else
            {
                currentHealth -= damagePerSecond * Time.deltaTime; 
            }

            animator.SetTrigger("isHurt");
            PlayHurtAudio();

            
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


    void PlayHurtAudio()
    {
        if (hurtSounds.Length > 0)
        {
            Debug.Log("in function");
            StartCoroutine("PlayHurtAudioDelay");
        }
    }

    private IEnumerator PlayHurtAudioDelay()
    {

        Debug.Log("in coroutine");
        int randomIndex = Random.Range(0, hurtSounds.Length);
        Debug.Log(randomIndex);
        audioSource1.clip = hurtSounds[randomIndex];
        audioSource1.Play();
        yield return new WaitForSeconds(audioSource1.clip.length);
    }
    private IEnumerator dieAnimDelay(){


        animator.SetBool("hasDied",true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject); // Destroy this object
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Die()
    {
        
       
        
        gameover.ShowGameOver();

    }

    public void RetryLevel()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    private IEnumerator loadMenuwithDelay()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("menu");
    }

    public void LoadMainMenu()
    {
        //Time.timeScale = 1f; // Resume the game
        StartCoroutine("loadMenuwithDelay");
    }
}
