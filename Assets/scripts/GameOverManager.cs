using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameOverPanel; 

    private IEnumerator gameover(){

        yield return new WaitForSeconds(2);
        Time.timeScale = 0f; // timescale to zero
        gameOverPanel.SetActive(true); // activate 
    }
    public void ShowGameOver()
    {
        StartCoroutine("gameover");
    }


    public void RetryLevel()
    {
        Time.timeScale = 1f;  // Reset the time scale to 1 (unpause)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("menu"); 
    }
}

