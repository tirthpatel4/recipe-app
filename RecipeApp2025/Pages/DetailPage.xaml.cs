using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Platform;
using RecipeApp2025.Resources.Classes;
using Syncfusion.Maui.Toolkit.PullToRefresh;

namespace RecipeApp2025.Pages;

public partial class DetailPage : ContentPage
{
    private bool isSaved = App.CurrentRecipe.isSaved;
    public ICommand ToggleIngListCommand { get; }
    public DetailPage()
    {

        InitializeComponent();
        BindingContext = App.CurrentRecipe;
        /*Set item sources for both lists: ing and stpes*/

       
        List<string> Ingredients_Text_List = new List<string>();
        for (int i = 0; i < App.CurrentRecipe.Ingredients_List.Count; i++)
        {
            Debug.WriteLine(i);
            Ingredients_Text_List.Add(App.CurrentRecipe.Ingredients_List[i].Full);
        }

        IngredientsList.ItemsSource = Ingredients_Text_List;
        StepsList.ItemsSource = App.CurrentRecipe.steps;

        /* Set width of Steps/Ingredients grids based on width of screen */
        
        /*behavior for rotating */
        this.SizeChanged += OnSizeChanged;


        //!!!!!! THIS NEEDS TO CHANGE !!!!! HACKY AF
        //IngredientsList.HeightRequest = 50 * App.CurrentRecipe.ingredients.Count;
        StepsIngredientsSL.HeightRequest = 50 * App.CurrentRecipe.Ingredients_List.Count + 150 * App.CurrentRecipe.steps.Count;




        //Set Binding context to global variable
        if (App.CurrentRecipe.isSaved)
        {
            ToggleButton.Text = "Unsave";
        }
        else
        {
            ToggleButton.Text = "Save";
        }


    }

    /*
    private String GenerateIngredientText(Ingredient ing)
    {
        string result = "";
        string a = "";
        string n = "";
        string u = "";

        // if whole number
        if (((double)((int)ing.Amount)) == ing.Amount)
        {
            a = ((double)((int)ing.Amount)).ToString();
        }
        else
        {
            a = ing.Amount.ToString();
        }


        result += a;
        n = ing.Name;
        if (ing.Amount > 1.0)
        {
            n += "s";
        }
        //if no units ie "15 apples" 
        if(ing.Unit != null && ing.Unit.Length > 0){
            result = result + " " + n;
        }
        else
        {
            //units ie "3 tbs of sugar"
            result = result + " " + u + " of " + n;
        }

        return result;


    } */
    private void OnSizeChanged(object sender, EventArgs e)
    {
        App.SetStackLayoutOrientation(StepsIngredientsSL);

        
    }
   

    
    
    private void SetGridWidths(Grid g)
    {
        if (App.IsInPortrait())
        {
            //g.WidthRequest = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
            g.WidthRequest = 350;
        }
        else
        {
            //g.WidthRequest = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width / 2;
            g.WidthRequest = 300;

        }
    }
	protected override void OnAppearing()
	{
		base.OnAppearing();
        if (App.CurrentRecipe.Cook_time == 0)
        {
            CookTimeLabel.IsVisible = false;
			PrepTimeLabel.IsVisible = false;
			TotalTimeLabel.IsVisible = true;
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

    private void IngredientsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

   
}
