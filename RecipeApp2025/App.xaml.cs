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
        private static FirebaseService firebaseService = new FirebaseService();
        

        public App()
        {
            InitializeComponent();
            RestoreList();
            
        }

        public static void ChangeCurrentRecipe(Recipe r)
        {
            CurrentRecipe = r;
        }

        public static async void AddSavedRecipe(Recipe r)
        {
            r.isSaved = true;
            SavedRecipes.Add(r);
            await firebaseService.AddRecipe(r);
        }

        public static async void RemoveSavedRecipe(Recipe r)
        {
            r.isSaved = false;
            SavedRecipes.Remove(r);
            await firebaseService.RemoveRecipe(r);
        }    
            
       public static async void RestoreList()
       {
            SavedRecipes = await firebaseService.GetRecipes();
            for(int i=0;  i < SavedRecipes.Count; i++)
            {
                SavedRecipes[i].isSaved = true;
            }
       }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}