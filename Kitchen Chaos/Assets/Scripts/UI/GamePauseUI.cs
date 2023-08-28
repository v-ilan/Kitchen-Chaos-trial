using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() => { GameHandler.Instance.TogglePauseGame(); });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); }) ;
        optionsButton.onClick.AddListener(() => { OptionsUI.Instance.Show(); Hide(); });
    }
    private void Start()
    {
        GameHandler.Instance.OnGamePause += GameHandlerOnGamePause;
        GameHandler.Instance.OnGameResume += GameHandlerOnGameResume;
        Hide();
    }

    private void GameHandlerOnGamePause(object sender, System.EventArgs e)
    {
        Show();
    }
    private void GameHandlerOnGameResume(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
