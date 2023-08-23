using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(PlayerController playerController)
    {
        if (!HasKitchenObject())
        {   //There is no KitchenObject on ClearCounter
            if (playerController.HasKitchenObject())
            {   //Player has KitchenObject
                playerController.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {   //There is KitchenObject on ClearCounter
            if (playerController.HasKitchenObject())
            {   //player has KitchenObject
                PlateKitchenObject plateKitchenObject = null;
                if (playerController.GetKitchenObject().TryGetPlate(out plateKitchenObject))
                {   //player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {   //player is carring something which is not a plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {   //There is a plate on the ClearCounter
                        if(plateKitchenObject.TryAddIngredient(playerController.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            playerController.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {   //Player has no KitchenObject
                GetKitchenObject().SetKitchenObjectParent(playerController);
            }
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {
        Interact(playerController);
    }
}