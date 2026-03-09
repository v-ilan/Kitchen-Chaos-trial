using UnityEngine;

public class PowerUpPickupSound : MonoBehaviour
{
    [SerializeField] private PowerUpPickup powerUpPickup;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float volume = 1f;

    private void Start() {
        powerUpPickup.OnPickedUp += PowerUpPickup_OnPickedUp;
    }

    private void PowerUpPickup_OnPickedUp(object sender, System.EventArgs e) {
        // Using SoundManager (if you have one) or PlayClipAtPoint
        AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
    }
}
