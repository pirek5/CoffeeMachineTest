using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeSize
{
    Small,
    Medium,
    Big,
};

public enum CoffeeStrength
{
    Mild,
    Normal,
    Strong,
}

public class Coffee
{
    public CoffeeStrength coffeeStrength;
    public CoffeeSize coffeeSize;
    public float amountOfWater;
    public float usedCoffeeSeeds;
    public float producedCoffeeGrounds;
}