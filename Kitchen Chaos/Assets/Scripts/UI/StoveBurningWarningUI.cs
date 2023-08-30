using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        Hide();
    }

    private void StoveCounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        const float BURN_SHOW_PROGRESS_AMOUNT = 0.5f;
        //bool show = stoveCounter.IsFried() && BURN_SHOW_PROGRESS_AMOUNT < e.progressNormalized;

        if ((stoveCounter.IsFried() && BURN_SHOW_PROGRESS_AMOUNT < e.progressNormalized))
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
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
