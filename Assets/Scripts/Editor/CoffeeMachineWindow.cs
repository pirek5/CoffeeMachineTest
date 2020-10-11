using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CoffeeMachineWindow : EditorWindow
{
    [SerializeField] private CoffeeMachineSettings coffeeMachineSettings;
    [SerializeField] private CoffeeMachineState coffeeMachineState;

    private readonly string windowTemplatePath = "Assets/Data/UiWindows/CoffeeMachineTemplate.uxml";
    
    [MenuItem("CoffeeMachine/CoffeeMachineWindow")]
    public static void ShowWindow()
    {
        var window = GetWindow<CoffeeMachineWindow>("Coffee Machine Window");
        window.minSize = new Vector2(700f, 300f);
    }

    private void OnEnable()
    {
        FavoritesCoffeesController favoritesCoffeesController = new FavoritesCoffeesController(coffeeMachineState, coffeeMachineSettings);
        CoffeeMachine coffeeMachine = new CoffeeMachine(coffeeMachineState, coffeeMachineSettings, favoritesCoffeesController);
        
        ShowCoffeeMachineView(coffeeMachine);
        ShowFavoritesView(favoritesCoffeesController);
    }

    private void ShowCoffeeMachineView(CoffeeMachine coffeeMachine)
    {
        CoffeeMachineEditorView coffeeMachineEditorView = new CoffeeMachineEditorView(rootVisualElement, coffeeMachine);
        var windowTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(windowTemplatePath);
        coffeeMachineEditorView.Show(windowTreeAsset);
    }

    private void ShowFavoritesView(FavoritesCoffeesController favoritesCoffeesController)
    {
        var favoritesRoot = rootVisualElement.Q<VisualElement>("Favorites");
        FavoritesCoffeesEditorView favoritesCoffeesEditorView =
            new FavoritesCoffeesEditorView(favoritesRoot, coffeeMachineSettings, favoritesCoffeesController);
        favoritesCoffeesEditorView.Show();
    }
}