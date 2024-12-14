using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightFeathers : MonoBehaviour
{
    public float flightDuration = 3f; // Duration of temporary flight
    public float flightSpeed = 10f;   // Speed during flight

    private player player; // Reference to the player

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<player>();
            if (player != null)
            {
                player.EnableFlight(flightSpeed, flightDuration); // Pass speed and duration to the player
            }
            Destroy(gameObject); // Destroy power-up object
        }
    }
}
