using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;

public class FirebaseAuthService
{
    private static FirebaseAuthService _instance;
    private static readonly object _lock = new object();
    private readonly FirebaseAuthClient _client;

    public FirebaseAuthService()
    {
        _client = new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyA-jYOMOCW7SJpa2u4IBrxqAH4OPGfMPVU",
            AuthDomain = "recipeapp2025.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
        });
    }

    public static FirebaseAuthService Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock) // Ensure thread safety
                {
                    if (_instance == null)
                    {
                        _instance = new FirebaseAuthService();
                    }
                }
            }
            return _instance;
        }
    }


    // User Registration
    public async Task<UserCredential> RegisterUser(string email, string password)
    {
        var auth = await _client.CreateUserWithEmailAndPasswordAsync(email, password);
        return auth;
    }

    public async Task<UserCredential> LoginUser(string email, string password)
    {
        var auth = await _client.SignInWithEmailAndPasswordAsync(email, password);
        return auth;
    }
}