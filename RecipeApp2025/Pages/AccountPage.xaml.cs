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
        
        signinButton.IsEnabled = App.CurrentUser == "" || App.CurrentUser == null;
        Debug.WriteLine(signinButton.IsEnabled);
        registerButton.IsEnabled = signinButton.IsEnabled; 
        signoutButton.IsEnabled = !signinButton.IsEnabled;  

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
                App.CurrentUser = user.Username;
                PersistentDataHelper.SetLogin(_username);
                PersistentDataHelper.SetTheme(App.ThemeIndicator);
                await _firebaseService.AddUser(user);
                await DisplayAlert("Success", "User registered successfully!", "OK");
                signinButton.IsEnabled = false;
                registerButton.IsEnabled = false;
                signoutButton.IsEnabled = true; 
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
                App.CurrentUser = user.Username;
                await DisplayAlert("Success", "Login successful!", "OK");
                PersistentDataHelper.SetLogin(user.Username);
                PersistentDataHelper.SetTheme(App.ThemeIndicator);
                signinButton.IsEnabled = false;
                registerButton.IsEnabled = false;
                signoutButton.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Error", "Login failed. Username or password is incorrect.", "OK");
            }
        }
        else await DisplayAlert("Error", "Neither field can be empty!", "OK");
    }

    private async void OnSignOutClicked(object sender, EventArgs e)
    {
        App.CurrentUser = "";
        PersistentDataHelper.SetLogin("");
        signinButton.IsEnabled = true;
        registerButton.IsEnabled = true;
        signoutButton.IsEnabled = false;
        await DisplayAlert("Success", "Logged out!", "OK");
    }
}