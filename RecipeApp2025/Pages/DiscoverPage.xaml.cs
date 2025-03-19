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

[QueryProperty(nameof(Keyword), "keyword")]
public partial class DiscoverPage : ContentPage, INotifyPropertyChanged
{
    RecipeService recipeService = new();
    public ObservableCollection<Recipe> Recipes { get; } = new();
    private bool IsLoading;
    public ICommand GoToRecipeDetailPageCommand { get; }
    private string _keyword = String.Empty;
    private int pageNumber = 1;
    public string Keyword
    {
        get => _keyword;
        set
        {
            if (_keyword != value)
            {
                _keyword = value;
                OnPropertyChanged(nameof(Keyword));
                UpdateRecipes();
            }
        }
    }
    public DiscoverPage()
    {
        InitializeComponent();
        BindingContext = this;
        DiscoverFeed.ItemsSource = Recipes;
        GoToRecipeDetailPageCommand = new Command<Recipe>(GoToRecipeDetailPage);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Reset the page number and load the first page
        pageNumber = 1;
        await LoadRecipesAsync();
    }
    public async void GoToRecipeDetailPage(Recipe r)
    {
        await App.ChangeCurrentRecipe(r);
        await Shell.Current.GoToAsync("/DetailPage");
    }
    private void UpdateRecipes()
    {
        if (_keyword != String.Empty)
        {
            for (int i = Recipes.Count - 1; i >= 0; i--)
            {
                if (!Recipes[i].Name.ToLower().Contains(Keyword.ToLower()))
                {
                    Recipes.Remove(Recipes[i]);
                }
            }
        }
        DiscoverFeed.ItemsSource = Recipes;
    }
    private async Task LoadRecipesAsync()
    {
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            var recipes = await recipeService.GetRecipesAsync(pageNumber);

            // Clear the list only on the first load
            if (pageNumber == 1)
            {
                Recipes.Clear();
            }

            foreach (var recipe in recipes)
            {
                Recipes.Add(recipe);
            }

            pageNumber++;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get recipes: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }
    private async void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
    {
        var lastItem = Recipes.LastOrDefault(); // Use Recipes directly
        if (e.Item == lastItem && !IsLoading)
        {
            await LoadMoreRecipes();
        }
    }

    private async Task LoadMoreRecipes()
    {
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            var newRecipes = await recipeService.GetRecipesAsync(pageNumber);

            if (newRecipes.Any())
            {
                foreach (var recipe in newRecipes)
                {
                    Recipes.Add(recipe); // Use the Recipes property directly
                }

                pageNumber++;
            }
        }
        finally
        {
            IsLoading = false;
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