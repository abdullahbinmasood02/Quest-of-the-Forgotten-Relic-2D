using UnityEngine;

public class IcicleFall : MonoBehaviour
{
    public float fallSpeed = 5f;   
    private Rigidbody2D rb;
    private bool isFalling = false;   
    public GameObject player;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;  //kinematic so icicle doesnt fall immediately
    }

    void Update()
    {
        //check if player is underneath icicle and hasnt started falling yet
        if (!isFalling && IsPlayerUnderneath())
        {
            Debug.Log("Player is underneath icicle!");
            StartFalling();
        }

        //icicle is falling apply gravity
        if (isFalling)
        {
            rb.isKinematic = false;  //disable kinematic to allow it to fall
            rb.velocity = new Vector2(0, -fallSpeed);  //velocity to make icicle fall
        }
    }

    bool IsPlayerUnderneath()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null) //player in scene
        {
            //positions of icicle and player
            Vector2 playerPosition = playerObject.transform.position;
            Vector2 iciclePosition = transform.position;

            //if player is below icicle vertically and within a certain horizontal range
            if (playerPosition.y < iciclePosition.y && Mathf.Abs(playerPosition.x - iciclePosition.x) < 2f)
            {
                return true;  //under icicle
            }
        }

        return false;  //not underneath icicle
    }

    void StartFalling()
    {
        isFalling = true; 
    }

    //destroy icicle when it collides with player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  //check if it collided 
        {
            Debug.Log("Icicle collided with player!");
            Destroy(gameObject);  
        }

        else if (collision.gameObject.CompareTag("ground"))  //collided with ground
        {
            Debug.Log("Icicle hit ground!");
            Destroy(gameObject); 
        }
        else if (collision.gameObject.CompareTag("platform"))  // with platform
        {
            Debug.Log("Icicle hit platform!");
            Destroy(gameObject);  
        }
        else if (collision.gameObject.CompareTag("bat"))  //with bat
        {
            Debug.Log("Icicle hit bat!");
            Destroy(gameObject); 
        }

    }
}
