using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject WinScreenPanel; 
    
    public void RetryLevel()
    {
        Time.timeScale = 1f;  // Reset the time scale to 1 (unpause)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    public void showwin(){

        Time.timeScale = 0f; // timescale to zero
        WinScreenPanel.SetActive(true);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("menu"); 
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("menu"); 
    }

   
}
