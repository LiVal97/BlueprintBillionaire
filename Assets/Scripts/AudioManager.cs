using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    private AudioSource _audioSource;
    public AudioClip qlickClip;
    public Slider musicSlider;
    public Slider sfxSlider;

    public static AudioManager instance;
    private GlobalManager _globalManager;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
        
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _globalManager = FindObjectOfType<GlobalManager>();
        RefreshSettings();
    }

    public void MusicVolume(float musicSliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSliderValue) * 20);
        _globalManager.currentData.musicVolume = musicSliderValue;
        _globalManager.SaveGame();
    }

    public void SfxVolume(float sfxSliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSliderValue) * 20);
        _globalManager.currentData.effectsVolume = sfxSliderValue;
        _globalManager.SaveGame();
    }

    public void ClickSound()
    {
        _audioSource.clip = qlickClip;
        _audioSource.Play();
    }

    private void RefreshSettings()
    {
        musicSlider.value = _globalManager.currentData.musicVolume;
        sfxSlider.value = _globalManager.currentData.effectsVolume;
    }
}
