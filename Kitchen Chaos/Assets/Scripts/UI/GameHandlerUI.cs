using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandlerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI numberRecipesText;
    [SerializeField] private GameObject GamePlayingClock;
    [SerializeField] private Image clockTimerImage;

    private const string NUMBER_POPUP = "NumberPopup";

    private Animator animator;
    private int previousCoundownNum = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameHandler.Instance.OnStateChange += GameHandlerOnStateChange;
        countdownText.gameObject.SetActive(false);
        gameOverUI.SetActive(false);
        GamePlayingClock.SetActive(false);
    }

    private void Update()
    {
        int countDownNum = Mathf.CeilToInt(GameHandler.Instance.getCountdownTimer());
        countdownText.text = countDownNum.ToString();
        if(GameHandler.Instance.IsCountdown() && previousCoundownNum !=  countDownNum)
        {
            previousCoundownNum = countDownNum;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
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
