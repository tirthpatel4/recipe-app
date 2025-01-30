using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RecipeApp2025.Pages;

public partial class AccountPage : ContentPage
{
	private string _username;
	private string _password;
    public AccountPage()
	{
		InitializeComponent();
        _username = String.Empty;
        _password = String.Empty;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        UsernameEntry.TextChanged += OnUsernameChanged;
        PasswordEntry.TextChanged += OnPasswordChanged;
    }
    private void OnUsernameChanged(object sender, TextChangedEventArgs e)
    {
        _username = e.NewTextValue;
    }

    private void OnPasswordChanged(object sender, TextChangedEventArgs e)
    {
        _password = e.NewTextValue;
    }
}