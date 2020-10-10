using System.Text;

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

public class ProducedCoffee
{
    private readonly string status;
    private readonly Coffee coffee;

    public ProducedCoffee(string status, Coffee coffee)
    {
        this.status = status;
        this.coffee = coffee;
    }

    public override string ToString()
    {
        StringBuilder producedCoffee = new StringBuilder();
        producedCoffee.AppendLine(status);
        if (coffee != null)
        {
            producedCoffee.AppendLine(coffee.ToString());
        }
        return producedCoffee.ToString();
    }
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
        StringBuilder coffee = new StringBuilder();
        coffee.AppendLine($"It is a nice cup of coffee, some may say that it is {coffeeStrength}, {coffeeSize} coffee");
        coffee.AppendLine($"This coffee contains {amountOfWater}ml of water");
        coffee.AppendLine($"To produced it {usedCoffeeSeeds}grams of coffee seeds was used");
        coffee.AppendLine($"There were produced {producedCoffeeGrounds}grams of coffee grounds to get it");
        return coffee.ToString();
    }
}