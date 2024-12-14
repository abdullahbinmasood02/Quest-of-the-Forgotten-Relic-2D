using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float batSpeed = 4f;  
    public float diveRange = 5f;  
    private GameObject player; 
    private Rigidbody2D rb;  
    private bool isDiving = false; 
    private Vector3 originalPosition; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");  
        originalPosition = transform.position; //original position of bat
        //disable gravity so that the bat does not fo=all immediately
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (player != null) 
        {
            CheckDiveCondition();
        }

        if (transform.position.y <= -1f) //-1f y position se neeche for it to reset its posisiotn
        {
            ResetBatPosition(); 
        }

    }

    void CheckDiveCondition()
    {
        //if players x position is in range
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < diveRange && !isDiving)
        {
            DiveAtPlayer();
        }
    }

    void DiveAtPlayer()
    {
        isDiving = true; //diving state for bat

        //disable gravity when diving
        rb.gravityScale = 0;

        //calculate direction to player - diagonal direction
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

        //bats velocity to move towards player
        rb.velocity = directionToPlayer * batSpeed;

        //if bat hits player
        if (Vector2.Distance(transform.position, player.transform.position) < 0.5f)
        {
            //bat hit player
            Debug.Log("Bat hit player!");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bat hit player through collision!");
        ResetBatPosition();

        }
    }

    void ResetBatPosition()
    {
        //reset bat position actual top location
        transform.position = originalPosition;
        //reset velocity + stop diving
        rb.velocity = Vector2.zero;
        //reset gravity 
        rb.gravityScale = 0;
        isDiving = false; 
    }

}
