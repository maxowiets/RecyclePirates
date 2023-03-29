using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> backgroundMusicTracks;
    int currentTrack = 0;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusicTracks[currentTrack];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        currentTrack++;
        if (currentTrack >= backgroundMusicTracks.Count)
        {
            currentTrack = 0;
        }
        audioSource.clip = backgroundMusicTracks[currentTrack];
        audioSource.Play();
    }
}
