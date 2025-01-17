using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;
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
	public List<Recipe> Recipes;

	/* variables for handing the save button */
	



	public DiscoverPage()
	{
		Recipes = new List<Recipe>();
		InitializeComponent();

        /* temporary hard coded data */
        for (int i = 1; i < 10; i++)
		{
			Recipes.Add(new Recipe(Names[i]));

        }

		DiscoverFeed.ItemsSource = Recipes;
		//DiscoverRecipeItem	\


		
		
    }
	
	
	

}


