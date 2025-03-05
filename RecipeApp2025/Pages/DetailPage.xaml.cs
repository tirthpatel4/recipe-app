using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp2025.Resources.Classes;

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
        //this.SizeChanged += OnSizeChanged;


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

    private void OnToggleButtonClicked(object sender, EventArgs e)
    {
        FirebaseService fs = new FirebaseService();
        isSaved = !isSaved;
        ToggleButton.Text = isSaved ? "Unsave" : "Save";
        if (isSaved) {
            Debug.WriteLine("Adding Recipe");
            fs.AddSavedRecipe(App.CurrentRecipe, App.CurrentUser);
        }
        else
        {
            Debug.WriteLine("Removing Recipe");
            fs.RemoveRecipe(App.CurrentRecipe, App.CurrentUser);
        }
        App.CurrentRecipe.isSaved = isSaved;    
    }

}
