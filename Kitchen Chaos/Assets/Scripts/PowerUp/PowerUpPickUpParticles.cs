using System.Collections;
using UnityEngine;

public class PowerUpPickUpParticles : MonoBehaviour
{
    [SerializeField] private PowerUpPickup powerUpPickup;
    [SerializeField] private ParticleSystem pickupParticles;

    float duration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerUpPickup.OnPickedUp += PowerUpPickup_OnPickedUp;
        if (pickupParticles != null) 
        {
            duration = pickupParticles.main.duration;
        }
    }

    private void PowerUpPickup_OnPickedUp(object sender, System.EventArgs e) 
    {
        if (pickupParticles != null) 
        {
            transform.parent = null; 
            pickupParticles.Play();

            StartCoroutine(ReturnParticlesToParent());
        }
        // Hide the model immediately upon pickup
        //visualModel.SetActive(false);

        
    }

    private IEnumerator ReturnParticlesToParent() 
    {
        yield return new WaitForSeconds(duration * 1.05f);
        transform.parent = powerUpPickup.transform;
        transform.localPosition = Vector3.up;
    }
}
