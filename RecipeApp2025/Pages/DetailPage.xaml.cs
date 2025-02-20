using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;

namespace RecipeApp2025.Pages;

public partial class DetailPage : ContentPage
{
    private bool isSaved = App.CurrentRecipe.isSaved;

    public DetailPage()
    {

        InitializeComponent();
        //Set Binding context to global variable
        BindingContext = App.CurrentRecipe;
        if (App.CurrentRecipe.isSaved)
        {
            ToggleButton.Text = "Unsave";
        }
        else
        {
            ToggleButton.Text = "Save";
        }
    }

    private void OnToggleButtonClicked(object sender, EventArgs e)
    {
        FirebaseService fs = new FirebaseService();
        isSaved = !isSaved;
        ToggleButton.Text = isSaved ? "Unsave" : "Save";
        if (isSaved) {
            fs.AddSavedRecipe(App.CurrentRecipe, App.CurrentUser);
        }
        else
        {
            fs.RemoveRecipe(App.CurrentRecipe, App.CurrentUser);
        }


        App.CurrentRecipe.isSaved = isSaved;    
    }

}
