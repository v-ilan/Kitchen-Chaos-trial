using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeOrdered;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private const int WAITING_RECIPE_MAX = 4;
    private const float SPAWN_RECIPE_TIMER_MAX = 4f;
    private float spawnRecipeTimer;
    private int successfullRecipesAmount = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = SPAWN_RECIPE_TIMER_MAX;
            if (waitingRecipeSOList.Count < WAITING_RECIPE_MAX)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeOrdered?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        bool hasDeliverd = false;
        for(int i = 0; !hasDeliverd && i < waitingRecipeSOList.Count; ++i)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {   //has same number of ingredients
                bool plateContentsMatchesRecipe = true;
                for(int j = 0; plateContentsMatchesRecipe && j < waitingRecipeSO.kitchenObjectSOList.Count; j++)
                {   //cycling through all ingredients in Recipe
                    bool ingredientFound = false;
                    for(int k = 0; !ingredientFound && k < plateKitchenObject.GetKitchenObjectSOList().Count; k++)
                    {    //cycling through all ingredients on plate
                        if (plateKitchenObject.GetKitchenObjectSOList()[k] == waitingRecipeSO.kitchenObjectSOList[j])
                        {   //ingredients match
                            ingredientFound = true;
                        }
                    }
                    if(!ingredientFound)
                    {   //This Recipe ingredient was not found on the Plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {   //player deliverd the corerct recipe
                    waitingRecipeSOList.RemoveAt(i);
                    hasDeliverd = true;
                    successfullRecipesAmount++;
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        if(!hasDeliverd)
        {
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<RecipeSO> GetWaitRecipeSOList()
    {
        return waitingRecipeSOList;    
    }

    public int GetSuccessfullRecipesAmount()
    { 
        return successfullRecipesAmount; 
    }
}
