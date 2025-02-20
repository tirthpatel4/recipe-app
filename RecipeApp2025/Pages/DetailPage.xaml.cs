using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;

namespace RecipeApp2025.Pages;

public partial class DetailPage : ContentPage
{
    private bool isSaved = false;

    public DetailPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (App.CurrentUser.Length > 0)
        {
            FirebaseService fs = new FirebaseService();
            Task<User> u = fs.GetUser(App.CurrentUser);
            ToggleButton.IsEnabled = true;
            if (App.CurrentRecipe.isSaved)
            {
                ToggleButton.Text = "Unsave";
            }
            else
            {
                ToggleButton.Text = "Save";
            }
        }
        else
        {
            ToggleButton.IsEnabled = false;
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
