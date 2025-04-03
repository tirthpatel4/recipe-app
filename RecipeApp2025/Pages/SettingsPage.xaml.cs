using System;
using System.Diagnostics;
using RecipeApp2025.Resources.Classes;

namespace RecipeApp2025.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        lightdarkpicker.SelectedIndex = App.ThemeIndicator;

    }
    void OnThemePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        
        Debug.WriteLine("LAKJGLKJADSG");
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {

            App.SwitchTheme(selectedIndex);
            App.ThemeIndicator = selectedIndex;
            PersistentDataHelper.SetTheme(selectedIndex);


        }
    }

}