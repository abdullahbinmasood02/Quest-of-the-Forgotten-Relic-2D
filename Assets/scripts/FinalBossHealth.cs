using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FinalBossHealth : MonoBehaviour
{
    public float maxHealth = 200f;  // maximum health for the boss (double as compared to the player which is 100)
    private float currentHealth;   // current
    public Slider healthBar;      
    public float attackDamage = 37.5f; 
    public WinManager winscreen;

    void Start()
    {
        currentHealth = maxHealth; 

        if (healthBar != null)
        {   healthBar.maxValue = maxHealth; // setting health bar with max value
            healthBar.value = currentHealth; 
        }
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // updation
        }

        if(maxHealth==0){
         
            winscreen.showwin();
        }
    }

    
    public void TakeDamage(float damage)
    {
      ///  currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        currentHealth -= damage;
        Debug.Log("Final boss is damaged ");
        if (currentHealth <= 0)
        {
           
            Die();
        }
       
    }

    private void Die()
    {
        
        Debug.Log("final boss defeated!");
        Destroy(gameObject);
    }

    public void AttackPlayer(GameObject player)
    {
        health playerHealth = player.GetComponent<health>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth -= attackDamage;
            Debug.Log("Player got hit by final boss");
            if (playerHealth.currentHealth <= 0)
            {
                playerHealth.Die();
            }
        }
    }
}
