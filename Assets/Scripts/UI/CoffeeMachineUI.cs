using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class CoffeeMachineUI : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private CoffeeMachineSettings settings;
    [SerializeField] private CoffeeMachineState state;
    [SerializeField] private FavoritesCoffeeUI favoritesCoffeeUi;
    [Space]
    [SerializeField] private TextMeshProUGUI status;

    private CoffeeMachine coffeeMachine;

    #endregion
    
    #region Unity Methods
    
    private void Start()
    {
        var favoritesCoffeeController = new FavoritesCoffeesController(state, settings);
        coffeeMachine = new CoffeeMachine(state, settings, favoritesCoffeeController);
        favoritesCoffeeUi.Init(favoritesCoffeeController, settings);
    }
    
    #endregion
    
    #region Public Methods

    [UsedImplicitly]
    public void OnEnableMachinePressed()
    {
        coffeeMachine.ToggleMachine(true);
    }
    
    [UsedImplicitly]
    public void OnDisableMachinePressed()
    {
        coffeeMachine.ToggleMachine(false);
    }
    
    [UsedImplicitly]
    public void OnRefillWaterPressed()
    {
        coffeeMachine.RefillWater();
    }

    [UsedImplicitly]
    public void OnRefillCoffeePressed()
    {
        coffeeMachine.RefillCoffee();
    }

    [UsedImplicitly]
    public void OnRemoveWaterFromTrayPressed()
    {
        coffeeMachine.RemoveWaterFromTray();
    }

    [UsedImplicitly]
    public void OnRemoveCoffeeGroundsPressed()
    {
        coffeeMachine.RemoveCoffeeGrounds();
    }
    
    [UsedImplicitly]
    public void OnMildPressed()
    {
        coffeeMachine.ChangeCoffeeType(CoffeeStrength.Mild);
    }

    [UsedImplicitly]
    public void OnNormalPressed()
    {
        coffeeMachine.ChangeCoffeeType(CoffeeStrength.Normal);
    }

    [UsedImplicitly]
    public void OnStrongPressed()
    {
        coffeeMachine.ChangeCoffeeType(CoffeeStrength.Strong);
    }

    [UsedImplicitly]
    public void OnSmallPressed()
    {
        coffeeMachine.ChangeCoffeeSize(CoffeeSize.Small);
    }

    [UsedImplicitly]
    public void OnMediumPressed()
    {
        coffeeMachine.ChangeCoffeeSize(CoffeeSize.Medium);
    }

    [UsedImplicitly]
    public void OnBigPressed()
    {
        coffeeMachine.ChangeCoffeeSize(CoffeeSize.Big);
    }

    [UsedImplicitly]
    public void OnCheckStatusPressed()
    {
        status.text = coffeeMachine.CheckMachineState();
    }
    
    [UsedImplicitly]
    public void OnMakeCoffeePressed()
    {
        ProducedCoffee coffee = coffeeMachine.MakeRegularCoffee();
        status.text = coffee.ToString();
    }
    
    [UsedImplicitly]
    public void OnMakeFavoriteCoffeePressed()
    {
        ProducedCoffee coffee = coffeeMachine.MakeFavoriteCoffee();
        status.text = coffee.ToString();
    }

    #endregion
}
