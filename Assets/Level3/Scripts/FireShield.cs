using System.Collections;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    public float shieldDuration = 5f;   
    private bool isShieldActive = false;  
    private SpriteRenderer playerRenderer;     
    private PlayerHealth playerHealth; 
    public AudioSource audioSource;   
    public AudioClip fireShieldSound;  

    private void Start()
    {
        playerRenderer = FindObjectOfType<PlayerHealth>().GetComponent<SpriteRenderer>();
        playerHealth = FindObjectOfType<PlayerHealth>();        //PlayerHealth script
    }

    //player collides with fire shield
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isShieldActive)
        {
            Debug.Log("Shield collected!");
            audioSource.Stop();
            audioSource.PlayOneShot(fireShieldSound);   
            ActivateShield(collision.gameObject); //Activate fire shield
        }
    }

    public bool IsShieldActive()
    {
        Debug.Log("Checking if shield is active: " + isShieldActive);
        return isShieldActive;
    }

    //activate shield: make player orange and givem shiedl
    private void ActivateShield(GameObject player)
    {
        isShieldActive = true;
        playerRenderer.color = new Color(1f, 0.647f, 0f); // Make player orange
        Destroy(gameObject);

        //player be immune to damage
        playerHealth.ActivateShieldImmunity(shieldDuration);
        StartCoroutine(RevertColorAfterTime(shieldDuration)); //revert player's color and reset shield
    }

    //revert color after shield ends
    private IEnumerator RevertColorAfterTime(float time)
    {
        //wait for shield time
        yield return new WaitForSeconds(time);

        //turnback player color to blue /--trials 
        playerRenderer.color = new Color(0f, 0f, 1f); 
        isShieldActive = false;
        Debug.Log("shield expired - color reverted to blue.");
    }
}
