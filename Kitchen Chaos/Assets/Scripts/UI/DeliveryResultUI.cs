using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messegeText;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;

    private const string SUCCESS_MSG = "DELIVERY\nSUCCESS";
    private const string FAILED_MSG = "DELIVERY\nFAILURE";

    private const string POPUP = "PopUp";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DelivryManagerOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DelivryManagerOnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DelivryManagerOnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = failureColor;
        iconImage.sprite = failureSprite;
        messegeText.text = FAILED_MSG;
        animator.SetTrigger(POPUP);
    }

    private void DelivryManagerOnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messegeText.text = SUCCESS_MSG;
        animator.SetTrigger(POPUP);
    }
}
