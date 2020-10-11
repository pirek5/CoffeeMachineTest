using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoritesCoffeesController
{
    #region Variables
    
    private readonly CoffeeMachineState coffeeMachineState;
    private readonly CoffeeMachineSettings settings;

    public int currentCoffeeIndex;
    
    #endregion
    
    #region Constructor & Init
    
    public FavoritesCoffeesController(CoffeeMachineState coffeeMachineState, CoffeeMachineSettings settings)
    {
        this.settings = settings;
        this.coffeeMachineState = coffeeMachineState;
        currentCoffeeIndex = -1; // -1 means add coffee page
    }
    
    #endregion
    
    #region Public Mehods

    public void SwitchToNextFavoriteCoffee()
    {
        currentCoffeeIndex++;
        if (!CoffeeWithCurrentIndexExists())
        {
            currentCoffeeIndex = -1;
        }
    }

    public void SwitchToPreviousFavoriteCoffee()
    {
        currentCoffeeIndex--;
        if (currentCoffeeIndex < -1)
        {
            currentCoffeeIndex = coffeeMachineState.favoriteCoffees.Count - 1;
        }
    }

    public void AddFavoriteCoffee(string coffeeName, float waterAmount, float coffeeSeedsAmount)
    {
        if (coffeeName == string.Empty)
            return;

        if (coffeeMachineState.favoriteCoffees.Exists((x) => x.CoffeeName == coffeeName))
            return;
        
        var subjectiveCoffeeSize = GetCoffeeSize(waterAmount);
        var subjectiveCoffeeStrength = GetCoffeeStrength(waterAmount, coffeeSeedsAmount);
        coffeeMachineState.favoriteCoffees.Add(new CoffeeMachineSettings.CoffeeSettings
            (coffeeName, waterAmount, coffeeSeedsAmount, subjectiveCoffeeStrength, subjectiveCoffeeSize));
        currentCoffeeIndex = coffeeMachineState.favoriteCoffees.Count - 1;
    }

    public void RemoveCurrentCoffee()
    {
        if (CoffeeWithCurrentIndexExists())
        {
            coffeeMachineState.favoriteCoffees.RemoveAt(currentCoffeeIndex);
        }

        currentCoffeeIndex--;
    }

    public CoffeeMachineSettings.CoffeeSettings GetCurrentFavoriteCoffee()
    {
        return CoffeeWithCurrentIndexExists() ? coffeeMachineState.favoriteCoffees[currentCoffeeIndex] : null;
    }
    
    #endregion
    
    #region Private Methods

    private CoffeeSize GetCoffeeSize(float waterAmount)
    {
        if (waterAmount >= settings.FavoritesSettings.AmountOfWaterToCoffeeBeDefinedAsBig)
            return CoffeeSize.Big;
        
        if (waterAmount >= settings.FavoritesSettings.AmountOfWaterToCoffeeBeDefinedAsMedium)
            return CoffeeSize.Medium;
        
        return CoffeeSize.Small;
    }

    private CoffeeStrength GetCoffeeStrength(float waterAmount, float seedAmount)
    {
        float ratio = seedAmount / waterAmount;
        if (ratio >= settings.FavoritesSettings.CoffeeWaterRatioForStrongCoffee)
            return CoffeeStrength.Strong;

        if (ratio >= settings.FavoritesSettings.CoffeeWaterRatioForNormalCoffee)
            return CoffeeStrength.Normal;

        return CoffeeStrength.Mild;
    }

    private bool CoffeeWithCurrentIndexExists()
    {
        return currentCoffeeIndex < coffeeMachineState.favoriteCoffees.Count && currentCoffeeIndex >= 0;
    }
    
    #endregion
}
