using UnityEngine;

public class TorchBlock : MonoBehaviour
{
    public GameObject torchPrefab;
    private bool isMelted = false;  //melts only once

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if player has collided with block
        if (collision.gameObject.CompareTag("Player"))
        {
            FireShield fireShield = collision.gameObject.GetComponent<FireShield>();

            //check if player has FireShield component and if it is active
          //  if (fireShield != null && fireShield.IsShieldActive())
            if(true)
            {
                Debug.Log("Fire shield is active, melting block...");
                MeltBlock();
            }
            else
            {
             //   Debug.Log("Fire shield is not active, cannot melt block.");
            }
        }
    }

    private void MeltBlock()
    {
        if (!isMelted)
        {
            isMelted = true;
            Debug.Log("replacing ice block with torch");
            //swap iceblock wiht torch 
            Instantiate(torchPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            ChangeBackgroundColor();
        }
    }

    private void ChangeBackgroundColor()
    {
        //findingall background with "bcg" tag
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("bcg");

        if (backgrounds.Length > 0)
        {
            foreach (GameObject bg in backgrounds)
            {
                //change the color of each background to white - original
                SpriteRenderer bgRenderer = bg.GetComponent<SpriteRenderer>();
                if (bgRenderer != null)
                {
                    bgRenderer.color = new Color(1f, 1f, 1f);  // RGB for white
                    Debug.Log("bcg color changed to white.");
                }
                else
                {
                    Debug.LogWarning("bcg object does not have a SpriteRenderer component.");
                }
            }
        }
        else
        {
            Debug.LogError("No bcg objects found with the 'bcg' tag!");
        }
    }
}
