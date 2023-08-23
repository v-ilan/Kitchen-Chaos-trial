using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private const float SPAWN_PLATES_TIMER_MAX = 4f;
    private float spawnPlatesTimer;
    private const int PLATES_AMMOUNT_MAX = 4;
    private int platesAmmount;

    private void Start()
    {
        spawnPlatesTimer = 0;
    }
    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if(SPAWN_PLATES_TIMER_MAX < spawnPlatesTimer)
        {
            spawnPlatesTimer = 0;
            if(PLATES_AMMOUNT_MAX > platesAmmount)
            {
                platesAmmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerController playerController)
    {
        if(!playerController.HasKitchenObject()) 
        {   //player is empty handed
            if(platesAmmount > 0) 
            {   //there are plates on countert to be picked
                platesAmmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, playerController);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
