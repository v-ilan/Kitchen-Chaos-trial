using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static event EventHandler OnAnyFootstep;
    private PlayerController playerController;
    private float footstepsTimer;

    private const float FOOTSTEPS_TIMER_MAX = 0.1f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footstepsTimer -= Time.deltaTime;
        if( footstepsTimer < 0f)
        {
            footstepsTimer = FOOTSTEPS_TIMER_MAX;
            if (playerController.IsWalking())
            {
                OnAnyFootstep?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
