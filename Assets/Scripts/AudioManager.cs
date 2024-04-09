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
    }

    public void MusicVolume(float musicSliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSliderValue) * 20);
        _globalManager.playersData.musicVolume = musicSliderValue;
    }

    public void SfxVolume(float sfxSliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSliderValue) * 20);
        _globalManager.playersData.effectsVolume = sfxSliderValue;
    }

    public void ClickSound()
    {
        _audioSource.clip = qlickClip;
        _audioSource.Play();
    }
}
