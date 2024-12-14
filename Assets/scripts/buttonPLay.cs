using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject Settings;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite clickedSprite;
    public GameObject levelScreen;
    public GameObject menuScreen;
    public Image buttonImage;
    public AudioSource audioSource;
    public Slider soundSlider;

    public Button fullscreenOnButton;
    public Button fullscreenOffButton;

    public Button keyboardControlButton; 
    public Button arrowControlButton; 

    private void Start()
    {
        levelScreen.SetActive(false);
        menuScreen.SetActive(true);
        Settings.SetActive(false);

        if (buttonImage == null)
        {
            Debug.LogError("Button Image is not assigned in the Inspector!", this);
            return;
        }
        buttonImage.sprite = normalSprite; 
        PlayerPrefs.SetInt("level", 0);

        soundSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        UpdateAudioSettings();

        fullscreenOnButton.onClick.AddListener(() => SetFullscreen(true));
        fullscreenOffButton.onClick.AddListener(() => SetFullscreen(false));

        keyboardControlButton.onClick.AddListener(() => SetControlScheme("Keyboard"));
        arrowControlButton.onClick.AddListener(() => SetControlScheme("ArrowKeys"));

        if (!PlayerPrefs.HasKey("ControlScheme"))
        {
            PlayerPrefs.SetString("ControlScheme", "Keyboard");
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen mode: " + isFullscreen);
    }

    public void SetControlScheme(string scheme)
    {
        PlayerPrefs.SetString("ControlScheme", scheme);
        Debug.Log("Control scheme set to: " + scheme);
    }

    // string controlScheme = PlayerPrefs.GetString("ControlScheme");
    // if (controlScheme == "Keyboard")
    // {
    // //keyboard
    // }
    // else if (controlScheme == "ArrowKeys")
    // {
    // //arrows
    // }


    public void OnHoverEnter()
    {
        buttonImage.sprite = hoverSprite;
    }

    public void OnHoverExit()
    {
        buttonImage.sprite = normalSprite;
    }

    public void OnClick()
    {
        buttonImage.sprite = clickedSprite;
        levelScreen.SetActive(true);
        menuScreen.SetActive(false);
        Settings.SetActive(false);
    }

    public void SettingsBtnClicked()
    {
        Settings.SetActive(true);
        levelScreen.SetActive(false);
        menuScreen.SetActive(false);
    }

    public void backbtnClicked()
    {
        Settings.SetActive(false);
        levelScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void stopMusic()
    {
        audioSource.Stop();
    }

    public void startMusic()
    {
        audioSource.Play();
    }

    public void OnVolumeChange()
    {
        PlayerPrefs.SetFloat("Volume", soundSlider.value);
        UpdateAudioSettings();
    }

    private void UpdateAudioSettings()
    {
        audioSource.volume = soundSlider.value;
    }
}
