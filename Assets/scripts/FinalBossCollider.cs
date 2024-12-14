using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCollider : MonoBehaviour
{
  
    private PlayerAttack playerAttackScript; //using playerattack script 

    void Start()
    {
        playerAttackScript = GetComponentInParent<PlayerAttack>();  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            Debug.Log("Sword hit detected on FinalBoss"); 
            playerAttackScript.OnSwordHit(other);
        }
        else
        {

                Debug.LogWarning("final boss collider destroyed ");
        }
    }
}

