using UnityEngine;

public class FrozenBlock : MonoBehaviour
{
    public GameObject woodenBoxPrefab; 
    private bool isMelted = false;      

    private void OnTriggerEnter2D(Collider2D collision)
    {
    //check for collisin
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLISION WITH PLAYER!");
            //if player has an active fire shield
             FireShield fireShield = collision.gameObject.GetComponent<FireShield>();
             //if (fireShield != null && fireShield.IsShieldActive())
             if (true) // forcing condition to alwways trigger- debug karne k liye kiya hai baad me remove yaad se
             {
                 MeltBlock();
             }
         }
     }

  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLISION WITH PLAYER!");

            FireShield fireShield = collision.gameObject.GetComponent<FireShield>();

            if (fireShield == null)
            {
                Debug.LogError("FireShield component not found on player!");
                return;
            }

            if (fireShield.IsShieldActive())
            {
                Debug.Log("Fire shield is active!");
                MeltBlock();
            }
            else
            {
                Debug.Log("Fire shield is NOT active!");
            }
        }
    }
*/

    private void MeltBlock()
    {
        if (!isMelted)
        {
            isMelted = true;
            Debug.Log("MeltBlock() called!");

            //replace ice block with wooden box
            Instantiate(woodenBoxPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            Debug.Log("ice block melted and replaced with wooden box");
        }
    }
}