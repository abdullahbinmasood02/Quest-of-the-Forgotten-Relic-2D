using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{

    public Sprite[] PauseSprites;
    public Sprite[] MusicSprites;
    public GameObject GamePausePanel;
    public GameObject GameLosePanel;
    public GameObject GameWinPanel;
    public GameObject GameCutScene;
    public GameObject MusicButton;
    public GameObject PauseButton;
    public bool isGamePaused = false;

    public AudioSource bgMusic;
    public AudioSource SFXMusic;
    public AudioSource SFX;

    public AudioClip[] bgClips;
    public AudioClip[] SFXClips;
    public AudioClip ButtonSound;

    public bool isMuted = false;
    // Start is called before the first frame update
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        isMuted = !isMuted;
        ToggleMusic();
        Time.timeScale = 1;
        GameCutScene.SetActive(true);
        GameLosePanel.SetActive(false);
        GameWinPanel.SetActive(false);
        GamePausePanel.SetActive(false);
        PauseButton.SetActive(false);
        GameWinPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMusic()
    {
        SFX.PlayOneShot(ButtonSound);
        Image musicButtonImage = MusicButton.GetComponent<Image>();

        isMuted = !isMuted;

        bgMusic.mute = isMuted;
        SFXMusic.mute = isMuted;
        SFX.mute = isMuted;

        if (musicButtonImage != null)
        {
            musicButtonImage.sprite = isMuted ? MusicSprites[1] : MusicSprites[0];
        }
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void TogglePause()
    {
        SFX.PlayOneShot(ButtonSound);

        Image buttonImage = PauseButton.GetComponent<Image>();
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            GamePausePanel.SetActive(false);
            if (PauseButton != null)
            {
                buttonImage.sprite = PauseSprites[0];
            }
            if (bgMusic != null) bgMusic.Stop();
            if (SFXMusic != null) SFXMusic.Play();
        }
        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
            GamePausePanel.SetActive(true);
            if (PauseButton != null)
            {
                buttonImage.sprite = PauseSprites[1];
            }
            if (SFXMusic != null) SFXMusic.Stop();
            if (bgMusic != null) bgMusic.PlayOneShot(bgClips[1]);

        }
    }

    public void GameWin()
    {
        PauseButton.SetActive(false);
        GameWinPanel.SetActive(true);
        if (SFXMusic != null) SFXMusic.Stop();
        if (bgMusic != null) bgMusic.PlayOneShot(bgClips[1]);
    }
    public void GameLose()
    {
        PauseButton.SetActive(false);
        GameLosePanel.SetActive(true);
        if (SFXMusic != null) SFXMusic.Stop();
        if (bgMusic != null) bgMusic.PlayOneShot(bgClips[2]);
    }
    public void Continue()
    {
        PauseButton.SetActive(true);
        GameCutScene.SetActive(false);
        bgMusic.Stop();
        SFX.PlayOneShot(ButtonSound);
        SFXMusic.Play();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("desert");
        SFX.PlayOneShot(ButtonSound);

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SFX.PlayOneShot(ButtonSound);

    }
    public void Exit()
    {
        SceneManager.LoadScene("menu");
        SFX.PlayOneShot(ButtonSound);

    }
    public void playSFX(string effect)
    {
        if(effect == "Hurt")
        {
            if(!SFX.isPlaying)
            SFX.PlayOneShot(SFXClips[2]);
        }
        else if(effect == "Door")
        {
            SFX.PlayOneShot(SFXClips[0]);
        }
        else if (effect == "Running")
        {
            SFX.PlayOneShot(SFXClips[1]);
        }
        else if (effect == "Collect")
        {
            SFX.PlayOneShot(ButtonSound);
        }
    }
}
