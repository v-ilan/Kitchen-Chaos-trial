using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainGameScene); });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        playButton.Select();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}
