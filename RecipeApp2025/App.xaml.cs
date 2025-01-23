using RecipeApp2025.Resources.Classes;
namespace RecipeApp2025
  
{
    public partial class App : Application
    {
        public static Recipe CurrentRecipe { get; set; }
        public App()
        {
            InitializeComponent();
        }
        
        public static void ChangeCurrentRecipe(Recipe r)
        {
            CurrentRecipe = r;
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}