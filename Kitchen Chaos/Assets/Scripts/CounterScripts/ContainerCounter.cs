using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(PlayerController playerController)
    {
        if (!playerController.HasKitchenObject())
        {   //Player has no KitchenObject
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, playerController);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {
        Interact(playerController);
    }
}
