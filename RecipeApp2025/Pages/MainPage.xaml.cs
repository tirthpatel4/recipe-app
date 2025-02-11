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
            SetStackLayoutOrientation();
        }

        private void SetStackLayoutOrientation()
        {
            if (IsInPortrait())
            {
                ButtonStackLayout.Orientation = StackOrientation.Vertical;
            }
            else
            {
                ButtonStackLayout.Orientation = StackOrientation.Horizontal;
            }

        }
        private async void OnDiscoverButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/DiscoverPage");
        }

        private async void OnSavedRecipesButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/SavedRecipesPage");
        }

        private Boolean IsInPortrait()
        {
            double width = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
            double height = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
            return (width <= height);
        }

    }

}
