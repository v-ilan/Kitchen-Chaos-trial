using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowreUpUI : MonoBehaviour
{
    [SerializeField] private GameObject powerUpClock;
    [SerializeField] private Image powerUpTimerImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PowerUpManager.Instance.OnAnyPowerUpPickedUp +=  PowerUpManager_OnAnyPowerUpPickedUp;
    }

    private void PowerUpManager_OnAnyPowerUpPickedUp(object sender, EventArgs e)
    {
        powerUpClock.SetActive(true);
    }

    // Update is called once per frame
    private void Update() 
    {
        if(powerUpClock.activeSelf)
        {
            float fillAmount = PowerUpManager.Instance.GetPowerUpTimerNormalized();
            powerUpTimerImage.fillAmount = fillAmount;

            // Toggle visibility based on if a power-up is active
            powerUpClock.SetActive(fillAmount > 0);
        }
    }
}
