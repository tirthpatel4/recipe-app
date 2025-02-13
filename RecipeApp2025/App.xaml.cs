using Newtonsoft.Json.Linq;
using System.Reflection;
using RecipeApp2025.Resources.Classes;
using System;
using System.IO;
namespace RecipeApp2025
  

{
    public partial class App : Application
    {
        public static Recipe CurrentRecipe { get; set; }
        public static List<Recipe> SavedRecipes { get; set; }

        private static FirebaseService db; 

        public App()
        {
            InitializeComponent();
            db = new FirebaseService();
            LoadData();


        }

        async void LoadData()
        {
            SavedRecipes = await db.GetRecipes();
        }

        public static void ChangeCurrentRecipe(Recipe r)
        {
            CurrentRecipe = r;
        }

        public static async void AddSavedRecipe(Recipe r)
        {
            SavedRecipes.Add(r);
            await db.AddRecipe(r);
        }

        public static async void RemoveSavedRecipe(Recipe r)
        {
            SavedRecipes.Remove(r);
            await db.RemoveRecipe(r);
        }    

        

        
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
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
    }
}