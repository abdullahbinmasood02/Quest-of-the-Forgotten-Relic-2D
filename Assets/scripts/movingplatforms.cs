using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingplatforms : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints for the platform
    public float speed = 100f;       // Movement speed
    private int currentWaypointIndex = 0;  // Track the current waypoint

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Move platform towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // Check if the platform has reached the current waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Move to the next waypoint, looping back to the start
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
