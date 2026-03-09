using UnityEngine;

public class PowerUpPickupVisual : MonoBehaviour
{
    [SerializeField] private PowerUpPickup powerUpPickup;
    [SerializeField] private GameObject pickupParticlePrefab;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject visualModel;

    private void Start()
    {
        powerUpPickup.OnPickedUp += PowerUpPickup_OnPickedUp;
    }

    private void Update()
    {
        // Rotation is a visual concern, so it stays here
        visualModel.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void PowerUpPickup_OnPickedUp(object sender, System.EventArgs e) 
    {
        if (pickupParticlePrefab != null) 
        {
            Instantiate(pickupParticlePrefab, transform.position, Quaternion.identity);
        }
        // Hide the model immediately upon pickup
        visualModel.SetActive(false);
    }
}
