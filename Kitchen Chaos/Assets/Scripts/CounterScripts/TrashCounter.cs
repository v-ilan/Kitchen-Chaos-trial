using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(PlayerController playerController)
    {
        if(playerController.HasKitchenObject())
        {
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            playerController.GetKitchenObject().DestroySelf();
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {
        Interact(playerController);
    }
}
