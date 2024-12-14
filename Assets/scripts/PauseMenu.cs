using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;   //  if the game is paused
    public GameObject pauseMenuUI;        
  
    public void Resume()
    {
        pauseMenuUI.SetActive(false);      
        Time.timeScale = 1f;                 // resume the game
        isPaused = false;
    }

    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);         
        Time.timeScale = 0f;               
        isPaused = true;
        Debug.Log("paused");
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1f;                 // reset game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // reload current level
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;                 // reset game time
        SceneManager.LoadScene("menu");  
    }
}
