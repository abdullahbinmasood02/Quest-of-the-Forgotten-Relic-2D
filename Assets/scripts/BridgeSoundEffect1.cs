using UnityEngine;

public class BridgeSoundEffect1 : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip bridgeRestoreSound; // The sound effect for the animation
    public float playDuration = 3f;      // Duration to play the sound

    private void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // This function will be triggered by the Animation Event
    public void PlayBridgeRestoreSound()
    {
        if (audioSource != null && bridgeRestoreSound != null)
        {
            audioSource.clip = bridgeRestoreSound;  // Assign the sound
            audioSource.time = 0;                  // Start at the beginning of the clip
            audioSource.Play();                    // Play the sound
            Invoke("StopAudio", playDuration);     // Stop after the specified duration
        }
    }

    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the audio playback
        }
    }
}
