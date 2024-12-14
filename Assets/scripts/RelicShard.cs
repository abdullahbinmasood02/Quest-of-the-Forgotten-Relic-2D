using UnityEngine;

public class RelicShard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player collects the shard
        {
            GameManager4.Instance.CollectShard(); // Notify the GameManager
            Destroy(gameObject); // Remove the shard from the game
        }
    }
}
