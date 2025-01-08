using MobileAppSpring2025.Models;
using MobileAppSpring2025.PageModels;

namespace MobileAppSpring2025.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}