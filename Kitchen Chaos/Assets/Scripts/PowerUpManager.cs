using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public event EventHandler OnAnyPowerUpPickedUp; 

    public static event EventHandler<OnTimeBonusEventArgs> OnTimeBonusReceived;
    public class OnTimeBonusEventArgs : EventArgs { public float amount; }

    private List<PowerUpSO> activePowerUps = new List<PowerUpSO>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPowerUp(PowerUpSO powerUp) {
        Debug.Log("PowerUp!");
        // Identify what kind of power-up we just got
        switch (powerUp.type) {
            case PowerUpSO.PowerUpType.AdrenalineShot:
                // We tell the PlayerController to move faster
                //PlayerController.Instance.ApplySpeedBoost(powerUp.multiplier, powerUp.duration);
                break;
            
            case PowerUpSO.PowerUpType.TimeWarp:
                // We'll hook this to KitchenGameManager later
                break;

            default:
                Debug.Log("No powerUp.type defined for" + powerUp.type);
                break;
        }

        // 2. Trigger the SoundManager event
        OnAnyPowerUpPickedUp?.Invoke(this, EventArgs.Empty);
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
