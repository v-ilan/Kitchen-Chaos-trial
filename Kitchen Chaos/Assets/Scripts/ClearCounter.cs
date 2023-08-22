using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(PlayerController playerController)
    {
        if(!HasKitchenObject())
        {   //There is no KitchenObject on ClearCounter
            if (playerController.HasKitchenObject())
            {   //Player has KitchenObject
                playerController.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {   //There is KitchenObject on ClearCounter
            if(!playerController.HasKitchenObject())
            {   //Player has no KitchenObject
                GetKitchenObject().SetKitchenObjectParent(playerController);
            }
        }
    }
}
