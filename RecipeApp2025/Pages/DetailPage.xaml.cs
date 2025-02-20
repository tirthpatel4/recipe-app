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
	protected override void OnAppearing()
	{
		base.OnAppearing();
        if (App.CurrentRecipe.Cook_time == 0)
        {
            CookTimeLabel.IsVisible = false;
		}
        else
        {
			CookTimeLabel.IsVisible = true;
		}
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

}
