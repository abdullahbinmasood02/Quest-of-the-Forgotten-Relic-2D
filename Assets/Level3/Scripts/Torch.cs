using UnityEngine;

public class Torch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player collides with torch
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collected torch!");

            //change player color to white
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(gameObject);
        }
    }
}
