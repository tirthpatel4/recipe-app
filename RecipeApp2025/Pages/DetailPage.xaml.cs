using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;
using Syncfusion.Maui.Toolkit.PullToRefresh;

namespace RecipeApp2025.Pages;

public partial class DetailPage : ContentPage
{
    private bool isSaved = App.CurrentRecipe.isSaved;
    public ICommand ToggleIngListCommand { get; }
    public DetailPage()
    {

        InitializeComponent();
        BindingContext = App.CurrentRecipe;
        /*Set item sources for both lists: ing and stpes*/
        IngredientsList.ItemsSource = App.CurrentRecipe.ingredients;
        StepsList.ItemsSource = App.CurrentRecipe.steps;

        /* Set width of Steps/Ingredients grids based on width of screen */
        
        /*behavior for rotating */
        this.SizeChanged += OnSizeChanged;

        //!!!!!! THIS NEEDS TO CHANGE !!!!! HACKY AF
        //IngredientsList.HeightRequest = 50 * App.CurrentRecipe.ingredients.Count;
        StepsIngredientsSL.HeightRequest = 50 * App.CurrentRecipe.ingredients.Count + 150 * App.CurrentRecipe.steps.Count;


        for(int i = 0; i < 12; i++)
        {
            Debug.WriteLine(App.CurrentRecipe.ingredients[i] + "\n");
        }
        


        //Set Binding context to global variable
        if (App.CurrentRecipe.isSaved)
        {
            ToggleButton.Text = "Unsave";
        }
        else
        {
            ToggleButton.Text = "Save";
        }

        



    }
    private void OnSizeChanged(object sender, EventArgs e)
    {
        App.SetStackLayoutOrientation(StepsIngredientsSL);

        
    }
   
    private void OnToggleButtonClicked(object sender, EventArgs e)
    {
        isSaved = !isSaved;
        ToggleButton.Text = isSaved ? "Unsave" : "Save";
        if (isSaved) {
            App.AddSavedRecipe(App.CurrentRecipe);
        }
        else
        {
            App.RemoveSavedRecipe(App.CurrentRecipe);
        }


        App.CurrentRecipe.isSaved = isSaved;    
    }

    private void IngredientsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

   
}
