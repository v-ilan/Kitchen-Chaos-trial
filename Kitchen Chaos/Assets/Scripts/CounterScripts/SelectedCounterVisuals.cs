using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisuals : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    
    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += PlayerControlelrOnSelectedCounterChanged; ;
    }

    private void PlayerControlelrOnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
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
        if (GameHandler.Instance.IsGamePlaying())
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(true);
            }
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

    private void OnDestroy() 
    {
        if (PlayerController.Instance != null) 
        {
            PlayerController.Instance.OnSelectedCounterChanged -= PlayerControlelrOnSelectedCounterChanged; ;
        }
    }
}
