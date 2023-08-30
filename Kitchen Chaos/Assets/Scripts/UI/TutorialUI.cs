using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyAltInteractText;
    [SerializeField] private TextMeshProUGUI keyTogglePauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInputOnBindingRebind;
        GameHandler.Instance.OnStateChange += GameHandlerOnStateChange;
        UpdateVisual();
        Show();
    }

    private void GameHandlerOnStateChange(object sender, System.EventArgs e)
    {
        if(GameHandler.Instance.IsCountdown())
        {
            Hide();
        }
    }

    private void GameInputOnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Alternative_Interact);
        keyTogglePauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Toggle_Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
