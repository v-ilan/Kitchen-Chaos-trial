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
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altInteractButton;
    [SerializeField] private Button togglePauseButton;

    [SerializeField] private TextMeshProUGUI SFXText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private TextMeshProUGUI togglePauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        Instance = this;
        SFXButton.onClick.AddListener(() => { SoundManager.Instance.ChangeVolume(); UpdateVisual(); });
        musicButton.onClick.AddListener(() => { MusicManager.Instance.ChangeVolume(); UpdateVisual(); });
        closeButton.onClick.AddListener(() => { Hide(); GamePauseUI.Instance.Show(); });
        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        altInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Alternative_Interact); });
        togglePauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Toggle_Pause); });
    }

    private void Start()
    {
        GameHandler.Instance.OnGameResume += GameHandlerOnGameResume;
        Hide();
        HidePressToRebindKey();
    }

    private void GameHandlerOnGameResume(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        SFXText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Alternative_Interact);
        togglePauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Toggle_Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        SFXButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => { HidePressToRebindKey(); UpdateVisual(); });
    }

    private void OnDestroy() 
    {
        if (GameHandler.Instance != null) 
        {
            GameHandler.Instance.OnGameResume -= GameHandlerOnGameResume;
        }
    }   
}
