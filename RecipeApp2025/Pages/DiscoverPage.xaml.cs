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
	private bool _isSaved;
	private string _saveIcon;
	public event PropertyChangedEventHandler PropertyChanged;




	public string SaveIcon
	{
		get => _saveIcon;
		set
		{
			_saveIcon = value;
			OnPropertyChanged(nameof(SaveIcon));
		}
	}
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


		_isSaved = false;
		SaveIcon = "saveempty.png";

		
    }
	
	
	private void OnSaveButtonClicked(object sender, EventArgs e)
	{
		_isSaved = !_isSaved;
        SaveIcon = _isSaved ? "savefull.png" : "saveempty.png";
    }

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}


