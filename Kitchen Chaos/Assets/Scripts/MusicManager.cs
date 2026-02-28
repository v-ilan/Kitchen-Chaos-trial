using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private AudioSource musicSource;
    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;
        musicSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        musicSource.volume = volume;

    }

    private void Start()
    {
        GameHandler.Instance.OnRushHourStarted += GameHandler_OnRushHourStarted;
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 01f)
        {
            volume = 0;
        }
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }

    private void GameHandler_OnRushHourStarted(object sender, System.EventArgs e) 
    {
        // Speed up music pitch
        musicSource.pitch = 1.2f;
    }

}
