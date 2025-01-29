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

public partial class DiscoverPage : ContentPage, INotifyPropertyChanged
{
    private readonly HttpClient _httpClient = new HttpClient();
	private const string ApiKey = "DM QUINTON";
	private const string BaseUrl = "https://api.spoonacular.com/";
    private string[] Names = {"Spaghetti Carbonara",
                            "Chicken Alfredo",
                            "Beef Stroganoff",
                            "Vegetarian Chili",
                            "Grilled Salmon",
                            "Garlic Butter Shrimp",
                            "Stuffed Bell Peppers",
                            "Teriyaki Chicken",
                            "Eggplant Parmesan",
                            "Lemon Herb Roasted Chicken",};
    public ObservableCollection<Recipe> Recipes { get; set; }
    public ICommand GoToRecipeDetailPageCommand { get; }
    public DiscoverPage()
    {
        InitializeComponent();

        Recipes = new ObservableCollection<Recipe>();

        /* temporary hard coded data */
        for (int i = 1; i < 10; i++)
        {
            Recipes.Add(new Recipe(Names[i]));

        }

        BindingContext = this;
        DiscoverFeed.ItemsSource = Recipes;
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
public class CustomEventArgs : EventArgs
{
    public Recipe SelectedRecipe { get; set; }

    public CustomEventArgs(Recipe sr)
    {
        SelectedRecipe = sr;
    }

}