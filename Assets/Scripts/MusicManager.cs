using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;

    public AudioClip[] musicClips;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicClips[UnityEngine.Random.Range(0, musicClips.Length)];
        musicSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = musicClips[UnityEngine.Random.Range(0, musicClips.Length)];
            musicSource.Play();
        }
    }
}
