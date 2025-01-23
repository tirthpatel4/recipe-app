using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;
using RecipeApp2025;
using System.Windows.Input;
using System.Diagnostics;

namespace RecipeApp2025.Pages;
public partial class DiscoverPage : ContentPage, INotifyPropertyChanged
{
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


}

public class CustomEventArgs : EventArgs
{
	public Recipe SelectedRecipe { get; set; }

	public CustomEventArgs(Recipe sr)
	{
		SelectedRecipe = sr;
	}

}

