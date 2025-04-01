using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Platform;
using RecipeApp2025.Resources.Classes;
using Syncfusion.Maui.Toolkit.PullToRefresh;
using System.Threading;
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

        Debug.WriteLine("IN DETAIL PAGE BEFORE ING LOOP");
        Debug.WriteLine("IN DETAIL PAGE: " + Process.GetCurrentProcess().Id);
        List<string> Ingredients_Text_List = new List<string>();
        for (int i = 0; i < App.CurrentRecipe.Ingredients_List.Count; i++)
        {
            Debug.WriteLine(i);
            Ingredients_Text_List.Add(App.CurrentRecipe.Ingredients_List[i].Full);
        }
        Debug.WriteLine("LOOP DONE !!");
        IngredientsList.ItemsSource = Ingredients_Text_List;
        StepsList.ItemsSource = App.CurrentRecipe.Steps_List;
        SetExpanderProperties();
        /* Set width of Steps/Ingredients grids based on width of screen */
        
        /*behavior for rotating */
        this.SizeChanged += OnSizeChanged;


        //!!!!!! THIS NEEDS TO CHANGE !!!!! HACKY AF
        //IngredientsList.HeightRequest = 50 * App.CurrentRecipe.ingredients.Count;
        //StepsIngredientsSL.HeightRequest = 50 * App.CurrentRecipe.Ingredients_List.Count + 150 * App.CurrentRecipe.Steps_List.Count;




        //Set Binding context to global variable
        if (App.CurrentRecipe.isSaved)
        {
            ToggleButton.Text = "Unsave";
        }
        else
        {
            ToggleButton.Text = "Save";
        }

        /* check that there are valid numbers in the prep time estimates if not, don't display the values */
        if(App.CurrentRecipe.Prep_time == 0 && App.CurrentRecipe.Cook_time == 0)
        {
            PrepCookTimeGrid.IsVisible = false; 
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Debug.WriteLine(App.CurrentRecipe.Name);
        if (App.CurrentUser.Length > 0)
        {
            FirebaseService fs = new FirebaseService();
            Task<User> u = fs.GetUser(App.CurrentUser);
            ToggleButton.IsEnabled = true;
            isSaved = App.CurrentRecipe.isSaved;
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
        SetExpanderProperties();
        
    }
   
    private void SetExpanderProperties()
    {
        if (App.IsInPortrait())
        {
            /*THIS MIGHT NEED TO BE CHANGED: WIILL ALWAYS CLOSE INGREDIENTS WHEN FLIPPING TO VERTICAL*/
            ingExpander.IsExpanded = false;
            ingExpander.IsEnabled = true;
            ingExpander.WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density; 
            Debug.WriteLine("Width from package: " + DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

        }
        else
        {
            ingExpander.WidthRequest = 350;
            ingExpander.IsExpanded = true;
            ingExpander.IsEnabled = false;
        }
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
