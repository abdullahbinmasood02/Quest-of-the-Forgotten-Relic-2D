using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage = 25f;  //damage by sword
    private Animator animator;
    public Collider2D swordCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponentInChildren<Collider2D>();  //child obj
        swordCollider.enabled = false; // disable sword collider initially so it doesnot collide with other obj
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical")) 
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("AttackTrigger");  
        swordCollider.enabled = true;  // enable the sword collider to detect collisions
        StartCoroutine(DisableSwordCollider()); // disable after attack anm ends
    }
  
    void OnTriggerEnter2D(Collider2D other)
    {
        OnSwordHit(other);
    }
   
    private IEnumerator DisableSwordCollider()
    {
        yield return new WaitForSeconds(0.5f); 
        swordCollider.enabled = false;
    }

    public void OnSwordHit(Collider2D other) //other-> collider of other objs that this sword will interact with
    {
        if (other.CompareTag("finalboss"))
        { Debug.Log("final boss hit by sword ");
            FinalBossHealth boss = other.GetComponent<FinalBossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage);  //calling damage function to reduce boss health
                Debug.Log("Final boss health decreased due to attack");
            }else {Debug.Log("boss is null");}
        }
    }
}
