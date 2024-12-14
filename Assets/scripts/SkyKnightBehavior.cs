using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyKnightBehavior : MonoBehaviour
{

    public float detectionRange = 5f; // detection range
    public float attackCooldown = 2f; // Time between attacks
    public float attackRange = 1.5f; 
    public float patrolSpeed = 2f; // speed of patrol 
    public float moveSpeed = 2f; // speed at which the knight moves toward the player

    public Transform leftPoint; 
    public Transform rightPoint; 

    private Transform player;
    private bool isAttacking = false;
    private bool movingRight = true; 
    public float damageAmount = 10f;
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position); // cacculates dist to player

        // If the player is within detection range, engage
        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer();

            // If in attack range, attack
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
            else if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            Patrol();
        }
    }
    void MoveTowardsPlayer()
    {
        // move the knight towards the player's position
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    void Patrol()
    {
        // move left or right between the patrol points
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, patrolSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, rightPoint.position) < 0.1f)
            {
                movingRight = false;
                Flip(); // flip face direction
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, patrolSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void FacePlayer()
    {
        Vector3 currentScale = transform.localScale;

        if (player.position.x > transform.position.x)
        {
            // face right
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else
        {
            
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }


    void Flip()
    {
        // reverse the direction of the knight
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    System.Collections.IEnumerator AttackPlayer()
    {
        isAttacking = true;
        Debug.Log("sky knight is attacking");

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }


}
