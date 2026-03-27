using UnityEngine;

public class PowerUpPickupVisual : MonoBehaviour
{
    [SerializeField] private PowerUpPickup powerUpPickup;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject visualModel;

    float hoverAmount = 0.1f;
    float hoverSpeed = 2f;
    float baseHeight=  0.5f;

    private void Start()
    {
        
        baseHeight = powerUpPickup.GetComponent<SphereCollider>().center.y;

        
    }

    private void Update()
    {
        // Rotation is a visual concern, so it stays here
        visualModel.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Sine Wave Hover - Moves the cube up and down by 0.1 units over time
        
        visualModel.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * hoverSpeed) * hoverAmount + baseHeight, 0);
    }


}
