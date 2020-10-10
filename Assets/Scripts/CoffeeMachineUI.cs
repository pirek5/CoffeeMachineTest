using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class CoffeeMachineUI : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private CoffeeMachineSettings settings;
    [SerializeField] private CoffeeMachineState state;

    [SerializeField] private TextMeshProUGUI status;

    private CoffeeMachine coffeeMachine;
    
    #endregion
    
    #region Unity Methods
    
    private void Start()
    {
        coffeeMachine = new CoffeeMachine(state, settings);
    }
    
    #endregion
    
    #region Public Methods

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
        coffeeMachine.SelectedCoffeeStrength = CoffeeStrength.Mild;
    }

    [UsedImplicitly]
    public void OnNormalPressed()
    {
        coffeeMachine.SelectedCoffeeStrength = CoffeeStrength.Normal;
    }

    [UsedImplicitly]
    public void OnStrongPressed()
    {
        coffeeMachine.SelectedCoffeeStrength = CoffeeStrength.Strong;
    }

    [UsedImplicitly]
    public void OnSmallPressed()
    {
        coffeeMachine.SelectedCoffeeSize = CoffeeSize.Small;
    }

    [UsedImplicitly]
    public void OnMediumPressed()
    {
        coffeeMachine.SelectedCoffeeSize = CoffeeSize.Medium;
    }

    [UsedImplicitly]
    public void OnBigPressed()
    {
        coffeeMachine.SelectedCoffeeSize = CoffeeSize.Big;
    }

    [UsedImplicitly]
    public void OnCheckStatusPressed()
    {
        status.text = coffeeMachine.CheckMachineState();
    }
    
    [UsedImplicitly]
    public void OnMakeCoffeePressed()
    {
        string warningMessage = coffeeMachine.ValidateMachineBeforeUse();
        if (warningMessage != String.Empty)
        {
            status.text = warningMessage;
            return;
        }

        Coffee coffee = coffeeMachine.MakeCoffee();
        status.text = coffee.ToString();
    }
    
    #endregion
}
