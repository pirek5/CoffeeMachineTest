using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "CoffeeMachineState", menuName = "CoffeeMachine/CoffeeMachineState")]
public class CoffeeMachineState : ScriptableObject
{
   public float amountOfWaterInTank;
   public float amountOfWaterInTray;
   public float amountOfCoffee;
   public float amountOfCoffeeGrounds;

   public List<CoffeeMachineSettings.CoffeeSettings> favoriteCoffees;


}
