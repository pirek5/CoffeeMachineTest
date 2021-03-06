﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoffeeMachine
{
    #region Variables

    private FavoritesCoffeesController favoritesCoffeesController;
    private CoffeeMachineState coffeeMachineState;
    private CoffeeMachineSettings settings;

    private bool machineEnabled;
    private CoffeeSize selectedCoffeeSize;
    private CoffeeStrength selectedCoffeeStrength;

    #endregion
    
    #region Contructor & Init

    public CoffeeMachine(CoffeeMachineState coffeeMachineState, CoffeeMachineSettings settings, FavoritesCoffeesController favoritesCoffeesController)
    {
        this.coffeeMachineState = coffeeMachineState;
        this.settings = settings;
        this.favoritesCoffeesController = favoritesCoffeesController;
    }

    #endregion
    
    #region Public Methods

    public void ToggleMachine(bool isEnabled)
    {
        machineEnabled = isEnabled;
    }

    public void RefillCoffee()
    {
        coffeeMachineState.amountOfCoffee = settings.CoffeeTankCapacity;
    }

    public void RefillWater()
    {
        coffeeMachineState.amountOfWaterInTank = settings.WaterTankCapacity;
    }

    public void RemoveCoffeeGrounds()
    {
        coffeeMachineState.amountOfCoffeeGrounds = 0f;
    }

    public void RemoveWaterFromTray()
    {
        coffeeMachineState.amountOfWaterInTray = 0f;
    }

    public void ChangeCoffeeType(CoffeeStrength coffeeStrength)
    {
        if (!machineEnabled)
            return;

        selectedCoffeeStrength = coffeeStrength;
    }

    public void ChangeCoffeeSize(CoffeeSize coffeeSize)
    {
        if (!machineEnabled)
            return;

        selectedCoffeeSize = coffeeSize;
    }
    
    public string CheckMachineState()
    {
        StringBuilder coffeeState = new StringBuilder();
        coffeeState.AppendLine($"Amount of water in the tank: {coffeeMachineState.amountOfWaterInTank} ml");
        coffeeState.AppendLine($"Amount of coffee in the tank: {coffeeMachineState.amountOfCoffee}g");
        coffeeState.AppendLine($"Amount of coffee grounds in the tank: {coffeeMachineState.amountOfCoffeeGrounds}g");
        coffeeState.AppendLine($"Amount of fluids in the tray: {coffeeMachineState.amountOfWaterInTray}ml");
        if (coffeeMachineState.amountOfWaterInTray >= settings.WaterTrayCapacity)
        {
            coffeeState.AppendLine("Tray is full, please empty tray!");
        }

        return coffeeState.ToString();
    }

    public ProducedCoffee MakeFavoriteCoffee()
    {
        if (!machineEnabled)
            return new ProducedCoffee(settings.StatusMachineDisabled , null);
        
        if (favoritesCoffeesController.GetCurrentFavoriteCoffee() == null)
        {
            return new ProducedCoffee(settings.NoFavCoffeeSelectedStatus, null);
        }
        
        Coffee coffee = favoritesCoffeesController.GetCurrentFavoriteCoffee().GetCoffeeFromThisSettings();
        return MakeCoffee(coffee);
    }

    public ProducedCoffee MakeRegularCoffee()
    {
        if (!machineEnabled)
            return new ProducedCoffee(settings.StatusMachineDisabled , null);
        
        Coffee coffee = settings.GetSpecificCoffee(selectedCoffeeStrength, selectedCoffeeSize);
        return MakeCoffee(coffee);
    }

    #endregion
    
    #region Private Methods
    
    private ProducedCoffee MakeCoffee(Coffee coffee)
    {
        var machineWarning = ValidateMachineBeforeUse(coffee);
        if(machineWarning != string.Empty)
            return new ProducedCoffee(machineWarning , null);
        
        UseCoffeeMachineResources(coffee);
        HandleCoffeeMachineTray(coffee);
        var status = CheckIfWaterSplitOutOfTray();

        return new ProducedCoffee(status, coffee);
    }
    
    private string ValidateMachineBeforeUse(Coffee coffeeToProduce)
    {
        if (!IsEnoughCoffeeInTank(coffeeToProduce))
            return settings.NoCoffeeWarning;

        if(!IsEnoughWaterInTank(coffeeToProduce))
            return settings.NoWaterWarning;

        if (!IsEnoughSpaceInCoffeeGroundsTank(coffeeToProduce))
            return settings.ToMuchCoffeeGroundsWarning;

        return String.Empty;
    }
    
    private bool IsEnoughWaterInTank(Coffee coffeeToProduce)
    {
        return coffeeMachineState.amountOfWaterInTank >= coffeeToProduce.amountOfWater;
    }

    private bool IsEnoughCoffeeInTank(Coffee coffeeToProduce)
    {
        return coffeeMachineState.amountOfCoffee >= coffeeToProduce.usedCoffeeSeeds;
    }

    private bool IsEnoughSpaceInCoffeeGroundsTank(Coffee coffeeToProduce)
    {
        return settings.CoffeeGroundsTankCapacity - coffeeMachineState.amountOfCoffeeGrounds >= coffeeToProduce.usedCoffeeSeeds;
    }

    private void UseCoffeeMachineResources(Coffee coffee)
    {
        coffeeMachineState.amountOfCoffee -= coffee.usedCoffeeSeeds;
        coffeeMachineState.amountOfWaterInTank -= coffee.amountOfWater;
        float producedCoffeeGrounds = coffee.usedCoffeeSeeds * settings.CoffeeGroundsWasteMultiplier;
        float waterInCoffeeGrounds = producedCoffeeGrounds - coffee.usedCoffeeSeeds;
        coffeeMachineState.amountOfWaterInTank -= waterInCoffeeGrounds;
        coffeeMachineState.amountOfCoffeeGrounds += producedCoffeeGrounds;
    }

    private void HandleCoffeeMachineTray(Coffee coffee)
    {
        float amountOfWaterThatGoesToTray = Random.Range(settings.MinAmountOfWaterThatGoesToTray,
            settings.MaxAmountOfWaterThatGoesToTray);
        coffee.amountOfWater -= amountOfWaterThatGoesToTray;

        coffeeMachineState.amountOfWaterInTray += amountOfWaterThatGoesToTray;
    }

    private string CheckIfWaterSplitOutOfTray()
    {
        string status;
        if (coffeeMachineState.amountOfWaterInTray > settings.WaterTrayCapacity)
        {
            coffeeMachineState.amountOfWaterInTray = settings.WaterTrayCapacity;
            status = settings.StatusTrayFull;
        }
        else
        {
            status = settings.StatusOk;
        }

        return status;
    }
    
    #endregion
}