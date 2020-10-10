using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoffeeSettings
{
    [SerializeField] private CoffeeStrength coffeeStrength;
    [SerializeField] private CoffeeSize coffeeSize;
    [Space] 
    [SerializeField] private float waterUsed;
    [SerializeField] private float coffeeSeedsUsed;
    [SerializeField] private float coffeeGroundsProduced;

    public CoffeeStrength CoffeeStrength => coffeeStrength;
    public CoffeeSize CoffeeSize => coffeeSize;
    
    public Coffee Coffee => new Coffee
    {
        coffeeStrength = coffeeStrength,
        coffeeSize = coffeeSize,
        usedCoffeeSeeds = coffeeSeedsUsed,
        producedCoffeeGrounds = coffeeGroundsProduced,
        amountOfWater = waterUsed,
    };
}

[CreateAssetMenu (fileName = "CoffeeMachineSettings", menuName = "CoffeeMachine/Settings")]
public class CoffeeMachineSettings : ScriptableObject
{
    [SerializeField] private float waterTankCapacity;
    [SerializeField] private float waterTrayCapacity;
    [SerializeField] private float coffeeTankCapacity;
    [SerializeField] private float coffeeGroundsTankCapacity;
    [Space] 
    [SerializeField] private float minAmountOfWaterThatGoesToTray;
    [SerializeField] private float maxAmountOfWaterThatGoesToTray;
    [Space]
    [SerializeField] private string noCoffeeWarning;
    [SerializeField] private string noWaterWarning;
    [SerializeField] private string toMuchCoffeeGrounds;
    
    [SerializeField] private List<CoffeeSettings> possibleCoffees;

    public float WaterTankCapacity => waterTankCapacity;
    public float WaterTrayCapacity => waterTrayCapacity;
    public float CoffeeTankCapacity => coffeeTankCapacity;
    public float CoffeeGroundsTankCapacity => coffeeGroundsTankCapacity;

    public float MinAmountOfWaterThatGoesToTray => minAmountOfWaterThatGoesToTray;
    public float MaxAmountOfWaterThatGoesToTray => maxAmountOfWaterThatGoesToTray;

    public string NoCoffeeWarning => noCoffeeWarning;
    public string NoWaterWarning => noWaterWarning;
    public string ToMuchCoffeeGrounds => toMuchCoffeeGrounds;

    public Coffee GetSpecificCoffee(CoffeeStrength coffeeStrength, CoffeeSize coffeeSize)
    {
        foreach (var coffeeSettings in possibleCoffees)
        {
            if (coffeeSettings.CoffeeSize == coffeeSize && coffeeSettings.CoffeeStrength == coffeeStrength)
            {
                return coffeeSettings.Coffee;
            }
        }

        Debug.LogError($"[{this}] Couldn't find settings for that type of coffee: {coffeeSize}, {coffeeStrength}");
        return null;
    }
}