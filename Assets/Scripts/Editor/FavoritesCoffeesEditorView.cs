using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class FavoritesCoffeesEditorView
{
    #region Variables

    private readonly FavoritesCoffeesController favoritesCoffeesController;
    private readonly CoffeeMachineSettings settings;

    private readonly VisualElement root;

    private TextField nameField;
    private FloatField waterField;
    private FloatField coffeeField;

    private Button addCoffeeButton;
    private Button removeCoffeeButton;

    #endregion
    
    public FavoritesCoffeesEditorView(VisualElement root, CoffeeMachineSettings settings, FavoritesCoffeesController favoritesCoffeesController)
    {
        this.root = root;
        this.settings = settings;
        this.favoritesCoffeesController = favoritesCoffeesController;
    }
    
    #region Constructor & Init
    
    #endregion

    #region Public Methods

    public void Show()
    {
        AssignInputFields();
        AssignButtons();
        RefreshView();
    }
    
    #endregion
    
    #region Private Methods

    private void AssignInputFields()
    {
        nameField = root.Q<TextField>("NameInput");
        waterField = root.Q<FloatField>("WaterInput");
        coffeeField = root.Q<FloatField>("CoffeeInput");
    }
    
    private void AssignButtons()
    {
        root.Q<Button>("PreviousButton").clicked += () =>
        {
            favoritesCoffeesController.SwitchToPreviousFavoriteCoffee();
            RefreshView();
        };
        
        root.Q<Button>("NextButton").clicked += () =>
        {
            favoritesCoffeesController.SwitchToNextFavoriteCoffee();
            RefreshView();
        };

        addCoffeeButton = root.Q<Button>("AddFavCoffee");
        addCoffeeButton.clicked += () =>
        {
            favoritesCoffeesController.AddFavoriteCoffee(nameField.value, waterField.value, coffeeField.value);
            RefreshView();
        };

        removeCoffeeButton = root.Q<Button>("RemoveFavCoffee");
        removeCoffeeButton.clicked += () =>
        {
            favoritesCoffeesController.RemoveCurrentCoffee();
            RefreshView();
        };
    }

    private void RefreshView()
    {
        var currentFavCoffee = favoritesCoffeesController.GetCurrentFavoriteCoffee();
        if (currentFavCoffee == null)
        {
            ShowAddCoffeeView();
        }
        else
        {
            ShowRemoveCoffeeView(currentFavCoffee);
        }
    }

    private void ShowAddCoffeeView()
    {
        nameField.value = String.Empty;
        nameField.isReadOnly = false;
        waterField.value = settings.FavoritesSettings.DefaultAmountOfWater;
        waterField.isReadOnly = false;
        coffeeField.value = settings.FavoritesSettings.DefaultAmountOfCoffeeSeeds;
        coffeeField.isReadOnly = false;
        
        addCoffeeButton.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        removeCoffeeButton.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
    }

    private void ShowRemoveCoffeeView(CoffeeMachineSettings.CoffeeSettings currentFavCoffee)
    {
        nameField.value = currentFavCoffee.CoffeeName;
        nameField.isReadOnly = true;
        waterField.value = currentFavCoffee.WaterUsed;
        waterField.isReadOnly = true;
        coffeeField.value = currentFavCoffee.CoffeeSeedsUsed;
        coffeeField.isReadOnly = true;
        
        addCoffeeButton.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        removeCoffeeButton.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }
    
    #endregion
}
