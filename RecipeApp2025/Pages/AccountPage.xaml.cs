using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RecipeApp2025.Resources.Classes;

namespace RecipeApp2025.Pages;

public partial class AccountPage : ContentPage
{
	private string _username;
	private string _password;
    private readonly FirebaseService _firebaseService;
    public AccountPage()
	{
		InitializeComponent();
        _username = String.Empty;
        _password = String.Empty;
        _firebaseService = new FirebaseService();
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
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (_username != String.Empty && _password != String.Empty)
        {
            var verify = await _firebaseService.GetUser(_username);
            if (verify is null)
            {
                var user = new User
                {
                    Username = _username,
                    Password = _password
                };
                await _firebaseService.AddUser(user);
                await DisplayAlert("Success", "User registered successfully!", "OK");
            }
            else await DisplayAlert("Error", "A user with that name has already been registered. Please enter a different username", "OK");
        }
        else await DisplayAlert("Error", "Neither field can be empty!", "OK");
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (_username != String.Empty && _password != String.Empty)
        {
            var user = await _firebaseService.GetUser(_username);
            if (user != null && user.Password == _password)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Login failed. Username or password is incorrect.", "OK");
            }
        }
        else await DisplayAlert("Error", "Neither field can be empty!", "OK");
    }
}