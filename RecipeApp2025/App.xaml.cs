using RecipeApp2025.Resources.Classes;
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
            SavedRecipes.Add(new Recipe("Saved Rec 1"));
            SavedRecipes.Add(new Recipe("Saved Rec 2"));
            SavedRecipes.Add(new Recipe("Saved Rec 3"));
            SavedRecipes.Add(new Recipe("Saved Rec 4"));
            SavedRecipes.Add(new Recipe("Saved Rec 5"));
            SavedRecipes.Add(new Recipe("Saved Rec 6")); 
            SavedRecipes.Add(new Recipe("Saved Rec 7"));
            SavedRecipes.Add(new Recipe("Saved Rec 8"));
            SavedRecipes.Add(new Recipe("Saved Rec 9"));
            SavedRecipes.Add(new Recipe("Saved Rec 10"));
            SavedRecipes.Add(new Recipe("Saved Rec 11"));
            SavedRecipes.Add(new Recipe("Saved Rec 3"));

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
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}