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

            // We don't Destroy(gameObject) immediately anymore! 
            // We need to give the Visuals/Sounds a frame to react.
            // We'll handle cleanup differently or disable the Collider/Mesh.
            gameObject.SetActive(false); 
            Destroy(gameObject, 1f); // Destroy after a delay for any lingering effects
        }
    }

    public PowerUpSO GetPowerUpSO() => powerUpSO;
}
