using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress = 0;

    public override void Interact(PlayerController playerController)
    {
        if (!HasKitchenObject())
        {   //There is no KitchenObject on ClearCounter
            KitchenObject kitchenObject = playerController.GetKitchenObject();
            if (playerController.HasKitchenObject())
            {   //Player has KitchenObject
                KitchenObjectSO inputKitchenObjectSO = kitchenObject.GetKitchenObjectSO();
                if (HasRecipe(inputKitchenObjectSO))
                {   //player is carring something that can be cut
                    kitchenObject.SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });
                }
            }
        }
        else
        {   //There is KitchenObject on ClearCounter
            if (!playerController.HasKitchenObject())
            {   //Player has no KitchenObject
                GetKitchenObject().SetKitchenObjectParent(playerController);
            }
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {
        KitchenObjectSO inputKitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
        if (HasKitchenObject() && HasRecipe(inputKitchenObjectSO))
        {   //There is KitchenObject on cuttingCounter with a cutting recipe
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = SliceKitchenObjectSO(inputKitchenObjectSO);
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO SliceKitchenObjectSO(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(inputKitchenObjectSO)?.output;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO outputKitchenObjectSO = null;
        for (int i = 0; i < cuttingRecipeSOArray.Length && outputKitchenObjectSO == null; i++)
        {
            if (cuttingRecipeSOArray[i].input == inputKitchenObjectSO)
            {
                outputKitchenObjectSO = cuttingRecipeSOArray[i];
            }
        }
        return outputKitchenObjectSO;
    }
}