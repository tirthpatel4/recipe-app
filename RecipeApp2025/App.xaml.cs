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
using banditoth.MAUI.DeviceId;
using banditoth.MAUI.DeviceId.Interfaces;
using Microsoft.Maui.Controls.PlatformConfiguration;
using CommunityToolkit.Maui.Core;
using Firebase.Auth;
namespace RecipeApp2025
  

{
    public partial class App : Application
    {
        private static RecipeService recipeService = new();
        public static Recipe CurrentRecipe { get; set; }
        public static List<Recipe> SavedRecipes { get; set; }
        public static string CurrentUser { get; set; }
        public static UserCredential CurrentUserCredential { get; set; }
        public static bool needsReshuffle { get; set; }
        public static Filter CurrentFilter { get; set; } 
        //light=0, dark = 1, system theme = 2
        public static int ThemeIndicator { get; set;  }
        public static IDeviceIdProvider DeviceIdProvider { get; set; }
        public static string DeviceId { get; set; }
        public static FirebaseService firebase {  get; set; }
        public App()
        {
            InitializeComponent();

            // Ensure colors exist before replacing dictionaries
            if (!Application.Current.Resources.ContainsKey("LightPrimary"))
            {
                Application.Current.Resources.Add("LightPrimary", Color.FromArgb("#D0E0D0"));
            }
            if (!Application.Current.Resources.ContainsKey("DarkPrimary"))
            {
                Application.Current.Resources.Add("DarkPrimary", Color.FromArgb("#4E5E4E"));
            }


            CurrentUser = PersistentDataHelper.GetLogin();
            CurrentFilter = new Filter();
            needsReshuffle = true;
            LoadData();
            /*
            DeviceIdProvider = new DeviceIdProvider();

            DeviceId = DeviceIdProvider.GetDeviceId();
            if( DeviceId == null)
            {
                DeviceId = DeviceIdProvider.GetInstallationId();
            }
            Debug.WriteLine("++++++++++++" + DeviceId);
            

            if (DeviceIdProvider.GetDeviceId() == null)
            {
                Debug.WriteLine("NULL");
            }
            */
            


           
            int theme = PersistentDataHelper.GetTheme();
            if(theme >= 0)
            {
                SwitchTheme(theme);
                ThemeIndicator = theme;
            }
            else
            {
                ThemeIndicator = 0;
                SwitchTheme(0);
                PersistentDataHelper.SetTheme(0);
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
            if (SavedRecipes is not null)
            {
                SavedRecipes.Add(r);
            }
        }

        public static async void RemoveSavedRecipe(Recipe r)
        {
            if (SavedRecipes is not null)
            {
                SavedRecipes.Remove(r);
            }
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