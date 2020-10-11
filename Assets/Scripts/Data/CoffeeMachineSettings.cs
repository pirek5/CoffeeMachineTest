using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "CoffeeMachineSettings", menuName = "CoffeeMachine/Settings")]
public class CoffeeMachineSettings : ScriptableObject
{
    #region Types
    
    [System.Serializable]
    public class CoffeeSettings
    {
        [SerializeField] private CoffeeStrength coffeeStrength;
        [SerializeField] private CoffeeSize coffeeSize;
        [Space] 
        [SerializeField] private float waterUsed;
        [SerializeField] private float coffeeSeedsUsed;
        [SerializeField] private string coffeeName;

        public string CoffeeName => coffeeName;
        public CoffeeStrength CoffeeStrength => coffeeStrength;
        public CoffeeSize CoffeeSize => coffeeSize;
        public float WaterUsed => waterUsed;
        public float CoffeeSeedsUsed => coffeeSeedsUsed;

        public CoffeeSettings(string coffeeName, float waterUsed, float coffeeSeedsUsed, CoffeeStrength coffeeStrength, CoffeeSize coffeeSize)
        {
            this.coffeeName = coffeeName;
            this.waterUsed = waterUsed;
            this.coffeeSeedsUsed = coffeeSeedsUsed;
            this.coffeeSize = coffeeSize;
            this.coffeeStrength = coffeeStrength;
        }

        public Coffee GetCoffeeFromThisSettings()
        {
            return new Coffee
            {
                coffeeStrength = coffeeStrength,
                coffeeSize = coffeeSize,
                usedCoffeeSeeds = coffeeSeedsUsed,
                amountOfWater = waterUsed,
            };
        }
    }
    
    #endregion
    
    #region Variables

    [SerializeField] private FavoriteCoffeesSettings favoriteCoffeesSettings;
    [Space]
    [SerializeField] private float waterTankCapacity;
    [SerializeField] private float waterTrayCapacity;
    [SerializeField] private float coffeeTankCapacity;
    [SerializeField] private float coffeeGroundsTankCapacity;
    [Space] 
    [Tooltip("Amount of liquid that goes to water tray from every cup of coffee is random, this is the minimum possible amount")]
    [SerializeField] private float minAmountOfWaterThatGoesToTray;
    [Tooltip("Amount of liquid that goes to water tray from every cup of coffee is random, this is the maximum possible amount")]
    [SerializeField] private float maxAmountOfWaterThatGoesToTray;
    [Tooltip("Amount of used coffee seeds is multiplied by this to get amount of coffee grounds (grounds are coffee seeds plus water")]
    [SerializeField, Range(1f,2f)] private float coffeeGroundsWasteMultiplier;
    [Space] 
    [SerializeField] private string statusMachineDisabled;
    [SerializeField] private string statusOk;
    [SerializeField] private string statusTrayFull;
    [SerializeField] private string noCoffeeWarning;
    [SerializeField] private string noWaterWarning;
    [SerializeField] private string toMuchCoffeeGroundsWarning;
    [SerializeField] private string noFavCoffeeSelectedStatus;
    [Space]
    [SerializeField] private List<CoffeeSettings> possibleCoffees;
    
    #endregion
    
    #region Properties

    public FavoriteCoffeesSettings FavoritesSettings => favoriteCoffeesSettings;
    
    public float WaterTankCapacity => waterTankCapacity;
    public float WaterTrayCapacity => waterTrayCapacity;
    public float CoffeeTankCapacity => coffeeTankCapacity;
    public float CoffeeGroundsTankCapacity => coffeeGroundsTankCapacity;

    public float MinAmountOfWaterThatGoesToTray => minAmountOfWaterThatGoesToTray;
    public float MaxAmountOfWaterThatGoesToTray => maxAmountOfWaterThatGoesToTray;
    public float CoffeeGroundsWasteMultiplier => coffeeGroundsWasteMultiplier;

    public string StatusMachineDisabled => statusMachineDisabled;
    public string StatusOk => statusOk;
    public string NoFavCoffeeSelectedStatus => noFavCoffeeSelectedStatus;
    public string StatusTrayFull => statusTrayFull;
    public string NoCoffeeWarning => noCoffeeWarning;
    public string NoWaterWarning => noWaterWarning;
    public string ToMuchCoffeeGroundsWarning => toMuchCoffeeGroundsWarning;
    
    #endregion
    
    #region Public Methods

    public Coffee GetSpecificCoffee(CoffeeStrength coffeeStrength, CoffeeSize coffeeSize)
    {
        foreach (var coffeeSettings in possibleCoffees)
        {
            if (coffeeSettings.CoffeeSize == coffeeSize && coffeeSettings.CoffeeStrength == coffeeStrength)
            {
                return coffeeSettings.GetCoffeeFromThisSettings();
            }
        }

        Debug.LogError($"[{this}] Couldn't find settings for that type of coffee: {coffeeSize}, {coffeeStrength}");
        return null;
    }
    
    #endregion
}