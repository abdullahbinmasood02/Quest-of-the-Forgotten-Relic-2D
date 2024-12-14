using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_clouds : MonoBehaviour
{
    public float speed = 2f; 
    public float moveDistance = 3f; 
    private Vector3 startPosition; 
    private bool movingRight = true; 

    void Start()
    {
        startPosition = transform.position; // Saving the initial position
    }

    void Update()
    {
        // we will calculate the next position
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= startPosition.x + moveDistance)
                movingRight = false;
        }
        else
        {      transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= startPosition.x - moveDistance)
                movingRight = true;
        }
    }
}
