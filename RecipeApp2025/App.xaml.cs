using Newtonsoft.Json.Linq;
using System.Reflection;
using RecipeApp2025.Resources.Classes;
using System;
using System.IO;
using RecipeApp2025.Services;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using RecipeApp2025.Resources.Styles;
namespace RecipeApp2025
  

{
    public partial class App : Application
    {
        private static RecipeService recipeService = new();
        public static Recipe CurrentRecipe { get; set; }
        public static List<Recipe> SavedRecipes { get; set; }
        public static string CurrentUser { get; set; }
        public static bool DarkModeIsOn { get; set;  }
        public App()
        {
            InitializeComponent();
            CurrentUser = String.Empty;
            LoadData();

            /* change later */
            DarkModeIsOn = false;
            SwitchTheme(DarkModeIsOn);

        }

        async void LoadData()
        {
            //SavedRecipes = await db.GetObjectsAsync();
        }
        public static void SwitchTheme(bool isDarkMode)
        {
            var theme = isDarkMode ? (ResourceDictionary)new DarkTheme() : (ResourceDictionary)new LightTheme();
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(theme);
            Application.Current.MainPage.Handler?.UpdateValue(nameof(VisualElement.BackgroundColor));
        }

        public static async Task<Boolean> ChangeCurrentRecipe(Recipe r)
        {
            Debug.WriteLine("IN CURRENT RECIPE");
            CurrentRecipe = r;
            Debug.WriteLine("IN app: " + Process.GetCurrentProcess().Id);
            if(CurrentRecipe.Ingredients_List.Count == 0)
            {
                Boolean temp = await recipeService.GetIngredientsAsync(r);
                Debug.WriteLine("LOADED INGREDIENTS" + temp);
            }

            if (CurrentRecipe.Steps_List.Count == 0)
            {
                Boolean temp = await recipeService.GetStepsAsync(r);
                Debug.WriteLine("LOADED STEPS");
            }
            return true;

        }

        public static async void AddSavedRecipe(Recipe r)
        {
            SavedRecipes.Add(r);
        }

        public static async void RemoveSavedRecipe(Recipe r)
        {
            SavedRecipes.Remove(r);
        }

        public static Boolean IsInPortrait()
        {
            double width = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
            double height = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
            return (width <= height);
        }

        public static void SetStackLayoutOrientation(StackLayout s)
        {
            if (App.IsInPortrait())
            {
                s.Orientation = StackOrientation.Vertical;
            }
            else
            {
                s.Orientation = StackOrientation.Horizontal;
            }

        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}