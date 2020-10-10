using UnityEngine.UIElements;

public class CoffeeMachineEditorView
{
    private readonly VisualElement root;
    private readonly CoffeeMachine coffeeMachine;

    private TextField status;
    
    public CoffeeMachineEditorView(VisualElement root, CoffeeMachine coffeeMachine)
    {
        this.root = root;
        this.coffeeMachine = coffeeMachine;
    }

    public void Show(VisualTreeAsset template)
    {
        template.CloneTree(root);
        AssignStatusTextField();
        AssignActionsButtons();
        AssignCoffeeMachineButtons();
    }

    private void AssignStatusTextField()
    {
        status = root.Q<TextField>("StatusTextField");
    }

    private void AssignActionsButtons()
    {
        root.Q<Button>("CheckMachineButton").clicked += () =>
        {
            status.value = coffeeMachine.CheckMachineState();
        };
        root.Q<Button>("RefillWaterTankButton").clicked += coffeeMachine.RefillWater;
        root.Q<Button>("RefillCoffeeTankButton").clicked += coffeeMachine.RefillCoffee;
        root.Q<Button>("RemoveWaterFromTrayButton").clicked += coffeeMachine.RemoveWaterFromTray;
        root.Q<Button>("RemoveCoffeeGroundsButton").clicked += coffeeMachine.RemoveCoffeeGrounds;
    }

    private void AssignCoffeeMachineButtons()
    {
        root.Q<Button>("OnButton").clicked += () =>
        {
            coffeeMachine.ToggleMachine(true);
        };
        root.Q<Button>("OffButton").clicked += () =>
        {
            coffeeMachine.ToggleMachine(false);
        };
        
        root.Q<Button>("MildButton").clicked += () =>
        {
            coffeeMachine.ChangeCoffeeType(CoffeeStrength.Mild);
        };
        root.Q<Button>("NormalButton").clicked += () =>
        {
            coffeeMachine.ChangeCoffeeType(CoffeeStrength.Normal);
        };
        root.Q<Button>("StrongButton").clicked += () =>
        {
            coffeeMachine.ChangeCoffeeType(CoffeeStrength.Strong);
        };
        
        root.Q<Button>("SmallButton").clicked += () =>
        {
            coffeeMachine.ChangeCoffeeSize(CoffeeSize.Small);
        };
        root.Q<Button>("MediumButton").clicked += () =>
        {
            coffeeMachine.ChangeCoffeeSize(CoffeeSize.Medium);
        };
        root.Q<Button>("BigButton").clicked += () =>
        {
            coffeeMachine.ChangeCoffeeSize(CoffeeSize.Big);
        };

        root.Q<Button>("MakeCoffeeButton").clicked += () =>
        {
            status.value = coffeeMachine.MakeCoffee().ToString();
        };
    }
}