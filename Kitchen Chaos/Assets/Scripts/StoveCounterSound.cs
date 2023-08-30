using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;
    private const float WARNING_SOUND_TIMER_MAX = 0.2f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounterOnStateChange;
        stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer < 0)
            {
                warningSoundTimer = WARNING_SOUND_TIMER_MAX;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        const float BURN_SHOW_PROGRESS_AMOUNT = 0.5f;
        playWarningSound = stoveCounter.IsFried() && BURN_SHOW_PROGRESS_AMOUNT < e.progressNormalized;
    }

    private void StoveCounterOnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
