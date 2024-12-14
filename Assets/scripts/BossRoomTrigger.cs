using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public GameObject finalBoss;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the final boss
            if (finalBoss != null)
            {
                finalBoss.SetActive(true);  // Enable boss object
            }
        }
    }
}
