using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Transform leftPoint;  
    public Transform rightPoint;
    public float patrolSpeed = 2f;
    public float attackRange = 3f;

    private Animator animator;
    private Transform player;
    private bool movingRight = true;
    public float attackCooldown = 2f; // time between attacks

    private FinalBossHealth bossHealth;
    private float lastAttackTime;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<FinalBossHealth>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        { Debug.Log("switchhing to attackplayer");
            AttackPlayer();
        }
        else
        {Debug.Log("switchhing to patrolling");
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("isPatrolling", true);
        animator.SetBool("isAttacking", false);

        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, patrolSpeed * Time.deltaTime);
            if (transform.position.x >= rightPoint.position.x)
            {        Flip();
                movingRight = false;}
        }
        else
        {  transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, patrolSpeed * Time.deltaTime);
            if (transform.position.x <= leftPoint.position.x)
            {    Flip();
                movingRight = true;
            }
        }
    }

    void AttackPlayer()
    {
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isAttacking", true);

        //triggring the attack anm
        animator.SetTrigger("AttackTrigger");

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            bossHealth.AttackPlayer(player.gameObject);
            lastAttackTime = Time.time;
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
    

