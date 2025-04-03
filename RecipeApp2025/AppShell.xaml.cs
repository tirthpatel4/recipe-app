using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using System.Diagnostics;

using RecipeApp2025.Pages;
using Font = Microsoft.Maui.Font;
using CommunityToolkit.Maui.Behaviors;
#if ANDROID
using Android.Views; 
using Android.App;
#endif
namespace RecipeApp2025
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            
            InitializeComponent();


            // Removed theme-based control logic
            // var currentTheme = Application.Current!.UserAppTheme;
            // ThemeSegmentedControl.SelectedIndex = currentTheme == AppTheme.Light ? 0 : 1;

            // Register routes for pages
            Routing.RegisterRoute(nameof(DiscoverPage), typeof(DiscoverPage));
            Routing.RegisterRoute(nameof(SavedRecipesPage), typeof(SavedRecipesPage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            

        }

        public static async Task DisplaySnackbarAsync(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Color.FromArgb("#FF3300"),
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(0),
                Font = Font.SystemFontOfSize(18),
                ActionButtonFont = Font.SystemFontOfSize(14)
            };

            var snackbar = Snackbar.Make(message, visualOptions: snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }

        public static async Task DisplayToastAsync(string message)
        {
            // Toast is currently not working in MCT on Windows
            if (OperatingSystem.IsWindows())
                return;

            var toast = Toast.Make(message, textSize: 18);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await toast.Show(cts.Token);
        }

    }
}
