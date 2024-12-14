using UnityEngine;
using UnityEngine.UI;

public class GameManager4 : MonoBehaviour
{
    public static GameManager4 Instance; // Singleton instance

    private int relicShardsCollected = 0; // Tracks collected shards
    public int totalShards = 3;          // Total shards needed to restore the bridge
    public Text shardsUI;                // UI element to display shard progress
    public Animator bridgeAnimator;      // Animator for the bridge restoration animation
    public GameObject ancientBridge;     // Reference to the bridge object
    public Animator panelAnimator;
    public Animator cameraAnimator;      // Reference to the Main Camera's Animator
    public GameObject player;            // Reference to the player GameObject
    public Text introText;               // Reference to the intro text UI
    public float introTextDuration = 3f; // Duration to display the intro text
    public float introDuration = 12f;    // Duration of the intro animation
    public float cameraReturnDelay = 3f; // Time to return camera to player after restoration

    private cameralvl4 cameraScript;         // Reference to the custom camera script
public AudioSource audioSource;      // Reference to the AudioSource component
    public AudioClip relicCollectSound;  // The sound effect for collecting relics
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Find the custom camera script
        cameraScript = Camera.main.GetComponent<cameralvl4>();

        // Disable the camera script during the intro
        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }

        // Disable player movement during the intro
        if (player != null)
        {
            player.GetComponent<playerlvl4>().enabled = false;
        }

        // Play the intro animation
        if (cameraAnimator != null)
        {
            cameraAnimator.Play("level4intro", 0, 0); // Play intro animation from the start
        }

        // Show the intro text after the animation ends
        Invoke("ShowIntroText", introDuration);

        // Start the game after the text disappears
        Invoke("StartGame", introDuration + introTextDuration);
    }

    private void ShowIntroText()
    {
        Debug.Log("Showing intro text...");

        if (introText != null)
        {
            introText.gameObject.SetActive(true); // Display the text
            Invoke("HideIntroText", introTextDuration); // Hide the text after the duration
        }
    }

    private void HideIntroText()
    {
        Debug.Log("Hiding intro text...");

        if (introText != null)
        {
            introText.gameObject.SetActive(false); // Hide the text
        }
    }

    private void StartGame()
    {
        Debug.Log("Starting game...");

        // Enable the camera script after the intro
        if (cameraScript != null)
        {
            cameraScript.enabled = true;
        }

        // Enable player movement
        if (player != null)
        {
            player.GetComponent<playerlvl4>().enabled = true;
        }

        Debug.Log("Level started!");
    }

   public void CollectShard()
    {
        relicShardsCollected++;
        UpdateShardUI();

        // Play the relic collection sound effect
        if (audioSource != null && relicCollectSound != null)
        {
            audioSource.PlayOneShot(relicCollectSound); // Play the sound effect
        }

        if (relicShardsCollected >= totalShards)
        {
            StartBridgeRestoration();
        }
    }

    private void UpdateShardUI()
    {
        if (shardsUI != null)
        {
            shardsUI.text = $"{relicShardsCollected}/{totalShards} Shards Collected";
        }
    }

    private void StartBridgeRestoration()
    {
        Debug.Log("All relic shards collected! Moving camera and restoring the bridge...");

        // Disable player movement and camera script temporarily
        if (player != null)
        {
            player.GetComponent<playerlvl4>().enabled = false;
        }
        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }

        // Play the camera animation to move to the bridge
        if (cameraAnimator != null)
        {
            cameraAnimator.Play("CameraToBridge", 0, 0); // Trigger camera animation
        }

        // Delay the restoration animation slightly to synchronize with the camera
        Invoke("RestoreBridge", 1f); // Adjust the delay if necessary
    }

    private void RestoreBridge()
    {
        Debug.Log("All relic shards collected! Playing bridge restoration animation...");

        // Play the bridge restoration animation
        if (bridgeAnimator != null)
        {
            bridgeAnimator.Play("bridgerestore"); // Trigger the animation
        }

        // Activate the bridge GameObject
        if (ancientBridge != null)
        {
            ancientBridge.SetActive(true);
        }

        // Trigger the panel animation
        Invoke("SlideInPanel", 0.5f); // Adjust the delay as needed
    }
private void SlideInPanel()
{
    Debug.Log("Sliding in the panel...");

    if (panelAnimator != null)
    {
        panelAnimator.SetTrigger("SlideInOut"); // Trigger the slide-in animation
    }

    // Re-enable player movement and camera after panel slides out
    Invoke("EnablePlayerAndCamera", 2f); // Adjust delay to match panel animation duration
}

private void EnablePlayerAndCamera()
{
    Debug.Log("Re-enabling player movement and camera...");

    // Re-enable the camera script
    if (cameraScript != null)
    {
        cameraScript.enabled = true;
    }

    // Re-enable player movement
    if (player != null)
    {
        player.GetComponent<playerlvl4>().enabled = true;
    }
}

}
