using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class FavoritesCoffeeUI : MonoBehaviour
{
    [SerializeField] private CoffeeMachineSettings generalSettings;
    [SerializeField] private FavoriteCoffeesSettings favoriteCoffeesSettings;
    [SerializeField] private CoffeeMachineState state;
    [Space]
    [SerializeField] private InputField favoriteCoffeeName;
    [SerializeField] private InputField amountOfCoffee;
    [SerializeField] private InputField amountOfWater;
    [SerializeField] private Button makeFavoriteCoffeeButton;
    [SerializeField] private Button addFavoriteCoffeeButton;
    [SerializeField] private Button removeFavoriteCoffeeButton;
    

    private FavoritesCoffeesController favoritesCoffeesController;

    private void Start()
    {
        favoritesCoffeesController = new FavoritesCoffeesController(state, favoriteCoffeesSettings);
        RefreshView();
    }

    [UsedImplicitly]
    public void OnAddFavoriteCoffeePressed()
    {
        favoritesCoffeesController.AddFavoriteCoffee(favoriteCoffeeName.text, 
            float.Parse(amountOfCoffee.text), float.Parse(amountOfCoffee.text));
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
    
    [UsedImplicitly]
    public void OnMakeFavoriteCoffeePressed()
    {
        favoritesCoffeesController.MakeFavoriteCoffee();
    }
    
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
        amountOfCoffee.interactable = true;
        amountOfWater.text = favoriteCoffeesSettings.DefaultAmountOfWater.ToString();
        amountOfCoffee.interactable = true;
        amountOfCoffee.text = favoriteCoffeesSettings.DefaultAmountOfCoffeeSeeds.ToString();
        makeFavoriteCoffeeButton.gameObject.SetActive(false);
        addFavoriteCoffeeButton.gameObject.SetActive(true);
        removeFavoriteCoffeeButton.gameObject.SetActive(false);
    }

    private void ShowMakeFavoriteCoffee(CoffeeMachineSettings.CoffeeSettings coffeeSettings)
    {
        favoriteCoffeeName.interactable = false;
        favoriteCoffeeName.text = coffeeSettings.CoffeeName;
        amountOfCoffee.interactable = false;
        amountOfWater.text = coffeeSettings.WaterUsed.ToString();
        amountOfCoffee.interactable = false;
        amountOfCoffee.text = coffeeSettings.CoffeeSeedsUsed.ToString();
        makeFavoriteCoffeeButton.gameObject.SetActive(true);
        addFavoriteCoffeeButton.gameObject.SetActive(false);
        removeFavoriteCoffeeButton.gameObject.SetActive(true);
    }
}