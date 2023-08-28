using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class GameHandlerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI numberRecipesText;
    [SerializeField] private GameObject GamePlayingClock;
    [SerializeField] private Image clockTimerImage;

    private void Start()
    {
        GameHandler.Instance.OnStateChange += GameHandlerOnStateChange;
        countdownText.gameObject.SetActive(false);
        gameOverUI.SetActive(false);
        GamePlayingClock.SetActive(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameHandler.Instance.getCountdownTimer()).ToString();
        clockTimerImage.fillAmount = GameHandler.Instance.GetGamePlayingTimerNormalized();
        //ClockColor();
    }

    private void GameHandlerOnStateChange(object sender, System.EventArgs e)
    {
        if(GameHandler.Instance.IsCountdown())
        {
            countdownText.gameObject.SetActive(true);
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }

        if(GameHandler.Instance.IsGamePlaying())
        {
            GamePlayingClock.SetActive(true);
        }
        else
        {
            GamePlayingClock.SetActive(false);
        }

        if (GameHandler.Instance.IsGameOver())
        {
            numberRecipesText.text = DeliveryManager.Instance.GetSuccessfullRecipesAmount().ToString();

            gameOverUI.SetActive(true);
        }
        else
        {
            gameOverUI.SetActive(false);
        }
    }

    private void ClockColor()
    {
        if(clockTimerImage.fillAmount < 0.5f)
        {
            clockTimerImage.color = Color.green;
        }
        else if(clockTimerImage.fillAmount < 0.75f)
        {
            clockTimerImage.color = Color.yellow;
        }
        else
        {
            clockTimerImage.color = Color.red;
        }
    }

}
