using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelOneHealth : MonoBehaviour
{

    public bool died;
    public float healthAmount = 100f;
    public float currentHealth;
    public float damagePerSecond = 10f;
    public Slider healthBar;
    private Animator animator;
    public TextMeshProUGUI healthText;
    public GameUI gameUI;
    // Start is called before the first frame update
    void Start()
    {
        died = false;

        damagePerSecond = 10f;
        healthAmount = 100f;
        currentHealth = healthAmount;

        if (healthBar != null)
        {
            healthBar.maxValue = healthAmount;
            healthBar.value = currentHealth;
        }
        if (healthText != null)
        {
            healthText.text = Mathf.Floor(currentHealth).ToString();
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
        if (gameObject.transform.position.y < -6)
            Die();

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        if (healthText != null)
        {
            healthText.text = Mathf.Floor(currentHealth).ToString();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        // Check if the colliding object is tagged as "Enemy"
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Reduce this object's health over time
            Debug.Log("Current health: " + currentHealth);
            currentHealth = Mathf.Max(currentHealth - (damagePerSecond * Time.deltaTime), 0);
            animator.SetTrigger("isHurt");
            gameUI.playSFX("Hurt");
            // Check if health has dropped to or below 0
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        //animator.SetBool("isHurting",false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("health"))
        {
            Debug.Log("Health collected! " + collision.name);
            gameUI.playSFX("Collect");

            // Increase player speed
            currentHealth = Mathf.Min(currentHealth + 25, 100);

            // Destroy the key object
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator dieAnimDelay()
    {


        animator.SetBool("hasDied", true);
        gameUI.playSFX("Hurt");
        yield return new WaitForSeconds(2);
        Destroy(gameObject); // Destroy this object
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameUI.GameLose();
    }
    private void Die()
    {

        StartCoroutine("dieAnimDelay");

    }
}
