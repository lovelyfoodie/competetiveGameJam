using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour {

    public GameObject wwiseGlobalRef;
    public WwisePostEvent startMenuMusicEvent;
    public WwisePostEvent startGameplayMusicEvent;
    public WwisePostEvent startGameoverMusicEvent;

    public WwisePostEvent startBirdChirpEvent;
    public WwisePostEvent stopBirdChirpEvent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (startMenuMusicEvent != null)
        {
            startMenuMusicEvent.Post(gameObject);
        }
    }

    public void PlayGameplayMusic()
    {
        if (startGameplayMusicEvent != null)
        {
            startGameplayMusicEvent.Post(gameObject);
        }
    }

    public void PlayGameoverMusic()
    {
        if (startGameoverMusicEvent != null)
        {
            startGameoverMusicEvent.Post(gameObject);
        }
    }

    public void EnableBirdSounds(bool enable)
    {
        if (enable)
        {
            if (startBirdChirpEvent != null)
            {
                startBirdChirpEvent.Post(gameObject);
            }
        } else
        {
            if (stopBirdChirpEvent != null)
            {
                stopBirdChirpEvent.Post(gameObject);
            }
        }
    }
}
