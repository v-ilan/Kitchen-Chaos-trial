using System.Collections;
using UnityEngine;

public class PowerUpPickupVisual : MonoBehaviour
{
    [SerializeField] private PowerUpPickup powerUpPickup;
    [SerializeField] private ParticleSystem pickupParticles;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject visualModel;

    float duration;

    float hoverAmount = 0.1f;
    float hoverSpeed = 2f;
    float baseHeight=  0.5f;

    private void Start()
    {
        powerUpPickup.OnPickedUp += PowerUpPickup_OnPickedUp;
        baseHeight = powerUpPickup.GetComponent<SphereCollider>().center.y;

        if (pickupParticles != null) 
        {
            duration = pickupParticles.main.duration;
        }
    }

    private void Update()
    {
        // Rotation is a visual concern, so it stays here
        visualModel.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Sine Wave Hover - Moves the cube up and down by 0.1 units over time
        
        visualModel.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * hoverSpeed) * hoverAmount + baseHeight, 0);
    }

    private void PowerUpPickup_OnPickedUp(object sender, System.EventArgs e) 
    {
        if (pickupParticles != null) 
        {
            pickupParticles.transform.parent = null; 
            pickupParticles.Play();

            StartCoroutine(ReturnParticlesToParent());
        }
        // Hide the model immediately upon pickup
        visualModel.SetActive(false);

        
    }

    private IEnumerator ReturnParticlesToParent() 
    {
        yield return new WaitForSeconds(duration * 1.05f);
        pickupParticles.transform.parent = transform;
        pickupParticles.transform.localPosition = Vector3.zero;
    }
}
