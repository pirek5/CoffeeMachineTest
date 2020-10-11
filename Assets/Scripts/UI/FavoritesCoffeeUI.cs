using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FavoritesCoffeeUI : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private TMP_InputField favoriteCoffeeName;
    [SerializeField] private TMP_InputField amountOfCoffee;
    [SerializeField] private TMP_InputField amountOfWater;
    [SerializeField] private Button addFavoriteCoffeeButton;
    [SerializeField] private Button removeFavoriteCoffeeButton;
    
    private FavoritesCoffeesController favoritesCoffeesController;
    private CoffeeMachineSettings settings;

    #endregion

    #region Constructors & Init

    public void Init(FavoritesCoffeesController favoritesCoffeesController, CoffeeMachineSettings settings)
    {
        this.settings = settings;
        this.favoritesCoffeesController = favoritesCoffeesController;
        RefreshView();
    }

    #endregion
    
    #region Public Methods

    [UsedImplicitly]
    public void OnAddFavoriteCoffeePressed()
    {
        favoritesCoffeesController.AddFavoriteCoffee(favoriteCoffeeName.text, 
            float.Parse(amountOfWater.text), float.Parse(amountOfCoffee.text));
        RefreshView();
    }
    
    [UsedImplicitly]
    public void OnRemoveFavoriteCoffeePressed()
    {
        favoritesCoffeesController.RemoveCurrentCoffee();
        RefreshView();
    }
    
    [UsedImplicitly]
    public void OnNextFavoriteCoffeePressed()
    {
        favoritesCoffeesController.SwitchToNextFavoriteCoffee();
        RefreshView();
    }
    
    [UsedImplicitly]
    public void OnPreviousFavoriteCoffeePressed()
    {
        favoritesCoffeesController.SwitchToPreviousFavoriteCoffee();
        RefreshView();
    }

    #endregion
    
    #region Private Methods
    
    private void RefreshView()
    {
        var currentFavoriteCoffee = favoritesCoffeesController.GetCurrentFavoriteCoffee();
        if (currentFavoriteCoffee == null)
        {
            ShowAddFavoriteCoffeeArea();
        }
        else
        {
            ShowMakeFavoriteCoffee(currentFavoriteCoffee);
        }
    }

    private void ShowAddFavoriteCoffeeArea()
    {
        favoriteCoffeeName.interactable = true;
        favoriteCoffeeName.text = String.Empty;
        amountOfWater.interactable = true;
        amountOfWater.text = settings.FavoritesSettings.DefaultAmountOfWater.ToString();
        amountOfCoffee.interactable = true;
        amountOfCoffee.text = settings.FavoritesSettings.DefaultAmountOfCoffeeSeeds.ToString();
        addFavoriteCoffeeButton.gameObject.SetActive(true);
        removeFavoriteCoffeeButton.gameObject.SetActive(false);
    }

    private void ShowMakeFavoriteCoffee(CoffeeMachineSettings.CoffeeSettings coffeeSettings)
    {
        favoriteCoffeeName.interactable = false;
        favoriteCoffeeName.text = coffeeSettings.CoffeeName;
        amountOfWater.interactable = false;
        amountOfWater.text = coffeeSettings.WaterUsed.ToString();
        amountOfCoffee.interactable = false;
        amountOfCoffee.text = coffeeSettings.CoffeeSeedsUsed.ToString();
        addFavoriteCoffeeButton.gameObject.SetActive(false);
        removeFavoriteCoffeeButton.gameObject.SetActive(true);
    }
    
    #endregion
}