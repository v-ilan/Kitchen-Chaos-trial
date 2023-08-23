using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgerssBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        if (null != (hasProgress = hasProgressGameObject.GetComponent<IHasProgress>()))
        {
            hasProgress.OnProgressChanged += HasProgressOnProgressChanged;
        }
        else
        {
            Debug.LogError("GameObject" +  hasProgressGameObject.name + "does not have a component that implement IHasProgress!");
        }
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgressOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if(e.progressNormalized == 0f || e.progressNormalized == 1f) 
        {
            Hide();
        }
        else
        {
            Show();
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
