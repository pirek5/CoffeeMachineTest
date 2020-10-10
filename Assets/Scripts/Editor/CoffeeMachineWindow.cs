using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CoffeeMachineWindow : EditorWindow
{
    [SerializeField] private CoffeeMachineSettings coffeeMachineSettings;
    [SerializeField] private CoffeeMachineState coffeeMachineState;
    [SerializeField] private FavoriteCoffeesSettings favoriteCoffeesSettings;
    
    private readonly string windowTemplatePath = "Assets/Data/UiWindows/CoffeeMachineTemplate.uxml";
    
    [MenuItem("CoffeeMachine/CoffeeMachineWindow")]
    public static void ShowWindow()
    {
        var window = GetWindow<CoffeeMachineWindow>("Coffee Machine Window");
        window.minSize = new Vector2(500f, 300f);
    }

    private void OnEnable()
    {
        FavoritesCoffeesController favoritesCoffeesController = new FavoritesCoffeesController(coffeeMachineState, favoriteCoffeesSettings);
        CoffeeMachine coffeeMachine = new CoffeeMachine(coffeeMachineState, coffeeMachineSettings, favoritesCoffeesController);
        CoffeeMachineEditorView coffeeMachineEditorView = new CoffeeMachineEditorView(rootVisualElement, coffeeMachine);
        var windowTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(windowTemplatePath);
        coffeeMachineEditorView.Show(windowTreeAsset);
    }
}