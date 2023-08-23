using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeOrdered += DeliveryManagerOnRecipeOrdered;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManagerOnRecipeDelivered;
        UpdateVisual();
    }

    private void DeliveryManagerOnRecipeDelivered(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManagerOnRecipeOrdered(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child != recipeTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitRecipeSOList())
        {
            Transform  recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
