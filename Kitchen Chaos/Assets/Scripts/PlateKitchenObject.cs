using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddIngredientEventArgs> OnAddIngredient;
    public class OnAddIngredientEventArgs: EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> valideKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;
    
    //
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    
    //
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        bool succeed = false;
        if (valideKitchenObjectSOList.Contains(kitchenObjectSO) && !kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            succeed = true;
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnAddIngredient?.Invoke(this, new OnAddIngredientEventArgs { KitchenObjectSO = kitchenObjectSO });
        }
        return succeed;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
