using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public event EventHandler OnAnyPowerUpPickedUp;

    private Coroutine activeTimerCoroutine;
    private float timer;
    private float maxDuration;
    private bool isTimerRunning;

    //public static event EventHandler<OnTimeBonusEventArgs> OnTimeBonusReceived;
    //public class OnTimeBonusEventArgs : EventArgs { public float amount; }

    //private List<PowerUpSO> activePowerUps = new List<PowerUpSO>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPowerUp(PowerUpSO powerUpSO) 
    {
        // Identify what kind of power-up we just got
        switch (powerUpSO.type) 
        {
            case PowerUpSO.PowerUpType.AdrenalineShot:
                // We tell the PlayerController to move faster
                PlayerController.Instance.SetSpeedBoost(powerUpSO.multiplier);
                break;
            
            case PowerUpSO.PowerUpType.TimeWarp:
                GameHandler.Instance.AddGameTime(powerUpSO.multiplier);
                break;

            default:
                Debug.Log("No powerUpSO.type defined for" + powerUpSO.type);
                return;
        }

        // 2. Trigger the SoundManager event
        OnAnyPowerUpPickedUp?.Invoke(this, EventArgs.Empty);

        if (activeTimerCoroutine != null) StopCoroutine(activeTimerCoroutine);
        activeTimerCoroutine = StartCoroutine(PowerUpTimerRoutine(powerUpSO.duration));
    }

    private IEnumerator PowerUpTimerRoutine(float duration) 
    {
        maxDuration = duration;
        timer = duration;
        isTimerRunning = true;

        // Tell the Player to start the effect

        // Wait until we hit the "Fade" threshold (e.g., last 20%)
        while (timer > duration * 0.2f) 
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        // Switch to Fade state

        while (timer > 0) 
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        // Power-up ended
        ResetPowerUps();
        isTimerRunning = false;
        timer = 0;
    }

    private void ResetPowerUps()
    {
        PlayerController.Instance.ResetSpeedBoost();
    }

    public float GetPowerUpTimerNormalized() {
        if (!isTimerRunning || maxDuration <= 0) return 0f;
        return timer / maxDuration;
    }


/*
    public void AddPowerUp(PowerUpSO powerUp)
    {
        StartCoroutine(PowerUpRoutine(powerUp));
        OnAnyPowerUpPickedUp?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator PowerUpRoutine(PowerUpSO powerUp)
    {
        activePowerUps.Add(powerUp);
        ApplyEffect(powerUp, true);

        yield return new WaitForSeconds(powerUp.duration);

        ApplyEffect(powerUp, false);
        activePowerUps.Remove(powerUp);
    }

    

    private void ApplyEffect(PowerUpSO data, bool isAdding)
    {
        if (data.type == PowerUpSO.PowerUpType.TimeWarp) 
        {
            if (isAdding)
            {
                OnTimeBonusReceived?.Invoke(this, new OnTimeBonusEventArgs { amount = data.multiplier });
            }
            return; // Extra time is a "one-shot," no need to wait for a duration to Remove it.
        }
        player.UpdateStats(data, isAdding);

        OnAnyPowerUpPickedUp?.Invoke(this, EventArgs.Empty);
    }
    */
}
