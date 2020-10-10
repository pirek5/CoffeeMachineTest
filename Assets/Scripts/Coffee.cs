using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    public override string ToString()
    {
        StringBuilder coffee = new StringBuilder($"It is a nice cup of coffee, some may say that it is {coffeeStrength}, and {coffeeSize} coffee");
        coffee.AppendLine($"There are {amountOfWater}ml of water, to produced it {usedCoffeeSeeds}grams of coffee seeds was used");
        coffee.AppendLine($"There were produced {producedCoffeeGrounds}grams of coffee grounds to get it");
        return coffee.ToString();
    }
}