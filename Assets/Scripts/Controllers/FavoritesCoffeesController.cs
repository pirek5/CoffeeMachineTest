using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoritesCoffeesController
{
    public event Action<CoffeeMachineSettings.CoffeeSettings> makeFavoriteCoffeeRequested;
    
    private readonly CoffeeMachineState coffeeMachineState;
    private readonly FavoriteCoffeesSettings favoriteCoffeesSettings;

    private int currentCoffeeIndex;
    
    public FavoritesCoffeesController(CoffeeMachineState coffeeMachineState, FavoriteCoffeesSettings favoriteCoffeesSettings)
    {
        this.favoriteCoffeesSettings = favoriteCoffeesSettings;
        this.coffeeMachineState = coffeeMachineState;
    }

    public void SwitchToNextFavoriteCoffee()
    {
        currentCoffeeIndex++;
        if (!CoffeeWithCurrentIndexExists())
        {
            currentCoffeeIndex = 0;
        }
    }

    public void SwitchToPreviousFavoriteCoffee()
    {
        currentCoffeeIndex--;
        if (currentCoffeeIndex < 0)
        {
            currentCoffeeIndex = coffeeMachineState.favoriteCoffees.Count;
        }
    }

    public void AddFavoriteCoffee(string coffeeName, float waterAmount, float coffeeSeedsAmount)
    {
        if (coffeeMachineState.favoriteCoffees.Exists((x) => x.CoffeeName == coffeeName))
            return;


        var subjectiveCoffeeSize = GetCoffeeSize(waterAmount);
        var subjectiveCoffeeStrength = GetCoffeeStrength(waterAmount, coffeeSeedsAmount);
        coffeeMachineState.favoriteCoffees.Add(new CoffeeMachineSettings.CoffeeSettings
            (coffeeName, waterAmount, coffeeSeedsAmount, subjectiveCoffeeStrength, subjectiveCoffeeSize));
    }

    public void RemoveCurrentCoffee()
    {
        if (CoffeeWithCurrentIndexExists())
        {
            coffeeMachineState.favoriteCoffees.RemoveAt(currentCoffeeIndex);
        }

        currentCoffeeIndex--;
        currentCoffeeIndex = Mathf.Max(0, currentCoffeeIndex);
    }

    public CoffeeMachineSettings.CoffeeSettings GetCurrentFavoriteCoffee()
    {
        return CoffeeWithCurrentIndexExists() ? coffeeMachineState.favoriteCoffees[currentCoffeeIndex] : null;
    }

    public void MakeFavoriteCoffee()
    {
        
    }

    private CoffeeSize GetCoffeeSize(float waterAmount)
    {
        if (waterAmount >= favoriteCoffeesSettings.AmountOfWaterToCoffeeBeDefinedAsBig)
        {
            return CoffeeSize.Big;
        }

        if (waterAmount >= favoriteCoffeesSettings.AmountOfWaterToCoffeeBeDefinedAsMedium)
        {
            return CoffeeSize.Medium;
        }

        return CoffeeSize.Small;
    }

    private CoffeeStrength GetCoffeeStrength(float waterAmount, float seedAmount)
    {
        float ratio = seedAmount / waterAmount;
        if (ratio >= favoriteCoffeesSettings.CoffeeWaterRatioForStrongCoffee)
        {
            return CoffeeStrength.Strong;
        }

        if (ratio >= favoriteCoffeesSettings.CoffeeWaterRatioForNormalCoffee)
        {
            return CoffeeStrength.Normal;
        }

        return CoffeeStrength.Mild;
    }

    private bool CoffeeWithCurrentIndexExists()
    {
        return currentCoffeeIndex <= coffeeMachineState.favoriteCoffees.Count - 1 && currentCoffeeIndex > 0;
    }
}
