using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoffeeMachine
{
    #region Variables
    
    private CoffeeMachineState coffeeMachineState;
    private CoffeeMachineSettings settings;

    public CoffeeSize SelectedCoffeeSize { get; set; }
    public  CoffeeStrength SelectedCoffeeStrength { get; set; }

    private bool machineEnabled;

    #endregion

    #region Properties

    public bool IsMachineEnabled => machineEnabled;

    #endregion
    
    #region Contructor & Init

    public CoffeeMachine(CoffeeMachineState coffeeMachineState, CoffeeMachineSettings settings)
    {
        this.coffeeMachineState = coffeeMachineState;
        this.settings = settings;
    }

    #endregion
    
    #region Public Methods

    public void EnableMachine()
    {
        machineEnabled = true;
    }

    public void DisableMachine()
    {
        machineEnabled = false;
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

    public string CheckMachineState()
    {
        StringBuilder coffeeState = new StringBuilder();
        coffeeState.AppendLine("Amount of water in the tank: " + coffeeMachineState.amountOfWaterInTank);
        coffeeState.AppendLine("Amount of coffee in the tank: " + coffeeMachineState.amountOfCoffee);
        coffeeState.AppendLine("Amount of coffee grounds in the tank: " + coffeeMachineState.amountOfCoffeeGrounds);
        coffeeState.AppendLine("Amount of water in the tray: " + coffeeMachineState.amountOfWaterInTray);
        if (coffeeMachineState.amountOfWaterInTray >= settings.WaterTrayCapacity)
        {
            coffeeState.AppendLine("Tray is full, please empty tray!");
        }

        return coffeeState.ToString();
    }

    public string ValidateMachineBeforeUse()
    {
        Coffee coffeeToProduce = settings.GetSpecificCoffee(SelectedCoffeeStrength, SelectedCoffeeSize);
        if (!IsEnoughCoffeeInTank(coffeeToProduce))
        {
            return settings.NoCoffeeWarning;
        }
        
        if(!IsEnoughWaterInTank(coffeeToProduce))
        {
            return settings.NoWaterWarning;
        }

        if (!IsEnoughSpaceInCoffeeGroundsTank(coffeeToProduce))
        {
            return settings.ToMuchCoffeeGrounds;
        }

        return String.Empty;
    }

    public Coffee MakeCoffee()
    {
        Coffee coffeeToProduce = settings.GetSpecificCoffee(SelectedCoffeeStrength, SelectedCoffeeSize);
        
        coffeeMachineState.amountOfCoffee -= coffeeToProduce.usedCoffeeSeeds;
        coffeeMachineState.amountOfWaterInTank -= coffeeToProduce.amountOfWater;
        float waterInCoffeeGrounds = coffeeToProduce.producedCoffeeGrounds - coffeeToProduce.usedCoffeeSeeds;
        coffeeMachineState.amountOfWaterInTank -= waterInCoffeeGrounds;
        coffeeMachineState.amountOfCoffeeGrounds += coffeeToProduce.producedCoffeeGrounds;
        
        float amountOfWaterThatGoesToTray = Random.Range(settings.MinAmountOfWaterThatGoesToTray,
            settings.MaxAmountOfWaterThatGoesToTray);
        coffeeToProduce.amountOfWater -= amountOfWaterThatGoesToTray;
        
        coffeeMachineState.amountOfWaterInTray = Mathf.Max(settings.WaterTrayCapacity, 
            coffeeMachineState.amountOfWaterInTank + amountOfWaterThatGoesToTray) ;
        return coffeeToProduce;
    }
    
    #endregion
    
    #region Private Methods
    
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
    
    #endregion
}
