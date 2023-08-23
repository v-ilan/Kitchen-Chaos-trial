using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private float fryingTimer;
    private float burningTimer;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {   //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        state = State.Fried;
                        burningTimer = 0;
                        burningRecipeSO =  GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)burningTimer / burningRecipeSO.burningTimerMax });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {   //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });

                    }
                    break;
                case State.Burned:

                    break;
            }
        }
    }
    public override void Interact(PlayerController playerController)
    {
        if (!HasKitchenObject())
        {   //There is no KitchenObject on Counter
            KitchenObject kitchenObject = playerController.GetKitchenObject();
            if (playerController.HasKitchenObject())
            {   //Player has KitchenObject
                KitchenObjectSO inputKitchenObjectSO = kitchenObject.GetKitchenObjectSO();
                if (HasRecipe(inputKitchenObjectSO))
                {   //player is carring something that can be fried
                    kitchenObject.SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
                    state = State.Frying;
                    fryingTimer = 0;
                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax });

                }
            }
        }
        else
        {   //There is KitchenObject on ClearCounter
            if (!playerController.HasKitchenObject())
            {   //Player has no KitchenObject
                GetKitchenObject().SetKitchenObjectParent(playerController);
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {

    }

    private bool HasRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetFryingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO FryKitchenObjectSO(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetFryingRecipeSOWithInput(inputKitchenObjectSO)?.output;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO outputKitchenObjectSO = null;
        for (int i = 0; i < fryingRecipeSOArray.Length && outputKitchenObjectSO == null; i++)
        {
            if (fryingRecipeSOArray[i].input == inputKitchenObjectSO)
            {
                outputKitchenObjectSO = fryingRecipeSOArray[i];
            }
        }
        return outputKitchenObjectSO;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BurningRecipeSO outputKitchenObjectSO = null;
        for (int i = 0; i < burningRecipeSOArray.Length && outputKitchenObjectSO == null; i++)
        {
            if (burningRecipeSOArray[i].input == inputKitchenObjectSO)
            {
                outputKitchenObjectSO = burningRecipeSOArray[i];
            }
        }
        return outputKitchenObjectSO;
    }
}
