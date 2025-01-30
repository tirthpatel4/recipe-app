using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;
using RecipeApp2025.Resources.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025;
using System.Windows.Input;

namespace RecipeApp2025.Pages;

public partial class SavedRecipesPage : ContentPage, INotifyPropertyChanged
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string ApiKey = "DM QUINTON";
    private const string BaseUrl = "https://api.spoonacular.com/";
   
    public ObservableCollection<Recipe> Recipes { get; set; }
    public ICommand GoToRecipeDetailPageCommand { get; }
    public SavedRecipesPage()
    {
        InitializeComponent();

        Recipes = new ObservableCollection<Recipe>(App.SavedRecipes);

        /* temporary hard coded data */
        
        BindingContext = this;
        SavedFeed.ItemsSource = Recipes;
        GoToRecipeDetailPageCommand = new Command<Recipe>(GoToRecipeDetailPage);


    }
    public async void GoToRecipeDetailPage(Recipe r)
    {
        Debug.WriteLine("uh oh\n");
        //var customEventArgs = new CustomEventArgs(r);
        //OnRecipesItemClicked(this, customEventArgs);
        App.ChangeCurrentRecipe(r);
        await Shell.Current.GoToAsync("/DetailPage");

    }
    private async Task<List<string>> GetRecipesAsync()
    {
        var url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}";
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var titles = json["results"].Select(r => r["title"].ToString()).ToList();

            // DEBUG ONLY!
            //foreach (string t in titles)
            //{
            //    Debug.Write(t);
            //}
            //Debug.WriteLine("");

            return titles;
        }
        else
        {
            Debug.WriteLine("Error: " + response.StatusCode);
            return null;
        }
    }


}
