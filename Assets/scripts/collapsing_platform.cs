using System.Collections;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public float collapseDelay = 0.2f; 
    public float destroyDelay = 3f; 

    private Rigidbody2D rb2d; 
  
    private bool isCollapsing = false;

    void Start()
    {
       
        rb2d = GetComponent<Rigidbody2D>();
        collapseDelay = 0.2f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollapsing)
        {
            StartCoroutine(CollapsePlatform());
        }
    }

    private IEnumerator CollapsePlatform()
    {
        isCollapsing = true;

        // Shake effect
        Vector3 originalPosition = transform.position;
        float shakeDuration = collapseDelay;
        float shakeMagnitude = 0.05f;

        while (shakeDuration > 0)
        {
            transform.position = originalPosition + (Vector3)(Random.insideUnitCircle * shakeMagnitude);
            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;

        // Enable gravity
        if (rb2d != null)
        {
            rb2d.isKinematic = false;
            rb2d.gravityScale = 1;
        }
       

        // Destroy the platform after a delay
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

}