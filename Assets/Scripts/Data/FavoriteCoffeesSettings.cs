using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FavoriteCoffeesSettings", menuName = "CoffeeMachine/FavoriteCoffeeSettings")]
public class FavoriteCoffeesSettings : ScriptableObject
{
    [SerializeField] private float defaultAmountOfWater;
    [SerializeField] private float defaultAmountOfCoffeeSeeds;
    [Space] 
    [SerializeField] private float amountOfWaterToCoffeeBeDefinedAsBig;
    [SerializeField] private float amountOfWaterToCoffeeBeDefinedAsMedium;
    [Space] 
    [SerializeField] private float coffeeWaterRatioForStrongCoffee;
    [SerializeField] private float coffeeWaterRatioForNormalCoffee;
    
    public float DefaultAmountOfWater => defaultAmountOfWater;
    public float DefaultAmountOfCoffeeSeeds => defaultAmountOfCoffeeSeeds;

    public float AmountOfWaterToCoffeeBeDefinedAsBig => amountOfWaterToCoffeeBeDefinedAsBig;
    public float AmountOfWaterToCoffeeBeDefinedAsMedium => amountOfWaterToCoffeeBeDefinedAsMedium;

    public float CoffeeWaterRatioForNormalCoffee => coffeeWaterRatioForNormalCoffee;
    public float CoffeeWaterRatioForStrongCoffee => coffeeWaterRatioForStrongCoffee;
}
