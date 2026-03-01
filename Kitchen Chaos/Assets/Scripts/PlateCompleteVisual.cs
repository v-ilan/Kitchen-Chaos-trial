using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSOGameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSOGameObject> KitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObjectOnAddIngredient;
        foreach(KitchenObjectSOGameObject kitchenObjectSOGameObject in KitchenObjectSOGameObjectList) 
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObjectOnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientEventArgs e)
    {
        bool isMatch = false;
        for(int i = 0; !isMatch && i < KitchenObjectSOGameObjectList.Count; i++) 
        {
            if (KitchenObjectSOGameObjectList[i].kitchenObjectSO == e.KitchenObjectSO)
            {
                KitchenObjectSOGameObjectList[i].gameObject.SetActive(true);
                isMatch = true;
            }
        }
    }

    private void OnDestroy() 
    {
        if (plateKitchenObject != null) 
        {
            plateKitchenObject.OnAddIngredient -= PlateKitchenObjectOnAddIngredient;
        }
    }
}
