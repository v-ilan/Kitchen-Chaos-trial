using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button SFXButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI SFXText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;
        SFXButton.onClick.AddListener(() => { SoundManager.Instance.ChangeVolume(); UpdateVisual(); });
        musicButton.onClick.AddListener(() => { MusicManager.Instance.ChangeVolume(); UpdateVisual(); });
        closeButton.onClick.AddListener(() => { Hide(); GamePauseUI.Instance.Show(); });
    }

    private void Start()
    {
        GameHandler.Instance.OnGameResume += GameHandlerOnGameResume;
        Hide();
    }

    private void GameHandlerOnGameResume(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        SFXText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
