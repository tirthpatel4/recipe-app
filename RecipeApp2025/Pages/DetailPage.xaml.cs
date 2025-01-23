using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;

namespace RecipeApp2025.Pages;

public partial class DetailPage : ContentPage
{
   
    public DetailPage()
    {

        InitializeComponent();
        //Set Binding context to global variable
        BindingContext = App.CurrentRecipe;
    }

    
    
}