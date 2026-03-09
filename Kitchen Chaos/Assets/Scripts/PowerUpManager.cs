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

    private PlayerController player;
    private List<PowerUpSO> activePowerUps = new List<PowerUpSO>();

    private void Awake()
    {
        Instance = this;
    }

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
    }
}
