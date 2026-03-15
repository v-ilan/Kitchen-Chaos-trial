using System;
using UnityEngine;

public class WindParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem windCarvings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerController.Instance.OnSpeedChange += PlayerController_OnSpeedChange;
        Hide();
    }

    private void PlayerController_OnSpeedChange(object sender, EventArgs e)
    {
        if (PlayerController.Instance.IsSpeedUp())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        windCarvings.gameObject.SetActive(true);
        windCarvings.Play();
    }

    private void Hide()
    {
        windCarvings.Stop();
        windCarvings.gameObject.SetActive(false);
    }
}
