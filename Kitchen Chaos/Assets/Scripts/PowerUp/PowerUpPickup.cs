using System;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public event EventHandler OnPickedUp;
    [SerializeField] private PowerUpSO powerUpSO;

    private void OnTriggerEnter(Collider other) 
    {
        // We look for the Player component
        if (other.TryGetComponent(out PlayerController playerController)) 
        {          
            // Logic First
            PowerUpManager.Instance.AddPowerUp(powerUpSO);
            
            // Notify the Visuals and Sounds
            OnPickedUp?.Invoke(this, EventArgs.Empty);

            // We don't Destroy(gameObject)
            // Return to pool instead of destroying
            gameObject.SetActive(false); 
        }
    }

    public PowerUpSO GetPowerUpSO() => powerUpSO;
}
