using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGolem : MonoBehaviour
{
    public GameObject fireballPrefab; // Fireball prefab
    public Transform firePoint; // Fireball spawn position
    public float detectionRange = 10f; // Range to detect the player
    public float fireRate = 2f; // Time between fireballs

    private GameObject player; // Reference to the player
    private bool isShooting = false; // Whether the golem is currently shooting

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Start shooting if the player is within range
            if (distanceToPlayer <= detectionRange && !isShooting)
            {
                StartCoroutine(ShootFireballs());
            }

            // Stop shooting if the player moves out of range
            if (distanceToPlayer > detectionRange && isShooting)
            {
                StopCoroutine(ShootFireballs());
                isShooting = false;
            }
        }
    }

    private IEnumerator ShootFireballs()
    {
        isShooting = true;

        while (true)
        {
            // Instantiate the fireball
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

            // Destroy fireball after a certain time to prevent memory leaks
            Destroy(fireball, 5f);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
