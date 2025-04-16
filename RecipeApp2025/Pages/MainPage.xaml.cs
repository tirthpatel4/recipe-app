using System.Diagnostics;
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

            if (App.CurrentUser == "")
            {
                Debug.WriteLine("not logged in");
                SavedRecipesButton.IsEnabled = false;
            }

             setImgProperties();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("IN MAIN : " + App.CurrentUser);
            SavedRecipesButton.IsEnabled = App.CurrentUser.Length > 0;
            if (App.CurrentUser != String.Empty)
            {
                LoginWelcome.Text = "Welcome back, " + App.CurrentUser + "!";
            }
            else
            {
                LoginWelcome.Text = "Welcome!";
            }
            KeywordEntry.TextChanged += OnKeywordChanged;
            setImgProperties();

        }
        private void OnKeywordChanged(object sender, TextChangedEventArgs e)
        {
            _keyword = e.NewTextValue;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            SetStackLayoutOrientation();
            setImgProperties();

            //AdjustSizing();
        }

        private void setImgProperties()
        {
            //MainGrid.HeightRequest = .95*DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;
            //foodimg.WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density;
            //foodimg.HeightRequest = .2*DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;
           
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
            App.needsReshuffle = true;
            await Shell.Current.GoToAsync($"/DiscoverPage?keyword={_keyword}");
        }
        private async void OnDiscoverButtonClicked(object sender, EventArgs e)
        {
            App.needsReshuffle = true;
            await Shell.Current.GoToAsync("/DiscoverPage");
        }

        private async void OnSavedRecipesButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/SavedRecipesPage");
        }
        private async void OnFilterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/FilterPage");
        }

        private Boolean IsInPortrait()
        {
            double width = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
            double height = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
            return (width <= height);
        }

    }

}