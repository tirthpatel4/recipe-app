using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;
using RecipeApp2025.Resources.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025;
using System.Windows.Input;
using RecipeApp2025.Services;

namespace RecipeApp2025.Pages;

public partial class DiscoverPage : ContentPage, INotifyPropertyChanged
{

	RecipeService recipeService = new();
	public ObservableCollection<Recipe> Recipes { get; } = new();
    public ICommand GoToRecipeDetailPageCommand { get; }
    public DiscoverPage()
    {
        InitializeComponent();
        BindingContext = this;
		DiscoverFeed.ItemsSource = Recipes;
        GoToRecipeDetailPageCommand = new Command<Recipe>(GoToRecipeDetailPage);
		_ = LoadRecipesAsync();
	}

	public async void GoToRecipeDetailPage(Recipe r)
    {
        Debug.WriteLine("uh oh\n");
        //var customEventArgs = new CustomEventArgs(r);
        //OnRecipesItemClicked(this, customEventArgs);
        App.ChangeCurrentRecipe(r);
        await Shell.Current.GoToAsync("/DetailPage");

    }
	private async Task LoadRecipesAsync()
	{
		try
		{
			var recipes = await recipeService.GetRecipesAsync();
			if (Recipes.Count != 0)
			{
				Recipes.Clear();
			}
			foreach (var recipe in recipes)
			{
				Recipes.Add(recipe);
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("Error!", $"Unable to get recipes: {ex.Message}", "OK");
		}


	}
}

public class CustomEventArgs : EventArgs
{
    public Recipe SelectedRecipe { get; set; }

    public CustomEventArgs(Recipe sr)
    {
        SelectedRecipe = sr;
    }

}