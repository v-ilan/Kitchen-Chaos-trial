using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerController playerController)
    {
        if(playerController.HasKitchenObject())
        {   //player carries some kitchen object
            if(playerController.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {   // only accept paltes
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                playerController.GetKitchenObject().DestroySelf();
            }
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {
        Interact(playerController);
    }
}