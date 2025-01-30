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
        public static List<Recipe> SavedRecipes {  get; set; }
        public App()
        {
            InitializeComponent();
            SavedRecipes = new List<Recipe>();
        }

        public static void ChangeCurrentRecipe(Recipe r)
        {
            CurrentRecipe = r;
        }

        public static void AddSavedRecipe(Recipe r)
        {
            SavedRecipes.Add(r);
        }

        public static void RemoveSavedRecipe(Recipe r)
        {
            SavedRecipes.Remove(r);
        }    

        public static void WriteSavesToFile()
        {
            string path = @"C:\Users\samba\source\repos\Collaborative-Software-Development-Club\Spring-2025-Mobile-App\RecipeApp2025\Resources\Raw\Saves.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    for (int i = 0; i < SavedRecipes.Count; i++)
                    {
                        sw.WriteLine(SavedRecipes[i].Name);
                    }
                }
            }
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}