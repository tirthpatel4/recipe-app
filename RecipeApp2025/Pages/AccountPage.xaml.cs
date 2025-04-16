using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Firebase.Auth;
using RecipeApp2025.Resources.Classes;

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
        Debug.WriteLine(App.CurrentUser == null);
        if (App.CurrentUser != null) {
            Debug.WriteLine("Current userrr: " + App.CurrentUser);
            }
       
        signinButton.IsEnabled = App.CurrentUser == "";
        Debug.WriteLine(signinButton.IsEnabled);
        registerButton.IsEnabled = signinButton.IsEnabled;
        signoutButton.IsEnabled = !signinButton.IsEnabled;
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
        if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
        {
            try
            {
                FirebaseAuthService authService = FirebaseAuthService.Instance;
                UserCredential uc = await authService.RegisterUser(_username, _password);

                var user = new Resources.Classes.User
                {
                    Id = uc.User.Uid,
                    Username = _username,
                    Email = _username,
                    UserRecipes = new List<Recipe>()
                };

                var _firebaseService = new FirebaseService(uc.User.Credential.IdToken);
                await _firebaseService.AddUser(user);
                PersistentDataHelper.SetLogin(_username);
                PersistentDataHelper.SetAuth(uc);

                await DisplayAlert("Success", "User registered successfully!", "OK");

                signinButton.IsEnabled = false;
                registerButton.IsEnabled = false;
                signoutButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Neither field can be empty!", "OK");
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (_username != String.Empty && _password != String.Empty)
        {
            try
            {
                FirebaseAuthService authService = FirebaseAuthService.Instance;
                UserCredential uc = await authService.LoginUser(_username, _password);

                await DisplayAlert("Success", "User signed in!", "OK");

                signinButton.IsEnabled = false;
                registerButton.IsEnabled = false;
                signoutButton.IsEnabled = true;
                var _firebaseService = new FirebaseService(uc.User.Credential.IdToken);
                var user = await _firebaseService.GetUser(_username);
                
                if (user != null)
                {
                    Debug.WriteLine("lksglj");
                    App.CurrentUser = user.Username;
                    Debug.WriteLine(App.CurrentUser);
                    App.CurrentUserCredential = uc;
                    PersistentDataHelper.SetLogin(user.Username);
                    PersistentDataHelper.SetAuth(uc);
                    PersistentDataHelper.SetTheme(App.ThemeIndicator);
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }
        }
        else await DisplayAlert("Error", "Neither field can be empty!", "OK");
    }

    private async void OnSignOutClicked(object sender, EventArgs e)
    {
        App.CurrentUser = "";
        App.CurrentUserCredential = null;
        PersistentDataHelper.SetLogin("");
        PersistentDataHelper.SetAuth(null);
        signinButton.IsEnabled = true;
        registerButton.IsEnabled = true;
        signoutButton.IsEnabled = false;
        await DisplayAlert("Success", "Logged out!", "OK");
    }
}