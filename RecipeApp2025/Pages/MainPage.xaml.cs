using System.Runtime.CompilerServices;

namespace RecipeApp2025.Pages
{
    public partial class MainPage : ContentPage
    {

        private string _keyword;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            this.SizeChanged += OnSizeChanged;
            _keyword = string.Empty;
            //AdjustSizing();
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SavedRecipesButton.IsEnabled = App.CurrentUser.Length > 0;
            if (App.CurrentUser != String.Empty)
            {
                LoginWelcome.Text = "Welcome back, " + App.CurrentUser + "!";
            }
            KeywordEntry.TextChanged += OnKeywordChanged;
        }
        private void OnKeywordChanged(object sender, TextChangedEventArgs e)
        {
            _keyword = e.NewTextValue;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            SetStackLayoutOrientation();
            //AdjustSizing();
        }

        private void AdjustSizing()
        {
            if (App.IsInPortrait())
            {
                MainGrid.RowSpacing = 40;
                DiscoverButton.HeightRequest = 120;
                SavedRecipesButton.HeightRequest = 120;

            }
            else
            {
                MainGrid.RowSpacing = 30;
                DiscoverButton.HeightRequest = 100;
                SavedRecipesButton.HeightRequest = 100;
            }
        }
        private void SetStackLayoutOrientation()
        {
            if (IsInPortrait())
            {
                ButtonStackLayout.Orientation = StackOrientation.Vertical;
            }
            else { 
           
               ButtonStackLayout.Orientation = StackOrientation.Horizontal;
            }

        }
        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"/DiscoverPage?keyword={_keyword}");
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