using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private ParticleSystem speedTrailParticles;
    private ParticleSystem.MainModule mainModule;
    private Coroutine activeFadeCoroutine;

    [Header("Visual Settings")]
    [SerializeField] private Color electricCyan = new Color(0f, 1f, 1f, 1f);
    [SerializeField] private Color electricYellow = new Color(1f, 1f, 0f, 1f);
    [SerializeField] private Color normalColor = Color.white;

    private void Awake() {
        speedTrailParticles = GetComponent<ParticleSystem>();
        mainModule = speedTrailParticles.main;
    }

    private void Start(){
        // Subscribe to the clean state-change event
        PlayerController.Instance.OnPowerUpStateChanged += PlayerController_OnPowerUpStateChanged;
    }

    private void PlayerController_OnPowerUpStateChanged(object sender, EventArgs e)
    {
        // Stop any running visual transitions to avoid conflicts
        if (activeFadeCoroutine != null) StopCoroutine(activeFadeCoroutine);

        // React based on the Enum state
        switch (PlayerController.Instance.GetPowerUpState()) 
        {
            case PlayerController.PowerUpState.Start:
                // Instantly jump to the high-energy pulse
                activeFadeCoroutine = StartCoroutine(PulseElectricColor());
                break;

            case PlayerController.PowerUpState.Fade:
                // Smoothly return to white over the remaining 20% of the time
                // We'll calculate the fade duration based on your 0.2f logic
                activeFadeCoroutine = StartCoroutine(FadeToNormal(2f)); // Assuming a ~10s total duration
                break;

            case PlayerController.PowerUpState.None:
                mainModule.startColor = normalColor;
                break;
        }
    }

    private IEnumerator PulseElectricColor() 
    {
        while (true) 
        {
            float lerp = Mathf.PingPong(Time.time * 8f, 1f);
            mainModule.startColor = Color.Lerp(electricCyan, electricYellow, lerp);
            yield return null;
        }
    }

    private IEnumerator FadeToNormal(float duration) 
    {
        Color startCol = mainModule.startColor.color;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;
            mainModule.startColor = Color.Lerp(startCol, normalColor, elapsed / duration);
            yield return null;
        }
        
        mainModule.startColor = normalColor;
    }

    void OnDestroy()
    {
        if (PlayerController.Instance != null) 
        {
            PlayerController.Instance.OnPickedSomething -= PlayerController_OnPowerUpStateChanged;
        }
    }
}
