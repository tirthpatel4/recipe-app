using System.Runtime.CompilerServices;

namespace RecipeApp2025.Pages
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            this.SizeChanged += OnSizeChanged;
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            App.SetStackLayoutOrientation(ButtonStackLayout);
        }

        
        
        private async void OnDiscoverButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/DiscoverPage");
        }

        private async void OnSavedRecipesButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/SavedRecipesPage");
        }

        

    }

}
