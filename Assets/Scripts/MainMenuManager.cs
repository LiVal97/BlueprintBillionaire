using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private AudioManager _audioManager;
    private GlobalManager _globalManager;
    
    public GameObject settingsPanel;
    public GameObject backButton;
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject quitButton;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _globalManager = FindObjectOfType<GlobalManager>();
        RefreshSettings();
    }

    public void StartGame()
    {
        _audioManager.ClickSound();
        SceneManager.LoadScene("Farm");
    }

    public void EnterSettings()
    {
        _audioManager.ClickSound();
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        settingsPanel.SetActive(true);
        backButton.SetActive(true);
    }

    public void GoBack()
    {
        _audioManager.ClickSound();
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
        settingsPanel.SetActive(false);
        backButton.SetActive(false);
    }

    public void QuitGame()
    {
        _audioManager.ClickSound();
        Application.Quit();
        Debug.Log("Application Quit");
    }
    private void RefreshSettings()
    {
        musicSlider.value = _globalManager.currentData.musicVolume;
        sfxSlider.value = _globalManager.currentData.effectsVolume;
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }
    
    public void ChangeSfxVolume()
    {
        AudioManager.instance.SfxVolume(sfxSlider.value);
    }
    
}
