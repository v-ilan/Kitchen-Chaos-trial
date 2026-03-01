using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;
    private bool isFlashing;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        const float BURN_SHOW_PROGRESS_AMOUNT = 0.5f;
        bool show = stoveCounter.IsFried() && BURN_SHOW_PROGRESS_AMOUNT < e.progressNormalized;
        animator.SetBool(IS_FLASHING, show);
    }

    private void OnDestroy() 
    {
        if (stoveCounter != null) 
        {
            stoveCounter.OnProgressChanged -= StoveCounterOnProgressChanged;
        }
    }
}
