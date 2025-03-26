using System;
using System.Diagnostics;

namespace RecipeApp2025.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    void OnThemePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        
        Debug.WriteLine("LAKJGLKJADSG");
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            switch (selectedIndex)
            {
                case 0:
                    if (App.DarkModeIsOn)
                    {
                        App.SwitchTheme(false);
                    }
                    break;
                case 1:
                    if (!App.DarkModeIsOn)
                    {
                        App.SwitchTheme(true);
                    }
                    break;
                default:
                    break;
            
            }
        }
    }

}