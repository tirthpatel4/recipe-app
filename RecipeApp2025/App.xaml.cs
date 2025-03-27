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
        //light=0, dark = 1, system theme = 2
        public static int ThemeIndicator { get; set;  }
        public App()
        {
            InitializeComponent();
            CurrentUser = String.Empty;
            LoadData();

            if(CurrentUser != "")
            {
                // code to set according to existing preferences
            }
            else
            {
                Debug.WriteLine("hello");
                SwitchTheme(0);
                ThemeIndicator = 0;
            }

            /* set response to device theme change */
            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                /* if set to use device settings, switch theme */
                if (ThemeIndicator == 2)
                {
                    SwitchTheme(2);
                }
                
            };
        }

        async void LoadData()
        {
            //SavedRecipes = await db.GetObjectsAsync();
        }
        public static void SwitchTheme(int i)
        {
            var theme = (ResourceDictionary)(new LightTheme());
            if (i == 1)
            {
                theme = (ResourceDictionary)(new DarkTheme());
            }else if (i == 2)
            {
                AppTheme currentTheme = Application.Current.RequestedTheme;
                Debug.WriteLine(currentTheme);
                if(currentTheme == AppTheme.Light)
                {
                    SwitchTheme(0);
                    return;
                }else if(currentTheme == AppTheme.Dark)
                {
                    SwitchTheme(1);
                    return;
                }
            }
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if(mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(theme);
            }
            
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